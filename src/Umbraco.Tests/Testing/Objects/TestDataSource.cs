using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Scoping;
using Umbraco.Web;
using Umbraco.Web.PublishedCache.NuCache;
using Umbraco.Web.PublishedCache.NuCache.DataSource;

namespace Umbraco.Tests.Testing.Objects
{

    internal class TestDataSource : IDataSource
    {
        public TestDataSource(params IContentNodeKit[] kits)
            : this((IEnumerable<IContentNodeKit>) kits)
        { }

        public TestDataSource(IEnumerable<IContentNodeKit> kits)
        {
            Kits = kits.ToDictionary(x => x.Node.Id, x => x);
        }

        public Dictionary<int, IContentNodeKit> Kits { get; }

        // note: it is important to clone the returned kits, as the inner
        // ContentNode is directly reused and modified by the snapshot service

        public IContentNodeKit GetContentSource(IScope scope, int id)
            => Kits.TryGetValue(id, out var kit) ? kit.Clone() : default;

        public IEnumerable<IContentNodeKit> GetAllContentSources(IScope scope)
            => Kits.Values
                .OrderBy(x => x.Node.Level)
                .ThenBy(x => x.Node.ParentContentId)
                .ThenBy(x => x.Node.SortOrder)
                .Select(x => x.Clone());

        public IEnumerable<IContentNodeKit> GetBranchContentSources(IScope scope, int id)
            => Kits.Values
                .Where(x => x.Node.Path.EndsWith("," + id) || x.Node.Path.Contains("," + id + ","))
                .OrderBy(x => x.Node.Level)
                .ThenBy(x => x.Node.ParentContentId)
                .ThenBy(x => x.Node.SortOrder)
                .Select(x => x.Clone());

        public IEnumerable<IContentNodeKit> GetTypeContentSources(IScope scope, IEnumerable<int> ids)
            => Kits.Values
                .Where(x => ids.Contains(x.ContentTypeId))
                .OrderBy(x => x.Node.Level)
                .ThenBy(x => x.Node.ParentContentId)
                .ThenBy(x => x.Node.SortOrder)
                .Select(x => x.Clone());

        public IContentNodeKit GetMediaSource(IScope scope, int id)
        {
            return default;
        }

        public IEnumerable<IContentNodeKit> GetAllMediaSources(IScope scope)
        {
            return Enumerable.Empty<IContentNodeKit>();
        }

        public IEnumerable<IContentNodeKit> GetBranchMediaSources(IScope scope, int id)
        {
            return Enumerable.Empty<IContentNodeKit>();
        }

        public IEnumerable<IContentNodeKit> GetTypeMediaSources(IScope scope, IEnumerable<int> ids)
        {
            return Enumerable.Empty<IContentNodeKit>();
        }
    }
}
