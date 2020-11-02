

using CSharpTest.Net.Serialization;
using Umbraco.Web.PublishedCache.NuCache;

namespace Umbraco.Web.PublishedCache
{
    public interface IBplusTreeLazySerializers
    {
        ISerializer<IContentNodeKit> RoutingPropertiesOnlySerializer { get; }
        ISerializer<IContentNodeKit> RoutingAndDraftPropertiesOnlySerializer { get; }
        ISerializer<IContentNodeKit> RoutingAndPublishedPropertiesOnlySerializer { get; }
        ISerializer<IContentNodeKit> ContentDataSerializer { get; }
    }
}
