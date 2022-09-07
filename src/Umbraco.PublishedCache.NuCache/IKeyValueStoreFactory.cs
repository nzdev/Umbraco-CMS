using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Cms.Infrastructure.PublishedCache
{
    public interface IKeyValueStoreFactory<TKey, TValue>
    {
        /// <summary>
        /// Gets an instance of ITransactableDictionary
        /// </summary>
        /// <param name="name">Dictionary name</param>
        /// <param name="keyComparer">Optional comparer for ordering</param>
        /// <param name="isReadOnly">Whether to open as readonly</param>
        /// <param name="enableCount">Whether Count is updated. (Expensive if true)</param>
        /// <returns></returns>
        IKeyValueStore<TKey, TValue> Get(string name, IComparer<TKey> keyComparer = null, bool isReadOnly = false, bool enableCount = false);
        /// <summary>
        /// Clear out all records
        /// </summary>
        /// <param name="name">Dictionary name</param>
        void Drop(string name);

        bool StorageExists(string name);
    }
}
