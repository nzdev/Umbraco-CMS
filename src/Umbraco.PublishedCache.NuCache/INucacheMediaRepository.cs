using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Cms.Infrastructure.PublishedCache
{
    /// <summary>
    /// Nucache repository for media.
    /// </summary>
    public interface INucacheMediaRepository : INucacheRepositoryBase<int, ContentNodeKit>
    {
        /// <summary>
        /// Get All by level, parentid, sortorder
        /// </summary>
        /// <returns></returns>
        ICollection<ContentNodeKit> GetAllSortedByLevelParentIdSortOrderDesc();
    }
}
