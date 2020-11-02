using CSharpTest.Net.Collections;
using System;
using System.Collections.Generic;
using Umbraco.Web.PublishedCache.NuCache;

namespace Umbraco.Web.PublishedCache
{
    public class LazyLoadingBPlusTreeTransactableDictionary<TKey, TValue> : BPlusTreeTransactableDictionary<TKey, TValue>, ILazyLoadingTransactableDictionary<TKey, TValue>
    {
        private readonly BPlusTree<TKey, TValue> _routingOnlyReadonlyBplusTree;
        private readonly BPlusTree<TKey, TValue> _routingAndDraftPropertyOnlyReadonlyBplusTree;
        private readonly BPlusTree<TKey, TValue> _routingAndPublishedPropertyOnlyReadonlyBplusTree;
        private readonly Func<TKey, TValue> _lazyLoader;
        public LazyLoadingBPlusTreeTransactableDictionary(BPlusTree<TKey, TValue> bplusTree, string filePath,
          bool localDbCacheFileExists,
          BPlusTree<TKey, TValue> routingPropertyOnlyReadonlyBplusTree = null,
          BPlusTree<TKey, TValue> routingAndDraftPropertyOnlyReadonlyBplusTree = null,
          BPlusTree<TKey, TValue> routingAndPublishedPropertyOnlyReadonlyBplusTree = null) : base(bplusTree, filePath, localDbCacheFileExists)
        {
            _routingOnlyReadonlyBplusTree = routingPropertyOnlyReadonlyBplusTree ?? bplusTree;
            _routingAndDraftPropertyOnlyReadonlyBplusTree = routingAndDraftPropertyOnlyReadonlyBplusTree ?? bplusTree;
            _routingAndPublishedPropertyOnlyReadonlyBplusTree = routingAndPublishedPropertyOnlyReadonlyBplusTree ?? bplusTree;
            _lazyLoader = (key) =>
            {
                bool found = bplusTree.TryGetValue(key, out TValue value);
                if (!found)
                {
                    return default(TValue);
                }
                return value;
            };
        }

        #region ILazyLoadingTransactableDictionary<TKey,TValue>
        public IEnumerable<KeyValuePair<TKey, TValue>> GetEnumerator(ContentNodeKitLoadState requestedLoadState)
        {
            if (requestedLoadState == ContentNodeKitLoadState.All ||
                (requestedLoadState == ContentNodeKitLoadState.AllDraftPropertiesLoaded &&
                requestedLoadState == ContentNodeKitLoadState.AllPublishedPropertiesLoaded
                ))
            {
                return new BPlusTreeLazyEnumerable<TKey, TValue>( _bplusTree, _lazyLoader);
            }
            if (requestedLoadState == ContentNodeKitLoadState.AllPublishedPropertiesLoaded)
            {
                return new BPlusTreeLazyEnumerable<TKey, TValue>(_routingAndPublishedPropertyOnlyReadonlyBplusTree, _lazyLoader);
            }
            if (requestedLoadState == ContentNodeKitLoadState.AllDraftPropertiesLoaded)
            {
                return new BPlusTreeLazyEnumerable<TKey, TValue>(_routingAndDraftPropertyOnlyReadonlyBplusTree, _lazyLoader);
            }
            if (requestedLoadState == ContentNodeKitLoadState.RoutingPropertiesLoaded)
            {
                return new BPlusTreeLazyEnumerable<TKey, TValue>(_routingOnlyReadonlyBplusTree, _lazyLoader);
            }
            return new BPlusTreeLazyEnumerable<TKey, TValue>(_bplusTree, _lazyLoader);
        }
        #endregion
    }
}
