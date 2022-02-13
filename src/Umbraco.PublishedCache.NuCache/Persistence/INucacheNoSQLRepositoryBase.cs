using System;
using System.Collections.Generic;
using System.Text;

namespace Umbraco.Cms.Infrastructure.PublishedCache.Persistence
{
    /// <summary>
    /// Repository For Nucache
    /// </summary>
    /// <typeparam name="TKey">Key Type</typeparam>
    /// <typeparam name="TValue">Value Type</typeparam>
    ///  /// <remarks>Ensure the repository is responsible for queries. The underlying storage may support more efficent queries</remarks>
    public interface INucacheNoSqlRepositoryBase<TKey, TValue> : INoSqlStore<TKey, TValue>
    {
        /// <summary>
        /// Get All by default sort order
        /// </summary>
        /// <returns></returns>
        ICollection<TValue> GetAllSorted();
    }
}
