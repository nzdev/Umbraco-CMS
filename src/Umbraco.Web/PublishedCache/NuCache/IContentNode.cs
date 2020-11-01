using System;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedCache.NuCache.DataSource;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public interface IContentNode
    {
        IPublishedContentType ContentType { get; set; }
        DateTime CreateDate { get; }
        int CreatorId { get; }
        IPublishedContent DraftModel { get; }
        int FirstChildContentId { get; set; }
        bool HasPublished { get; }
        int Id { get; }
        int LastChildContentId { get; set; }
        int Level { get; }
        int NextSiblingContentId { get; set; }
        int ParentContentId { get; }
        string Path { get; }
        int PreviousSiblingContentId { get; set; }
        IPublishedContent PublishedModel { get; }
        int SortOrder { get; }
        Guid Uid { get; }

        bool HasPublishedCulture(string culture);
        void SetContentTypeAndData(IPublishedContentType contentType, IContentData draftData, IContentData publishedData, IPublishedSnapshotAccessor publishedSnapshotAccessor, IVariationContextAccessor variationContextAccessor);
        IContentNodeKit ToKit();
        IContentNode Clone(IPublishedContentType contentType = null);
    }
}
