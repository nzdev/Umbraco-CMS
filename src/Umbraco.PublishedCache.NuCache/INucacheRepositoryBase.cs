using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Cms.Infrastructure.PublishedCache
{
    /// <summary>
    /// Repository For Nucache
    /// </summary>
    /// <typeparam name="TKey">Key Type</typeparam>
    /// <typeparam name="TValue">Value Type</typeparam>
    ///  /// <remarks>Ensure the repository is responsible for queries. The underlying storage may support more efficent queries</remarks>
    public interface INucacheRepositoryBase<TKey, TValue> : IKeyValueStore<TKey, TValue>
    {
        /// <summary>
        /// Get All by level + parentId + sortOrder
        /// </summary>
        /// <returns></returns>
        ICollection<TValue> GetAllSortedByLevelParentIdSortOrderDesc();
    }
}
