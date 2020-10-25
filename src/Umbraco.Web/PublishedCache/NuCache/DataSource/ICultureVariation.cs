using System;

namespace Umbraco.Web.PublishedCache.NuCache.DataSource
{
    public interface ICultureVariation
    {
        DateTime Date { get; set; }
        bool IsDraft { get; set; }
        string Name { get; set; }
        string UrlSegment { get; set; }
    }
}