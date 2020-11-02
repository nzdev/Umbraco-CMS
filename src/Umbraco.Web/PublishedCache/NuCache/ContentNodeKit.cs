using System;
using System.Collections;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedCache.NuCache.DataSource;

namespace Umbraco.Web.PublishedCache.NuCache
{
    // what's needed to actually build a content node
    public struct ContentNodeKit : IContentNodeKit
    {
        public IContentNode Node { get; set; }
        public int ContentTypeId { get; set; }
        public IContentData DraftData { get; set; }
        public IContentData PublishedData { get; set; }
        public ContentNodeKitLoadState LoadState { get; set; }

        public bool IsEmpty => Node == null;

        public bool IsNull => ContentTypeId < 0;

        public static ContentNodeKit Empty { get; } = new ContentNodeKit();
        public static ContentNodeKit Null { get; } = new ContentNodeKit { ContentTypeId = -1 };
        

        public void Build(
            IPublishedContentType contentType,
            IPublishedSnapshotAccessor publishedSnapshotAccessor,
            IVariationContextAccessor variationContextAccessor,
            bool canBePublished)
        {
            var draftData = DraftData;

            // no published data if it cannot be published (eg is masked)
            var publishedData = canBePublished ? PublishedData : null;

            // we *must* have either published or draft data
            // if it cannot be published, published data is going to be null
            // therefore, ensure that draft data is not
            if (draftData == null && !canBePublished)
                draftData = PublishedData;

            Node.SetContentTypeAndData(contentType, draftData, publishedData, publishedSnapshotAccessor, variationContextAccessor);
        }

        public IContentNodeKit Clone()
        {
           var clone = new ContentNodeKit
               {
                   ContentTypeId = ContentTypeId,
                   DraftData = DraftData,
                   PublishedData = PublishedData,
                   Node = Node.Clone()
               };
            clone.SetLazyLoader(_lazyLoader);
            return clone;
        }
            

        Func<int, IContentNodeKit> _lazyLoader;
        public void SetLazyLoader(Func<int, IContentNodeKit> lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
    }
}
