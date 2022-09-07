using CSharpTest.Net.Collections;
using CSharpTest.Net.Serialization;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Core.Exceptions;
using Umbraco.Cms.Core.Hosting;
using Umbraco.Cms.Core.IO;

namespace Umbraco.Cms.Infrastructure.PublishedCache.DataSource
{
    public class BPlusTreeKeyValueStoreFactory<TKey, TValue> : IKeyValueStoreFactory<TKey, TValue>
    {
        private readonly NuCacheSettings _nucacheSettings;
        private readonly IKeyValueStoreSerializer<TValue> _serializer;
        private readonly IKeyValueStoreSerializer<TKey> _keySerializer;
        private readonly IIOHelper _iOHelper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private string _folderName;

        public BPlusTreeKeyValueStoreFactory(IOptionsMonitor<NuCacheSettings> nucacheSettings,
                                                      IKeyValueStoreSerializer<TValue> valueSerializer,
                                                      IKeyValueStoreSerializer<TKey> keySerializer,
                                                      IIOHelper iOHelper,
                                                      IHostingEnvironment hostingEnvironment)
        {
            _nucacheSettings = nucacheSettings.CurrentValue;
            _serializer = valueSerializer;
            _keySerializer = keySerializer;
            _iOHelper = iOHelper;
            _hostingEnvironment = hostingEnvironment;
            _folderName = "NuCache";
        }

        public IKeyValueStore<TKey, TValue> Get(string name, IComparer<TKey> keyComparer = null, bool isReadOnly = false, bool enableCount = false)
        {
            var localContentDbPath = GetDbPath(name);
            var localContentCacheFilesExist = StoreExists(name);
            var keySerializer = new BPlusTreeFromKeyValueStoreSerializerAdapter<TKey>(_keySerializer);
            var valueSerializer = new BPlusTreeFromKeyValueStoreSerializerAdapter<TValue>(_serializer);
            var bplusTree = GetTree(localContentDbPath, localContentCacheFilesExist, keySerializer, valueSerializer, keyComparer, isReadOnly, enableCount);
            return new BPlusTreeKeyValueStore<TKey, TValue>(bplusTree, localContentDbPath, localContentCacheFilesExist, enableCount, _iOHelper);
        }

        private bool StoreExists(string name)
        {
            var localContentDbPath = GetDbPath(name);
            return File.Exists(localContentDbPath);
        }
        public void Drop(string name)
        {
            var localContentDbPath = GetDbPath(name);
            var localContentCacheFilesExist = StoreExists(name);
            var dictDoc = new BPlusTreeKeyValueStore<TKey, TValue>(null, localContentDbPath, localContentCacheFilesExist, false, _iOHelper);
            dictDoc.Drop();
        }
        protected virtual string GetDbPath(string name)
        {
            var contentPath = GetLocalFilesPath();
            var localContentDbPath = Path.Combine(contentPath, $"{name}.db");
            return localContentDbPath;
        }
        private string GetLocalFilesPath()
        {
            var path = Path.Combine(_hostingEnvironment.LocalTempPath, _folderName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        protected virtual BPlusTree<TKey, TValue> GetTree(string filepath, bool exists, ISerializer<TKey> keySerializer,
            ISerializer<TValue> valueSerializer,
            IComparer<TKey> keyComparer = null, bool isReadOnly = false, bool enableCount = false)
        {

            var options = new BPlusTree<TKey, TValue>.OptionsV2(keySerializer, valueSerializer)
            {
                CreateFile = exists ? CreatePolicy.IfNeeded : CreatePolicy.Always,
                FileName = filepath,

                // read or write but do *not* keep in memory
                CachePolicy = CachePolicy.None,

                // default is 4096, min 2^9 = 512, max 2^16 = 64K
                FileBlockSize = GetBlockSize(_nucacheSettings),

                //HACK: Forces FileOptions to be WriteThrough here: https://github.com/mamift/CSharpTest.Net.Collections/blob/9f93733b3af7ee0e2de353e822ff54d908209b0b/src/CSharpTest.Net.Collections/IO/TransactedCompoundFile.cs#L316-L327,
                // as the reflection uses otherwise will failed in .NET Core as the "_handle" field in FileStream is renamed to "_fileHandle".
                StoragePerformance = StoragePerformance.CommitToDisk,

                // other options?
                ReadOnly = isReadOnly,


            };
            if (keyComparer != null)
            {
                options.KeyComparer = keyComparer;
            }

            var tree = new BPlusTree<TKey, TValue>(options);

            // anything?
            //btree.
            if (enableCount)
            {
                tree.EnableCount();
            }
            return tree;
        }

        private static int GetBlockSize(NuCacheSettings settings)
        {
            var blockSize = 4096;

            var appSetting = settings.BTreeBlockSize;
            if (!appSetting.HasValue)
                return blockSize;

            blockSize = appSetting.Value;

            var bit = 0;
            for (var i = blockSize; i != 1; i >>= 1)
                bit++;
            if (1 << bit != blockSize)
                throw new ConfigurationException($"Invalid block size value \"{blockSize}\": must be a power of two.");
            if (blockSize < 512 || blockSize > 65536)
                throw new ConfigurationException($"Invalid block size value \"{blockSize}\": must be >= 512 and <= 65536.");

            return blockSize;
        }

        public bool StorageExists(string name) => StoreExists(name);
    }
}
