using System;
using System.Collections.Generic;
using System.Text;
using Umbraco.Cms.Core;
using Umbraco.Cms.Infrastructure.PublishedCache.Persistence;

namespace Umbraco.Cms.Infrastructure.PublishedCache.DataSource
{
    public class TransactableDictionaryNucacheRepositoryFactory : INucacheRepositoryFactory
    {
        private readonly ITransactableDictionaryFactory<int, ContentNodeKit> _transactableDictionaryFactory;

        public TransactableDictionaryNucacheRepositoryFactory(ITransactableDictionaryFactory<int, ContentNodeKit> transactableDictionaryFactory)
        {
            _transactableDictionaryFactory = transactableDictionaryFactory;
        }

        public INucacheNoSqlContentRepository GetContentRepository()
        {
            var transactableDictionary = _transactableDictionaryFactory.Get(Constants.NuCache.ContentDatabaseName);
            return new TransactableDictionaryNucacheRepository(transactableDictionary);
        }

        public INucacheNoSqlMediaRepository GetMediaRepository()
        {
            var transactableDictionary = _transactableDictionaryFactory.Get(Constants.NuCache.MediaDatabaseName);
            return new TransactableDictionaryNucacheRepository(transactableDictionary);
        }
    }
}
