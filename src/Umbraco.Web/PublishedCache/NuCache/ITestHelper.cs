namespace Umbraco.Web.PublishedCache.NuCache
{
    public interface ITestHelper
    {
        bool CollectAuto { get; set; }
        long FloorGen { get; }
        long LiveGen { get; }
        bool NextGen { get; }

        (long gen, IContentNode contentNode)[] GetValues(int id);
    }
}
