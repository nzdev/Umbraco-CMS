using System;
using System.Collections.Generic;
using System.Text;
using Umbraco.Cms.Infrastructure.PublishedCache.Persistence;

namespace Umbraco.Cms.Infrastructure.PublishedCache.DataSource
{
    public interface INoSqlStoreFactory<TKey, TValue>
    {
        /// <summary>
        /// Gets an instance of INoSqlStore
        /// </summary>
        /// <param name="name">Dictionary name</param>
        /// <param name="keyComparer">Optional comparer for ordering</param>
        /// <param name="isReadOnly">Whether to open as readonly</param>
        /// <param name="enableCount">Whether Count is updated. (Expensive if true)</param>
        /// <returns></returns>
        INoSqlStore<TKey, TValue> Get(string name, IComparer<TKey> keyComparer = null, bool isReadOnly = false, bool enableCount = false);

        /// <summary>
        /// Clear out all records
        /// </summary>
        /// <param name="name">Dictionary name</param>
        void Drop(string name);

        /// <summary>
        /// Ensures that the INoSqlStoreFactory has the proper environment to run.
        /// </summary>
        /// <param name="errors">The errors, if any.</param>
        /// <returns>A value indicating whether the INoSqlStoreFactory has the proper environment to run.</returns>
        bool EnsureEnvironment(out IEnumerable<string> errors);
    }
}
