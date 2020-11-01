using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Scoping;
using static Umbraco.Web.PublishedCache.NuCache.ContentStore;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public interface IContentStore
    {
        int Count { get; }
        long GenCount { get; }
        ISnapshot LiveSnapshot { get; }
        long SnapCount { get; }

        bool ClearLocked(int id);
        Task CollectAsync();
        ISnapshot CreateSnapshot();
        IContentNode Get(Guid uid, long gen);
        IContentNode Get(int id, long gen);
        IEnumerable<IContentNode> GetAll(long gen);
        IEnumerable<IContentNode> GetAtRoot(long gen);
        IPublishedContentType GetContentType(Guid key, long gen);
        IPublishedContentType GetContentType(int id, long gen);
        IPublishedContentType GetContentType(string alias, long gen);
        IDisposable GetScopedWriteLock(IScopeProvider scopeProvider);
        bool IsEmpty(long gen);
        void NewContentTypesLocked(IEnumerable<IPublishedContentType> types);
        void ReleaseLocalDb();
        void SetAllContentTypesLocked(IEnumerable<IPublishedContentType> types);
        bool SetAllFastSortedLocked(IEnumerable<IContentNodeKit> kits, bool fromDb);
        bool SetAllLocked(IEnumerable<IContentNodeKit> kits);
        bool SetBranchLocked(int rootContentId, IEnumerable<IContentNodeKit> kits);
        bool SetLocked(IContentNodeKit kit);
        void UpdateContentTypesLocked(IEnumerable<IPublishedContentType> types);
        void UpdateContentTypesLocked(IReadOnlyCollection<int> removedIds, IReadOnlyCollection<IPublishedContentType> refreshedTypes, IReadOnlyCollection<IContentNodeKit> kits);
        void UpdateDataTypesLocked(IEnumerable<int> dataTypeIds, Func<int, IPublishedContentType> getContentType);

        void Lock(WriteLockInfo lockInfo, bool forceGen = false);

        void Release(WriteLockInfo lockInfo, bool commit = true);

        ITestHelper Test { get; }
    }
}
