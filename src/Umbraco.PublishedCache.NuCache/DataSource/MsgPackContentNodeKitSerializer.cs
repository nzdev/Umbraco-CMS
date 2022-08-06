using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpTest.Net.Serialization;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.IO;
using System.Buffers;
using MessagePack.Formatters;
using Umbraco.Cms.Infrastructure.PublishedCache.MsgPack;

namespace Umbraco.Cms.Infrastructure.PublishedCache.DataSource
{
    internal class MsgPackContentNodeKitSerializer : ISerializer<ContentNodeKit>
    {
        private readonly MessagePackSerializerOptions _options;
        private static readonly RecyclableMemoryStreamManager manager = new RecyclableMemoryStreamManager();

        public MsgPackContentNodeKitSerializer()
        {
            MessagePackSerializerOptions? defaultOptions = StandardResolver.Options;
            IFormatterResolver? resolver = CompositeResolver.Create(
                 new IMessagePackFormatter[] { new StringInterningFormatter() },
                 new IFormatterResolver[] { defaultOptions.Resolver });

            _options = defaultOptions
                .WithResolver(resolver)
                .WithCompression(MessagePackCompression.Lz4BlockArray)
                .WithSecurity(MessagePackSecurity.UntrustedData);
        }

        public ContentNodeKit ReadFrom(Stream stream)
        {
            var length = PrimitiveSerializer.Int64.ReadFrom(stream);
            var buffer = ArrayPool<byte>.Shared.Rent((int)length);
            Span<byte> bufferSpan = new Span<byte>(buffer).Slice(0, (int)length);
            stream.Read(bufferSpan);
            ContentNodeMsgPackModel model;
            using (var bufferStream = manager.GetStream())
            {
                bufferStream.Write(bufferSpan);
                bufferStream.Position = 0;
                model = MessagePackSerializer.Deserialize<ContentNodeMsgPackModel>(bufferStream, _options);

            }
            ArrayPool<byte>.Shared.Return(buffer);
            var contentNode = new ContentNode(model.Id, model.Uid, model.Level, model.Path ?? "", model.SortOrder, model.ParentContentId, model.CreateDate, model.CreatorId);


            var contentTypeId = model.ContentTypeId;
            ContentData? draftData = null;
            ContentData? publishedData = null;
            if (model.DraftData is not null)
            {
                var draft = model.DraftData.Value;

                Dictionary<string, PropertyData[]> propDatas = new Dictionary<string, PropertyData[]>(draft.Properties.PropertyDatas.Count);
                foreach (var propData in draft.Properties.PropertyDatas)
                {
                    var propVals = propData.Value.Length == 0 ? Array.Empty<PropertyData>() : new PropertyData[propData.Value.Length];
                    for (int i = 0; i < propData.Value.Length; i++)
                    {
                        propVals[i] = new PropertyData
                        {
                            Value = propData.Value[i].Value,
                            Culture = propData.Value[i].Culture,
                            Segment = propData.Value[i].Segment
                        };
                    }
                    propDatas.Add(propData.Key, propVals);
                }
                Dictionary<string, CultureVariation> variations = new Dictionary<string, CultureVariation>(0); //TODO
                draftData = new ContentData(draft.Name, draft.UrlSegment, draft.VersionId, draft.VersionDate, draft.WriterId, draft.TemplateId, draft.Published, propDatas, variations); //TODO
            }

            if (model.PublishedData is not null)
            {
                var pubData = model.PublishedData.Value;

                Dictionary<string, PropertyData[]> propDatas = new Dictionary<string, PropertyData[]>(pubData.Properties.PropertyDatas.Count);
                foreach (var propData in pubData.Properties.PropertyDatas)
                {
                    var propVals = propData.Value.Length == 0 ? Array.Empty<PropertyData>() : new PropertyData[propData.Value.Length];
                    for (int i = 0; i < propData.Value.Length; i++)
                    {
                        propVals[i] = new PropertyData
                        {
                            Value = propData.Value[i].Value,
                            Culture = propData.Value[i].Culture,
                            Segment = propData.Value[i].Segment
                        };
                    }
                    propDatas.Add(propData.Key, propVals);
                }
                Dictionary<string, CultureVariation> variations = new Dictionary<string, CultureVariation>(0); //TODO
                publishedData = new ContentData(pubData.Name, pubData.UrlSegment, pubData.VersionId, pubData.VersionDate, pubData.WriterId, pubData.TemplateId, pubData.Published, propDatas, variations); //TODO
            }

            var kit = new ContentNodeKit(
                contentNode,
                contentTypeId,
                draftData,
                publishedData);

            return kit;
        }

        public void WriteTo(ContentNodeKit value, Stream stream)
        {
            ContentDataMsgPackModel? draftData = null;
            if (value.DraftData != null)
            {
                Dictionary<string, PropertyDataMsgPackModel[]> propertyDatas = new Dictionary<string, PropertyDataMsgPackModel[]>(value.DraftData.Properties.Count);
                foreach (var item in value.DraftData.Properties)
                {
                    var pdata = item.Value.Length == 0 ? Array.Empty<PropertyDataMsgPackModel>() : new PropertyDataMsgPackModel[item.Value.Length];
                    for (int i = 0; i < item.Value.Length; i++)
                    {
                        pdata[i] = new PropertyDataMsgPackModel(item.Value[i].Culture, item.Value[i].Segment, item.Value[i].Value);
                    }
                    propertyDatas.Add(item.Key, pdata);
                }
                ContentDataPropertiesMsgPackModel props = new ContentDataPropertiesMsgPackModel(propertyDatas);
                ContentDataCultureInfosMsgPackModel cultures;
                if (value.DraftData.CultureInfos is not null)
                {
                    Dictionary<string, ContentDataCultureVariationMsgPackModel> cultureDatas = new Dictionary<string, ContentDataCultureVariationMsgPackModel>(value.DraftData.Properties.Count);
                    foreach (var item in value.DraftData.CultureInfos)
                    {
                        if (item.Value.Name is not null && item.Value.UrlSegment is not null)
                        {
                            cultureDatas.Add(item.Key, new ContentDataCultureVariationMsgPackModel(item.Value.Name, item.Value.UrlSegment, item.Value.Date, item.Value.IsDraft));
                        }
                    }
                    cultures = new ContentDataCultureInfosMsgPackModel();
                }
                else
                {
                    cultures = new ContentDataCultureInfosMsgPackModel();
                }
                draftData = new ContentDataMsgPackModel(value.DraftData.Published, value.DraftData.Name, value.DraftData.UrlSegment ?? "", value.DraftData.VersionId, value.DraftData.VersionDate, value.DraftData.WriterId, value.DraftData.TemplateId ?? 0, props, cultures);
            }

            ContentDataMsgPackModel? publishData = null;
            if (value.PublishedData != null)
            {
                Dictionary<string, PropertyDataMsgPackModel[]> propertyDatas = new Dictionary<string, PropertyDataMsgPackModel[]>(value.PublishedData.Properties.Count);
                foreach (var item in value.PublishedData.Properties)
                {
                    var pdata = item.Value.Length == 0 ? Array.Empty<PropertyDataMsgPackModel>() : new PropertyDataMsgPackModel[item.Value.Length];
                    for (int i = 0; i < item.Value.Length; i++)
                    {
                        pdata[i] = new PropertyDataMsgPackModel(item.Value[i].Culture, item.Value[i].Segment, item.Value[i].Value);
                    }
                    propertyDatas.Add(item.Key, pdata);
                }
                ContentDataPropertiesMsgPackModel props = new ContentDataPropertiesMsgPackModel(propertyDatas);
                ContentDataCultureInfosMsgPackModel cultures;
                if (value.PublishedData.CultureInfos is not null)
                {
                    Dictionary<string, ContentDataCultureVariationMsgPackModel> cultureDatas = new Dictionary<string, ContentDataCultureVariationMsgPackModel>(value.PublishedData.Properties.Count);
                    foreach (var item in value.PublishedData.CultureInfos)
                    {
                        if (item.Value.Name is not null && item.Value.UrlSegment is not null)
                        {
                            cultureDatas.Add(item.Key, new ContentDataCultureVariationMsgPackModel(item.Value.Name, item.Value.UrlSegment, item.Value.Date, item.Value.IsDraft));
                        }
                    }
                    cultures = new ContentDataCultureInfosMsgPackModel();
                }
                else
                {
                    cultures = new ContentDataCultureInfosMsgPackModel();
                }
                publishData = new ContentDataMsgPackModel(value.PublishedData.Published, value.PublishedData.Name, value.PublishedData.UrlSegment ?? "", value.PublishedData.VersionId, value.PublishedData.VersionDate, value.PublishedData.WriterId, value.PublishedData.TemplateId ?? 0, props, cultures);
            }
            if (value.Node is not null)
            {
                ContentNodeMsgPackModel model = new ContentNodeMsgPackModel(value.Node.Id, value.Node.Uid, value.Node.Level, value.Node.Path, value.Node.SortOrder, value.Node.ParentContentId, value.Node.CreateDate, value.Node.CreatorId, value.ContentTypeId, draftData, publishData);
                using (var bufferStream = manager.GetStream())
                {
                    MessagePackSerializer.Serialize(bufferStream, model, _options);
                    PrimitiveSerializer.Int64.WriteTo(bufferStream.Position, stream);
                    bufferStream.Position = 0;
                    bufferStream.CopyTo(stream);
                }
            }
        }
    }
}
