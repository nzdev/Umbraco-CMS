using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Configuration;
using Umbraco.Web.Install;
using Umbraco.Web.PublishedCache.NuCache;

namespace Umbraco.PublishedCache.NuCache.LiteDb
{
    public class LiteDbTransactableDictionaryFactory : ITransactableDictionaryFactory
    {
        private readonly IGlobalSettings _globalSettings;

        public LiteDbTransactableDictionaryFactory(IGlobalSettings globalSettings)
        {
            _globalSettings = globalSettings;
        }

        public ITransactableDictionary<int, ContentNodeKit> Create(string filepath, bool exists)
        {
            var connectionString = filepath;// System.Configuration.ConfigurationManager.ConnectionStrings["LiteDBNuCache"].ConnectionString;
            var name = filepath.Split('\\').Last().Replace(".", "");
            return new LiteDbTransactableDictionary<int, ContentNodeKit>(connectionString, name);
        }

        public ITransactableDictionary<int, ContentNodeKit> Get(ContentCacheEntityType entityType)
        {
            switch (entityType)
            {
                case ContentCacheEntityType.Document:
                    var localContentDbPath = GetContentDbPath();
                    var connectionStringContent = new ConnectionString()
                    {
                        Filename = localContentDbPath
                    };
                    var nameContent = ContentCollectionName();
                    return new LiteDbTransactableDictionary<int, ContentNodeKit>(connectionStringContent, nameContent);
                case ContentCacheEntityType.Media:
                    var localMediaDbPath = GetMediaDbPath();
                    var connectionString = new ConnectionString()
                    {
                        Filename = localMediaDbPath
                    };
                    var name = MediaCollectionName();
                    return new LiteDbTransactableDictionary<int, ContentNodeKit>(connectionString, name);
                case ContentCacheEntityType.Member:
                    throw new ArgumentException("Unsupported Entity Type", nameof(entityType));
                default:
                    throw new ArgumentException("Unsupported Entity Type", nameof(entityType));
            }
        }

        public string MediaCollectionName()
        {
            return "NuCacheMedia";
        }

        public string ContentCollectionName()
        {
            return "NuCacheContent";
        }

        public string GetContentDbPath()
        {
            var contentPath = GetLocalFilesPath();
            var localContentDbPath = Path.Combine(contentPath, "NuCache.Content.db");
            return localContentDbPath;
        }

        public string GetMediaDbPath()
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
