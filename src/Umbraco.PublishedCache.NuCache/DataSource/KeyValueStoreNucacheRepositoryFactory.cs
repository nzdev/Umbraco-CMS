namespace Umbraco.Cms.Infrastructure.PublishedCache
{
    public class KeyValueStoreNucacheRepositoryFactory : INucacheRepositoryFactory
    {
        private readonly IKeyValueStoreFactory<int, ContentNodeKit> _transactableDictionaryFactory;

        public KeyValueStoreNucacheRepositoryFactory(IKeyValueStoreFactory<int, ContentNodeKit> transactableDictionaryFactory)
        {
            _transactableDictionaryFactory = transactableDictionaryFactory;
        }

        public bool ContentRespositoryStorageExists() => _transactableDictionaryFactory.StorageExists(NuCacheConstants.NuCache.ContentDatabaseName);

        public INucacheContentRepository GetContentRepository()
        {
            var transactableDictionary = _transactableDictionaryFactory.Get(NuCacheConstants.NuCache.ContentDatabaseName);
            return new KeyValueStoreNucacheRepository(transactableDictionary);
        }

        public INucacheMediaRepository GetMediaRepository()
        {
            var transactableDictionary = _transactableDictionaryFactory.Get(NuCacheConstants.NuCache.MediaDatabaseName);
            return new KeyValueStoreNucacheRepository(transactableDictionary);
        }

        public bool MediaRespositoryStorageExists() => _transactableDictionaryFactory.StorageExists(NuCacheConstants.NuCache.MediaDatabaseName);
    }
}
