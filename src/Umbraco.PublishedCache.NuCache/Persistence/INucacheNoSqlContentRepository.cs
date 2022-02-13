using System;
using System.Collections.Generic;
using System.Text;

namespace Umbraco.Cms.Infrastructure.PublishedCache.Persistence
{
    /// <summary>
    /// Nucache repository for documents.
    /// </summary>
    /// <remarks>Ensure the repository is responsible for queries. The underlying storage may support more efficent queries</remarks>
    public interface INucacheNoSqlContentRepository : INucacheNoSqlRepositoryBase<int, ContentNodeKit>
    {
        /// <summary>
        /// Get All by level, parentid, sortorder
        /// </summary>
        /// <returns></returns>
        ICollection<ContentNodeKit> GetAllSorted();
    }
}
