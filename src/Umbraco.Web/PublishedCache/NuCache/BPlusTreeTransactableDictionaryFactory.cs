using CSharpTest.Net.Collections;
using CSharpTest.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Umbraco.Core.Configuration;
using Umbraco.Web.Install;

namespace Umbraco.Web.PublishedCache.NuCache
{

    public class BPlusTreeTransactableDictionaryFactory : ITransactableDictionaryFactory
    {
        private readonly IGlobalSettings _globalSettings;
        private readonly ISerializer<IContentNodeKit> _contentDataSerializer;

        public BPlusTreeTransactableDictionaryFactory(IGlobalSettings globalSettings,
            ISerializer<IContentNodeKit> contentDataSerializer)
        {
            _globalSettings = globalSettings;
            _contentDataSerializer = contentDataSerializer;
        }

        public ITransactableDictionary<int, IContentNodeKit> Get(ContentCacheEntityType entityType)
        {
            switch (entityType)
            {
                case ContentCacheEntityType.Document:
                    var localContentDbPath = GetContentDbPath();
                    var localContentCacheFilesExist = File.Exists(localContentDbPath);
                    return GetDocumentBPlusTree(localContentDbPath, localContentCacheFilesExist);
                case ContentCacheEntityType.Media:
                    var localMediaDbPath = GetMediaDbPath();
                    var localMediaCacheFilesExist = File.Exists(localMediaDbPath);
                    var fullLoadMediaDictionary = GetTree(localMediaDbPath, localMediaCacheFilesExist, _contentDataSerializer);
                    return new BPlusTreeTransactableDictionary<int, IContentNodeKit>(fullLoadMediaDictionary, localMediaDbPath, localMediaCacheFilesExist);
                case ContentCacheEntityType.Member:
                    throw new ArgumentException("Unsupported Entity Type", nameof(entityType));
                default:
                    throw new ArgumentException("Unsupported Entity Type", nameof(entityType));
            }
        }
        protected virtual ITransactableDictionary<int, IContentNodeKit> GetDocumentBPlusTree(string localContentDbPath, bool localContentCacheFilesExist)
        {
            var fullLoadDictionary = GetTree(localContentDbPath, localContentCacheFilesExist, _contentDataSerializer);
            return new BPlusTreeTransactableDictionary<int, IContentNodeKit>(fullLoadDictionary, localContentDbPath, localContentCacheFilesExist);
        }
        

        private string GetContentDbPath()
        {
            var contentPath = GetLocalFilesPath();
            var localContentDbPath = Path.Combine(contentPath, "NuCache.Content.db");
            return localContentDbPath;
        }

        private string GetMediaDbPath()
        {
            var mediaPath = GetLocalFilesPath();
            var localMediaDbPath = Path.Combine(mediaPath, "NuCache.Media.db");
            return localMediaDbPath;
        }

        private string GetLocalFilesPath()
        {
            var path = Path.Combine(_globalSettings.LocalTempPath, "NuCache");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        /// <summary>
        /// Ensures that the ITransactableDictionaryFactory has the proper environment to run.
        /// </summary>
        /// <param name="errors">The errors, if any.</param>
        /// <returns>A value indicating whether the ITransactableDictionaryFactory has the proper environment to run.</returns>
        public bool EnsureEnvironment(out IEnumerable<string> errors)
        {
            // must have app_data and be able to write files into it
            var ok = FilePermissionHelper.TryCreateDirectory(GetLocalFilesPath());
            errors = ok ? Enumerable.Empty<string>() : new[] { "NuCache local files." };
            return ok;
        }

        public virtual BPlusTree<int, IContentNodeKit> GetTree(string filepath, bool exists, ISerializer<IContentNodeKit> valueSerializer,bool readOnly = false)
        {
            var keySerializer = new PrimitiveSerializer();
            var options = new BPlusTree<int, IContentNodeKit>.OptionsV2(keySerializer, valueSerializer)
            {
                CreateFile = exists ? CreatePolicy.IfNeeded : CreatePolicy.Always,
                FileName = filepath,

                // read or write but do *not* keep in memory
                CachePolicy = CachePolicy.None,

                // default is 4096, min 2^9 = 512, max 2^16 = 64K
                FileBlockSize = GetBlockSize(),
                ReadOnly = readOnly
                // other options?
            };

            var tree = new BPlusTree<int, IContentNodeKit>(options);

            // anything?
            //btree.

            return tree;
        }

        private static int GetBlockSize()
        {
            var blockSize = 4096;

            var appSetting = ConfigurationManager.AppSettings["Umbraco.Web.PublishedCache.NuCache.BTree.BlockSize"];
            if (appSetting == null)
                return blockSize;

            if (!int.TryParse(appSetting, out blockSize))
                throw new ConfigurationErrorsException($"Invalid block size value \"{appSetting}\": not a number.");

            var bit = 0;
            for (var i = blockSize; i != 1; i >>= 1)
                bit++;
            if (1 << bit != blockSize)
                throw new ConfigurationErrorsException($"Invalid block size value \"{blockSize}\": must be a power of two.");
            if (blockSize < 512 || blockSize > 65536)
                throw new ConfigurationErrorsException($"Invalid block size value \"{blockSize}\": must be >= 512 and <= 65536.");

            return blockSize;
        }

        public bool IsPopulated(ContentCacheEntityType entityType)
        {
            switch (entityType)
            {
                case ContentCacheEntityType.Document:
                    return File.Exists(GetContentDbPath());
                case ContentCacheEntityType.Media:
                    return File.Exists(GetMediaDbPath());
                case ContentCacheEntityType.Member:
                    throw new ArgumentException("Unsupported Entity Type", nameof(entityType));
                default:
                    throw new ArgumentException("Unsupported Entity Type", nameof(entityType));
            }
        }
    }
}
