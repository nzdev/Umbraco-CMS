﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public interface ITransactableDictionaryFactory
    {
        ITransactableDictionary<int, IContentNodeKit> Get(ContentCacheEntityType entityType);

        /// <summary>
        /// Ensures that the ITransactableDictionaryFactory has the proper environment to run.
        /// </summary>
        /// <param name="errors">The errors, if any.</param>
        /// <returns>A value indicating whether the ITransactableDictionaryFactory has the proper environment to run.</returns>
        bool EnsureEnvironment(out IEnumerable<string> errors);

        /// <summary>
        /// Whether the dictionary has been populated
        /// </summary>
        /// <returns></returns>
        bool IsPopulated(ContentCacheEntityType entityType);
    }
}
