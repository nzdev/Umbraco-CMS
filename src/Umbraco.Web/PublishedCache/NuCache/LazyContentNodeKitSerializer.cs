using CSharpTest.Net.Serialization;
using System.IO;
using System.Linq;
using Umbraco.Web.PublishedCache.NuCache.DataSource;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public class LazyContentNodeKitSerializer : ISerializer<IContentNodeKit>
    {
        public LazyContentNodeKitSerializer(ISerializer<IContentData> contentDataSerializer,
            IDictionaryOfPropertyDataSerializer dictionaryOfPropertyDataSerializer,
            IRoutingProperties routingPropertySelector, ContentNodeKitLoadState contentNodeKitLoadState)
        {
            _contentDataSerializer = contentDataSerializer;
            _dictionaryOfPropertyDataSerializer = dictionaryOfPropertyDataSerializer;
            _routingPropertySelector = routingPropertySelector;
            _contentNodeKitLoadState = contentNodeKitLoadState;
        }
        private readonly ISerializer<IContentData> _contentDataSerializer;
        private readonly IDictionaryOfPropertyDataSerializer _dictionaryOfPropertyDataSerializer;
        private readonly IRoutingProperties _routingPropertySelector;
        private readonly ContentNodeKitLoadState _contentNodeKitLoadState;

        public IContentNodeKit ReadFrom(Stream stream)
        {
            var kit = new ContentNodeKit
            {
                Node = new ContentNode(
                    PrimitiveSerializer.Int32.ReadFrom(stream), // id
                    PrimitiveSerializer.Guid.ReadFrom(stream), // uid
                    PrimitiveSerializer.Int32.ReadFrom(stream), // level
                    PrimitiveSerializer.String.ReadFrom(stream), // path
                    PrimitiveSerializer.Int32.ReadFrom(stream), // sort order
                    PrimitiveSerializer.Int32.ReadFrom(stream), // parent id
                    PrimitiveSerializer.DateTime.ReadFrom(stream), // date created
                    PrimitiveSerializer.Int32.ReadFrom(stream) // creator id
                ),
                ContentTypeId = PrimitiveSerializer.Int32.ReadFrom(stream)
            };

            var hasPublished = PrimitiveSerializer.Boolean.ReadFrom(stream);
            if (hasPublished)
                kit.PublishedData = _contentDataSerializer.ReadFrom(stream);
            var hasDraft = PrimitiveSerializer.Boolean.ReadFrom(stream);
            if (hasDraft)
                kit.DraftData = _contentDataSerializer.ReadFrom(stream);

            var hasAdditionalPublished = PrimitiveSerializer.Boolean.ReadFrom(stream);
            if (hasAdditionalPublished &&
                _contentNodeKitLoadState == ContentNodeKitLoadState.All || _contentNodeKitLoadState == ContentNodeKitLoadState.AllPublishedPropertiesLoaded
                || _contentNodeKitLoadState == ContentNodeKitLoadState.AllDraftPropertiesLoaded)
            {
                //Load remaining published properties
                var additionalPublishedProperties = _dictionaryOfPropertyDataSerializer.ReadFrom(stream);
                foreach (var item in additionalPublishedProperties)
                {
                    kit.PublishedData.Properties.Add(item.Key, item.Value);
                }
            }
            var hasAdditionalDraft = PrimitiveSerializer.Boolean.ReadFrom(stream);
            if (hasAdditionalDraft &&
                _contentNodeKitLoadState == ContentNodeKitLoadState.All || _contentNodeKitLoadState == ContentNodeKitLoadState.AllDraftPropertiesLoaded)
            {
                //Load remaining draft properties
                var additionalDraftProperties = _dictionaryOfPropertyDataSerializer.ReadFrom(stream);
                foreach (var item in additionalDraftProperties)
                {
                    kit.DraftData.Properties.Add(item.Key, item.Value);
                }
            }
            //Set state
            kit.LoadState = ContentNodeKitLoadState.RoutingPropertiesLoaded;
            if (!hasAdditionalPublished &&
                _contentNodeKitLoadState == ContentNodeKitLoadState.All || _contentNodeKitLoadState == ContentNodeKitLoadState.AllPublishedPropertiesLoaded)
            {
                kit.LoadState = kit.LoadState | ContentNodeKitLoadState.AllPublishedPropertiesLoaded;
            }
            if (!hasAdditionalDraft || hasAdditionalDraft &&
                _contentNodeKitLoadState == ContentNodeKitLoadState.All || _contentNodeKitLoadState == ContentNodeKitLoadState.AllDraftPropertiesLoaded)
            {
                kit.LoadState = kit.LoadState | ContentNodeKitLoadState.AllDraftPropertiesLoaded;
            }

            return kit;
        }

        public void WriteTo(IContentNodeKit value, Stream stream)
        {
            PrimitiveSerializer.Int32.WriteTo(value.Node.Id, stream);
            PrimitiveSerializer.Guid.WriteTo(value.Node.Uid, stream);
            PrimitiveSerializer.Int32.WriteTo(value.Node.Level, stream);
            PrimitiveSerializer.String.WriteTo(value.Node.Path, stream);
            PrimitiveSerializer.Int32.WriteTo(value.Node.SortOrder, stream);
            PrimitiveSerializer.Int32.WriteTo(value.Node.ParentContentId, stream);
            PrimitiveSerializer.DateTime.WriteTo(value.Node.CreateDate, stream);
            PrimitiveSerializer.Int32.WriteTo(value.Node.CreatorId, stream);
            PrimitiveSerializer.Int32.WriteTo(value.ContentTypeId, stream);

            if (value.PublishedData != null)
            {
                PrimitiveSerializer.Boolean.WriteTo(value.PublishedData != null, stream);//Routing published data
                IContentData publishedRoutingData = new ContentData()
                {
                    Name = value.PublishedData.Name,
                    Published = value.PublishedData.Published,
                    TemplateId = value.PublishedData.TemplateId,
                    UrlSegment = value.PublishedData.UrlSegment,
                    VersionDate = value.PublishedData.VersionDate,
                    VersionId = value.PublishedData.VersionId,
                    WriterId = value.PublishedData.WriterId,
                    Properties = value.PublishedData.Properties.Where(x => _routingPropertySelector.EagerLoadProperties.ContainsKey(x.Key)).ToDictionary(x => x.Key, x => x.Value),
                    CultureInfos = value.PublishedData.CultureInfos
                };
                _contentDataSerializer.WriteTo(publishedRoutingData, stream);
            }
            if (value.DraftData != null) //routing draft data
            {
                PrimitiveSerializer.Boolean.WriteTo(value.PublishedData != null, stream);
                IContentData draftRoutingData = new ContentData()
                {
                    Name = value.DraftData.Name,
                    Published = value.DraftData.Published,
                    TemplateId = value.DraftData.TemplateId,
                    UrlSegment = value.DraftData.UrlSegment,
                    VersionDate = value.DraftData.VersionDate,
                    VersionId = value.DraftData.VersionId,
                    WriterId = value.DraftData.WriterId,
                    Properties = value.DraftData.Properties.Where(x => _routingPropertySelector.EagerLoadProperties.ContainsKey(x.Key)).ToDictionary(x => x.Key, x => x.Value),
                    CultureInfos = value.DraftData.CultureInfos
                };
                _contentDataSerializer.WriteTo(draftRoutingData, stream);
            }

            //Remaining published data
            var remainingPublishedData = value.PublishedData?.Properties?.Where(x => _routingPropertySelector.EagerLoadProperties.ContainsKey(x.Key))?.ToDictionary(x => x.Key, x => x.Value);
            PrimitiveSerializer.Boolean.WriteTo(remainingPublishedData != null, stream);
            if (value.PublishedData != null)
            {
                _dictionaryOfPropertyDataSerializer.WriteTo(remainingPublishedData, stream);
            }
            var remainingDraftData = value.PublishedData?.Properties?.Where(x => _routingPropertySelector.EagerLoadProperties.ContainsKey(x.Key))?.ToDictionary(x => x.Key, x => x.Value);
            PrimitiveSerializer.Boolean.WriteTo(remainingDraftData != null, stream);
            if (value.DraftData != null)
            {
                _dictionaryOfPropertyDataSerializer.WriteTo(remainingDraftData, stream);
            }
        }
    }
}
