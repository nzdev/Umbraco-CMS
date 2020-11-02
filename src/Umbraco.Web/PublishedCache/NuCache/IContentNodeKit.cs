using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedCache.NuCache.DataSource;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public interface IContentNodeKit : ILazyLoad<int, IContentNodeKit>
    {
        int ContentTypeId { get; set; }
        IContentData DraftData { get; set; }
        bool IsEmpty { get; }
        bool IsNull { get; }
        IContentNode Node { get; set; }
        IContentData PublishedData { get; set; }
        ContentNodeKitLoadState LoadState { get; set; }

        void Build(IPublishedContentType contentType, IPublishedSnapshotAccessor publishedSnapshotAccessor, IVariationContextAccessor variationContextAccessor, bool canBePublished);
        IContentNodeKit Clone();
    }
}
