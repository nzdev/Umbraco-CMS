using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Logging;
using Umbraco.Core.Models.PublishedContent;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public class ContentStoreFactory : IContentStoreFactory
    {
        public IContentStore GetContentStore(IPublishedSnapshotAccessor publishedSnapshotAccessor, IVariationContextAccessor variationContextAccessor, ILogger logger, ITransactableDictionary<int, ContentNodeKit> localDb = null)
        {
            return new ContentStore(publishedSnapshotAccessor, variationContextAccessor, logger, localDb);
        }
    }
}
