using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Umbraco.Cms.Infrastructure.PublishedCache.DataSource
{
    [MessagePackObject]
    public struct ContentNodeKitMsgPackModel
    {
        public ContentNodeKitMsgPackModel(ContentNodeMsgPackModel? node, ContentDataMsgPackModel? draftData, ContentDataMsgPackModel? publishedData)
        {
            Node = node;
            DraftData = draftData;
            PublishedData = publishedData;
        }
        [Key(1)]
        public ContentNodeMsgPackModel? Node { get; }
        [Key(2)]
        public ContentDataMsgPackModel? DraftData { get; }
        [Key(3)]
        public ContentDataMsgPackModel? PublishedData { get; }
    }

    [MessagePackObject]
    public struct ContentDataPropertiesMsgPackModel
    {
        public ContentDataPropertiesMsgPackModel(Dictionary<string, PropertyDataMsgPackModel[]> propertyDatas)
        {
            PropertyDatas = propertyDatas;
        }
        [Key(0)]
        public Dictionary<string, PropertyDataMsgPackModel[]> PropertyDatas { get; }
    }
    [MessagePackObject]
    public struct PropertyDataMsgPackModel
    {
        public PropertyDataMsgPackModel(string? culture, string? segment, object? value)
        {
            Culture = culture;
            Segment = segment;
            Value = value;
        }

        [Key(0)]
        public string? Culture { get; }
        [Key(1)]
        public string? Segment { get; }
        [Key(2)]
        public object? Value { get; }
    }

    [MessagePackObject]
    public struct ContentDataCultureVariationMsgPackModel
    {
        public ContentDataCultureVariationMsgPackModel(string name, string urlSegment, DateTime date, bool isDraft)
        {
            Name = name;
            UrlSegment = urlSegment;
            Date = date;
            IsDraft = isDraft;
        }

        [Key(0)]
        public string Name { get; }
        [Key(1)]
        public string UrlSegment { get; }
        [Key(2)]
        public DateTime Date { get; }
        [Key(3)]
        public bool IsDraft { get; }
    }
    [MessagePackObject]
    public struct ContentDataCultureInfosMsgPackModel
    {
        public ContentDataCultureInfosMsgPackModel(Dictionary<string, ContentDataCultureVariationMsgPackModel> variations)
        {
            Variations = variations;
        }

        [Key(0)]
        public Dictionary<string, ContentDataCultureVariationMsgPackModel> Variations { get; }
    }

    [MessagePackObject]
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

        [Key(0)]
        public bool Published { get; }
        [Key(1)]
        public string Name { get; }
        [Key(2)]
        public string UrlSegment { get; }
        [Key(3)]
        public int VersionId { get; }
        [Key(4)]
        public DateTime VersionDate { get; }
        [Key(5)]
        public int WriterId { get; }
        [Key(6)]
        public int TemplateId { get; }
        [Key(7)]
        public ContentDataPropertiesMsgPackModel Properties { get; }
        [Key(8)]
        public ContentDataCultureInfosMsgPackModel CultureInfos { get; }
    }

    [MessagePackObject]
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

        [Key(0)]
        public int Id { get; }
        [Key(1)]
        public Guid Uid { get; }
        [Key(2)]
        public int Level { get; }
        [Key(3)]
        public string Path { get; }
        [Key(4)]
        public int SortOrder { get; }
        [Key(5)]
        public int ParentContentId { get; }
        [Key(6)]
        public DateTime CreateDate { get; }
        [Key(7)]
        public int CreatorId { get; }
        [Key(8)]
        public int ContentTypeId { get; }
        [Key(9)]
        public ContentDataMsgPackModel? DraftData { get; }
        [Key(10)]
        public ContentDataMsgPackModel? PublishedData { get; }
    }
}
