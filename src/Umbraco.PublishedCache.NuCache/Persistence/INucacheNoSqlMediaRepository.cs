using System;
using System.Collections.Generic;
using System.Text;

namespace Umbraco.Cms.Infrastructure.PublishedCache.Persistence
{
    /// <summary>
    /// Nucache repository for media.
    /// </summary>
    public interface INucacheNoSqlMediaRepository : INucacheNoSqlRepositoryBase<int, ContentNodeKit>
    {
        /// <summary>
        /// Get All by level, parentid, sortorder
        /// </summary>
        /// <returns></returns>
        ICollection<ContentNodeKit> GetAllSorted();
    }
}
