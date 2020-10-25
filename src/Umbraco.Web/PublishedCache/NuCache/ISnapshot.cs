using System;
using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public interface ISnapshot
    {
        long Gen { get; }
        bool IsEmpty { get; }

        void Dispose();
        IContentNode Get(Guid id);
        IContentNode Get(int id);
        IEnumerable<IContentNode> GetAll();
        IEnumerable<IContentNode> GetAtRoot();
        IPublishedContentType GetContentType(Guid key);
        IPublishedContentType GetContentType(int id);
        IPublishedContentType GetContentType(string alias);
    }
}
