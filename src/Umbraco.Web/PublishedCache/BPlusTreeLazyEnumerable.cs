using CSharpTest.Net.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web.PublishedCache.NuCache;

namespace Umbraco.Web.PublishedCache
{
    public class BPlusTreeLazyEnumerable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private readonly BPlusTree<TKey, TValue> _bplusTree;
        private readonly Func<TKey, TValue> _lazyLoader;

        public BPlusTreeLazyEnumerable(BPlusTree<TKey,TValue> bplusTree, Func<TKey, TValue> lazyLoader)
        {
            _bplusTree = bplusTree;
            _lazyLoader = lazyLoader;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new BPlusTreeLazyKeyValuePairEnumerator<TKey, TValue>(_bplusTree, _lazyLoader);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new BPlusTreeLazyEnumerator<TKey, TValue>(_bplusTree, _lazyLoader);
        }
    }

    public class BPlusTreeLazyEnumerator<TKey, TValue> : IEnumerator, IDisposable
    {
        private Func<TKey, TValue> _lazyLoader;
        private IEnumerator<KeyValuePair<TKey, TValue>> _enumerator;

        public BPlusTreeLazyEnumerator(BPlusTree<TKey, TValue> bplusTree, Func<TKey, TValue> lazyLoader)
        {
            _enumerator = bplusTree.GetEnumerator();
            _lazyLoader = lazyLoader;
        }
        public object Current => _enumerator.Current;

        public void Dispose()
        {
            _enumerator?.Dispose();
            _lazyLoader = null;
        }

        public bool MoveNext()
        {
            var moved = _enumerator.MoveNext();
            if(moved && _enumerator.Current is ILazyLoad<TKey,TValue> lazyLoadable)
            {
                lazyLoadable?.SetLazyLoader(_lazyLoader);
            }
            return moved;
        }

        public void Reset()
        {
            _enumerator.Reset();
            if (_enumerator.Current is ILazyLoad<TKey, TValue> lazyLoadable)
            {
                lazyLoadable?.SetLazyLoader(_lazyLoader);
            }
        }

    }

    public class BPlusTreeLazyKeyValuePairEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private Func<TKey, TValue> _lazyLoader;
        private IEnumerator<KeyValuePair<TKey, TValue>> _enumerator;

        public BPlusTreeLazyKeyValuePairEnumerator(BPlusTree<TKey, TValue> bplusTree, Func<TKey, TValue> lazyLoader)
        {
            _enumerator = bplusTree.GetEnumerator();
            _lazyLoader = lazyLoader;
        }
        public KeyValuePair<TKey, TValue> Current => _enumerator.Current;

        object IEnumerator.Current => _enumerator.Current;

        public void Dispose()
        {
            _enumerator.Dispose();
            _lazyLoader = null;
        }

        public bool MoveNext()
        {
            var moved = _enumerator.MoveNext();
            if (moved && _enumerator.Current is ILazyLoad<TKey, TValue> lazyLoadable)
            {
                lazyLoadable?.SetLazyLoader(_lazyLoader);
            }
            return moved;
        }

        public void Reset()
        {
            _enumerator.Reset();
            if (_enumerator.Current is ILazyLoad<TKey, TValue> lazyLoadable)
            {
                lazyLoadable?.SetLazyLoader(_lazyLoader);
            }
        }
    }
}
