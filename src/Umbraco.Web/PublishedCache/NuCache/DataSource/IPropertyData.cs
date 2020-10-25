namespace Umbraco.Web.PublishedCache.NuCache.DataSource
{
    public interface IPropertyData
    {
        string Culture { get; set; }
        string Segment { get; set; }
        object Value { get; set; }
    }
}