using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Logging;
namespace Umbraco.Web.PublishedCache.NuCache
{
    public interface IContentStoreFactory
    {
        IContentStore GetContentStore(
           IPublishedSnapshotAccessor publishedSnapshotAccessor,
           IVariationContextAccessor variationContextAccessor,
           ILogger logger,
           ITransactableDictionary<int, IContentNodeKit> localDb = null);
    }
}
