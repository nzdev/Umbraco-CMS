using CSharpTest.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web.PublishedCache.NuCache.DataSource;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public class BPlusTreeLazySerializers : IBplusTreeLazySerializers
    {
        public BPlusTreeLazySerializers(ISerializer<IContentData> contentDataSerializer,
            IDictionaryOfPropertyDataSerializer dictionaryOfPropertyDataSerializer,
            IRoutingProperties routingPropertySelector)
        {
            RoutingPropertiesOnlySerializer = new LazyContentNodeKitSerializer(contentDataSerializer, dictionaryOfPropertyDataSerializer, routingPropertySelector,
                ContentNodeKitLoadState.RoutingPropertiesLoaded);
            RoutingAndDraftPropertiesOnlySerializer = new LazyContentNodeKitSerializer(contentDataSerializer, dictionaryOfPropertyDataSerializer, routingPropertySelector,
                ContentNodeKitLoadState.RoutingPropertiesLoaded | ContentNodeKitLoadState.AllDraftPropertiesLoaded);
            RoutingPropertiesOnlySerializer = new LazyContentNodeKitSerializer(contentDataSerializer, dictionaryOfPropertyDataSerializer, routingPropertySelector,
                ContentNodeKitLoadState.RoutingPropertiesLoaded | ContentNodeKitLoadState.AllPublishedPropertiesLoaded);
            ContentDataSerializer = new LazyContentNodeKitSerializer(contentDataSerializer, dictionaryOfPropertyDataSerializer, routingPropertySelector,
                ContentNodeKitLoadState.All);
        }

        /// <summary>
        /// Deserialises Routing Properties Only
        /// </summary>
        public ISerializer<IContentNodeKit> RoutingPropertiesOnlySerializer { get;  private set; }

        /// <summary>
        /// Deserialises Routing and Draft Properties Only
        /// </summary>
        public ISerializer<IContentNodeKit> RoutingAndDraftPropertiesOnlySerializer { get; private set; }

        /// <summary>
        /// Deserialises Routing and Published Properties Only
        /// </summary>
        public ISerializer<IContentNodeKit> RoutingAndPublishedPropertiesOnlySerializer { get; private set; }

        /// <summary>
        /// Serializer and Deserializer.
        /// </summary>
        public ISerializer<IContentNodeKit> ContentDataSerializer { get; private set; }
    }
}
