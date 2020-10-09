using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Configuration;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedCache;
using Umbraco.Web.PublishedCache.NuCache;

namespace Umbraco.PublishedCache.NuCache.LiteDb.Routing
{
    public class LiteDbContentCacheContentRouter : ContentCacheContentRouter
    {
        private readonly LiteDbContentSettings _liteDbSettings;

        private LiteDatabase _db;
        private const int rootLevel = 1;

        public LiteDbContentCacheContentRouter(IGlobalSettings globalSettings, LiteDbContentSettings liteDbSettings) : base(globalSettings)
        {
            _liteDbSettings = liteDbSettings;
            _db = new LiteDatabase(_liteDbSettings.ConnectionString, BsonMapper.Global);
            _db.GetCollection<ContentNodeKit>(_liteDbSettings.CollectionName);
        }
        protected override IPublishedContent HideTopLevel(IPublishedSnapshot snapshot, bool preview, string culture, string[] parts)
        {
            // Get a collection (or create, if doesn't exist)
            var col = _db.GetCollection<ContentNodeKit>(_liteDbSettings.CollectionName);

            // Use LINQ to query documents (filter, sort, transform)
            var result = col.Query()
                .Where(x => AllowPreview(x, preview) && IsRootNode(x) && UrlSegment(x, culture) == parts[0])
                .Select(x => x.Key)
                .FirstOrDefault();

            return snapshot.Content.GetById(result);
        }

        private static bool IsRootNode(ContentNodeKit x)
        {
            return x.Node.Level == rootLevel;
        }

        private static bool AllowPreview(ContentNodeKit kit, bool preview)
        {
            return (kit.PublishedData != null || preview);
        }

        private string UrlSegment(ContentNodeKit kit, string culture)
        {
            return kit.Node.PublishedModel.UrlSegment;//TODO UrlSegment(culture)
        }

        protected override IPublishedContent NotInDomain(IPublishedSnapshot snapshot, bool preview, bool? hideTopLevelNode, string culture, string[] parts)
        {
            // Get a collection (or create, if doesn't exist)
            var col = _db.GetCollection<ContentNodeKit>(_liteDbSettings.CollectionName);

            // Use LINQ to query documents (filter, sort, transform)
            if (hideTopLevelNode.Value)
            {
                //First child of a root node for the given culture with the url segment of the given culture
                var result = col.Query()
                .Where(x => AllowPreview(x, preview) && IsChildOfAnyRootNode(x, culture) && UrlSegment(x, culture) == parts[0])
                .Select(x => x.Key)
                .FirstOrDefault();

                return snapshot.Content.GetById(result);
            }
            else
            {
                var result = col.Query()
                .Where(x => AllowPreview(x, preview) && IsRootNode(x) && UrlSegment(x, culture) == parts[0])
                .Select(x => x.Key)
                .FirstOrDefault();

                return snapshot.Content.GetById(result);
            }
        }

        private static bool IsChildOfAnyRootNode(ContentNodeKit x,string culture)
        {
            return x.Node.Level == rootLevel + 1; //TODO x.Children(culture)
        }

        public override IPublishedContent FollowRoute(IPublishedSnapshot snapshot, IPublishedContent content, IReadOnlyList<string> parts, int start, string culture)
        {
            return base.FollowRoute(snapshot, content, parts, start, culture);
        }
        protected override IPublishedContent FindContentByAlias(IPublishedContentCache publishedCache, int rootNodeId, string culture, string alias, bool preview)
        {
            return base.FindContentByAlias(publishedCache, rootNodeId, culture, alias, preview);
        }

        protected override RouteByIdResult GetRouteByIdInternal(IPublishedCache2 publishedCache, IDomainCache domainCache, bool preview, int contentId, bool? hideTopLevelNode, string culture)
        {
            return base.GetRouteByIdInternal(publishedCache, domainCache, preview, contentId, hideTopLevelNode, culture);
        }

        public void Dispose()
        {
            ((IDisposable)_db).Dispose();
        }
    }
}
