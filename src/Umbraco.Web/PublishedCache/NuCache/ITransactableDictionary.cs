﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public interface ITransactableDictionary<TKey, TValue> :
        ITransactable,
        IDictionary<TKey, TValue>,
        ICollection<KeyValuePair<TKey, TValue>>,
        IEnumerable<KeyValuePair<TKey, TValue>>,
        IEnumerable,
        IDisposable
    {
        /// <summary>
        /// Whether the files exist in the local filesystem
        /// </summary>
        /// <returns></returns>
        bool LocalFilesExist();

        /// <summary>
        /// Delete the local files
        /// </summary>
        void DeleteLocalFiles();
        bool TryRemove(TKey key, out TValue unused);
    }
    
}
