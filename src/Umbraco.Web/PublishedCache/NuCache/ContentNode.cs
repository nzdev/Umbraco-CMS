using System;
using System.Diagnostics;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedCache.NuCache.DataSource;

namespace Umbraco.Web.PublishedCache.NuCache
{
    // represents a content "node" ie a pair of draft + published versions
    // internal, never exposed, to be accessed from ContentStore (only!)
    [DebuggerDisplay("Id: {Id}, Path: {Path}")]
    public class ContentNode : IContentNode
    {
        // special ctor for root pseudo node
        public ContentNode()
        {
            FirstChildContentId = -1;
            LastChildContentId = -1;
            NextSiblingContentId = -1;
            PreviousSiblingContentId = -1;
        }

        // special ctor with no content data - for members
        public ContentNode(int id, Guid uid, IPublishedContentType contentType,
            int level, string path, int sortOrder,
            int parentContentId,
            DateTime createDate, int creatorId)
            : this()
        {
            _id = id;
            _uid = uid;
            ContentType = contentType;
            _level = level;
            _path = path;
            _sortOrder = sortOrder;
            _parentContentId = parentContentId;
            _createDate = createDate;
            _creatorId = creatorId;
        }

        public ContentNode(int id, Guid uid, IPublishedContentType contentType,
            int level, string path, int sortOrder,
            int parentContentId,
            DateTime createDate, int creatorId,
            IContentData draftData, IContentData publishedData,
            IPublishedSnapshotAccessor publishedSnapshotAccessor,
            IVariationContextAccessor variationContextAccessor)
            : this(id, uid, level, path, sortOrder, parentContentId, createDate, creatorId)
        {
            SetContentTypeAndData(contentType, draftData, publishedData, publishedSnapshotAccessor, variationContextAccessor);
        }

        // 2-phases ctor, phase 1
        public ContentNode(int id, Guid uid,
            int level, string path, int sortOrder,
            int parentContentId,
            DateTime createDate, int creatorId)
        {
            _id = id;
            _uid = uid;
            _level = level;
            _path = path;
            _sortOrder = sortOrder;
            _parentContentId = parentContentId;
            FirstChildContentId = -1;
            LastChildContentId = -1;
            NextSiblingContentId = -1;
            PreviousSiblingContentId = -1;
            _createDate = createDate;
            _creatorId = creatorId;
        }

        // two-phase ctor, phase 2
        public void SetContentTypeAndData(IPublishedContentType contentType, IContentData draftData, IContentData publishedData, IPublishedSnapshotAccessor publishedSnapshotAccessor, IVariationContextAccessor variationContextAccessor)
        {
            ContentType = contentType;

            if (draftData == null && publishedData == null)
                throw new ArgumentException("Both draftData and publishedData cannot be null at the same time.");

            _publishedSnapshotAccessor = publishedSnapshotAccessor;
            _variationContextAccessor = variationContextAccessor;

            _draftData = draftData;
            _publishedData = publishedData;
        }

        public IContentNode Clone(IPublishedContentType contentType = null)
        {
            var origin = this;
            var cn = new ContentNode(origin.Id, origin.Uid, contentType ?? origin.ContentType,
                origin.Level, origin.Path, origin.SortOrder, origin.ParentContentId, origin.CreateDate, origin.CreatorId, origin._draftData,
                origin._publishedData, origin._publishedSnapshotAccessor, _variationContextAccessor);

            cn.FirstChildContentId = origin.FirstChildContentId;
            cn.LastChildContentId = origin.LastChildContentId;
            cn.NextSiblingContentId = origin.NextSiblingContentId;
            cn.PreviousSiblingContentId = origin.PreviousSiblingContentId;
            return cn;
        }
        // clone
       
        // everything that is common to both draft and published versions
        // keep this as small as possible

        private readonly int _id;
        private readonly Guid _uid;
        private readonly int _level;
        private readonly string _path;
        private readonly int _sortOrder;
        private readonly int _parentContentId;

        public int Id => _id;
        public Guid Uid => _uid;
        public IPublishedContentType ContentType { get; set; }
        public int Level => _level;
        public string Path => _path;
        public int SortOrder => _sortOrder;
        public int ParentContentId => _parentContentId;

        // TODO: Can we make everything readonly?? This would make it easier to debug and be less error prone especially for new developers.
        // Once a Node is created and exists in the cache it is readonly so we should be able to make that happen at the API level too.
        public int FirstChildContentId { get; set; }
        public int LastChildContentId { get; set; }
        public int NextSiblingContentId { get; set; }
        public int PreviousSiblingContentId { get; set; }

        public readonly DateTime _createDate;
        public readonly int _creatorId;

        public DateTime CreateDate => _createDate;
        public int CreatorId => _creatorId;

        private IContentData _draftData;
        private IContentData _publishedData;
        private IVariationContextAccessor _variationContextAccessor;
        private IPublishedSnapshotAccessor _publishedSnapshotAccessor;

        public bool HasPublished => _publishedData != null;
        public bool HasPublishedCulture(string culture) => _publishedData != null && _publishedData.CultureInfos.ContainsKey(culture);

        // draft and published version (either can be null, but not both)
        // are models not direct PublishedContent instances
        private IPublishedContent _draftModel;
        private IPublishedContent _publishedModel;

        private IPublishedContent GetModel(ref IPublishedContent model, IContentData contentData)
        {
            if (model != null) return model;
            if (contentData == null) return null;

            // create the model - we want to be fast, so no lock here: we may create
            // more than 1 instance, but the lock below ensures we only ever return
            // 1 unique instance - and locking is a nice explicit way to ensure this

            var m = new PublishedContent(this, contentData, _publishedSnapshotAccessor, _variationContextAccessor).CreateModel();

            // locking 'this' is not a best-practice but ContentNode is internal and
            // we know what we do, so it is fine here and avoids allocating an object
            lock (this)
            {
                return model = model ?? m;
            }
        }

        public IPublishedContent DraftModel => GetModel(ref _draftModel, _draftData);

        public IPublishedContent PublishedModel => GetModel(ref _publishedModel, _publishedData);

        public ContentNodeKit ToKit()
            => new ContentNodeKit
            {
                Node = this,
                ContentTypeId = ContentType.Id,

                DraftData = _draftData,
                PublishedData = _publishedData
            };
    }
}
