using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Cms.Infrastructure.PublishedCache.DataSource
{
    public struct ContentNodeKitMsgPackModel
    {
        public ContentNodeKitMsgPackModel(ContentNodeMsgPackModel? node, ContentDataMsgPackModel? draftData, ContentDataMsgPackModel? publishedData)
        {
            Node = node;
            DraftData = draftData;
            PublishedData = publishedData;
        }

        public ContentNodeMsgPackModel? Node { get; }
        public ContentDataMsgPackModel? DraftData { get; }
        public ContentDataMsgPackModel? PublishedData { get; }
    }

    public struct ContentDataPropertiesMsgPackModel
    {
        public ContentDataPropertiesMsgPackModel(Dictionary<string, PropertyDataMsgPackModel[]> propertyDatas)
        {
            PropertyDatas = propertyDatas;
        }

        public Dictionary<string, PropertyDataMsgPackModel[]> PropertyDatas { get; }
    }
    public struct PropertyDataMsgPackModel
    {
        public PropertyDataMsgPackModel(string? culture, string? segment, object? value)
        {
            Culture = culture;
            Segment = segment;
            Value = value;
        }

        public string? Culture { get; }
        public string? Segment { get; }
        public object? Value { get; }
    }

    public struct ContentDataCultureVariationMsgPackModel
    {
        public ContentDataCultureVariationMsgPackModel(string name, string urlSegment, DateTime date, bool isDraft)
        {
            Name = name;
            UrlSegment = urlSegment;
            Date = date;
            IsDraft = isDraft;
        }

        public string Name { get; }
        public string UrlSegment { get; }
        public DateTime Date { get; }
        public bool IsDraft { get; }
    }
    public struct ContentDataCultureInfosMsgPackModel
    {
        public ContentDataCultureInfosMsgPackModel(Dictionary<string, ContentDataCultureVariationMsgPackModel> variations)
        {
            Variations = variations;
        }

        public Dictionary<string, ContentDataCultureVariationMsgPackModel> Variations { get; }
    }

    public struct ContentDataMsgPackModel
    {
        public ContentDataMsgPackModel(bool published, string name, string urlSegment, int versionId, DateTime versionDate, int writerId, int templateId, ContentDataPropertiesMsgPackModel properties, ContentDataCultureInfosMsgPackModel cultureInfos)
        {
            Published = published;
            Name = name;
            UrlSegment = urlSegment;
            VersionId = versionId;
            VersionDate = versionDate;
            WriterId = writerId;
            TemplateId = templateId;
            Properties = properties;
            CultureInfos = cultureInfos;
        }

        public bool Published { get; }
        public string Name { get; }
        public string UrlSegment { get; }
        public int VersionId { get; }
        public DateTime VersionDate { get; }
        public int WriterId { get; }
        public int TemplateId { get; }
        public ContentDataPropertiesMsgPackModel Properties { get; }
        public ContentDataCultureInfosMsgPackModel CultureInfos { get; }
    }

    public struct ContentNodeMsgPackModel
    {
        public ContentNodeMsgPackModel(int id, Guid uid, int level, string path, int sortOrder, int parentContentId,DateTime createDate, int creatorId, int contentTypeId, ContentDataMsgPackModel? draftData, ContentDataMsgPackModel? publishedData)
        {
            Id = id;
            Uid = uid;
            Level = level;
            Path = path;
            SortOrder = sortOrder;
            ParentContentId = parentContentId;
            CreateDate = createDate;
            CreatorId = creatorId;
            ContentTypeId = contentTypeId;
            DraftData = draftData;
            PublishedData = publishedData;
        }

        public int Id { get; }
        public Guid Uid { get; }
        public int Level { get; }
        public string Path { get; }
        public int SortOrder { get; }
        public int ParentContentId { get; }
        public DateTime CreateDate { get; }
        public int CreatorId { get; }
        public int ContentTypeId { get; }
        public ContentDataMsgPackModel? DraftData { get; }
        public ContentDataMsgPackModel? PublishedData { get; }
    }
}
