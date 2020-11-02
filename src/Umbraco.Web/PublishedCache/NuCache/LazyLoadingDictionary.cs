using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public class LazyLoadingDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private IDictionary<TKey, TValue> _dictionary;
        private IReadOnlyDictionary<TKey, bool> _loadedProperties;
        private Func<IDictionary<TKey,TValue>> _load;
        private bool _materialized;
        private object _loadLock = new object();

        public ICollection<TKey> Keys => _dictionary.Keys;

        public ICollection<TValue> Values => _dictionary.Values;

        public int Count => _dictionary.Count;

        public bool IsReadOnly => _dictionary.IsReadOnly;

        public TValue this[TKey key] { get => _dictionary[key]; set => _dictionary[key] = value; }

        public LazyLoadingDictionary(IDictionary<TKey, TValue> propertiesDictionary, IReadOnlyDictionary<TKey, bool> loadedStateDictionary, Func<IDictionary<TKey, TValue>> load, bool materialized)
        {
            _dictionary = propertiesDictionary;
            _load = load;
            _materialized = materialized;
            _loadedProperties = loadedStateDictionary;
        }

        private void MaterializeIfRequired(TKey key)
        {
            if (!_materialized)
            {
                if (_loadedProperties != null && _loadedProperties.TryGetValue(key, out bool isLoaded))
                {
                    //is a property
                    if (!isLoaded)
                    {
                        Materialize();
                    }
                }
            }
        }
        private void Materialize()
        {
            if (!_materialized)
            {
                //not loaded
                lock (_loadLock)
                {
                    //Double Check materialized
                    if (!_materialized)
                    {
                        _dictionary = _load();//Load
                        _materialized = true;
                        _loadedProperties = null;
                        _load = null;
                    }
                }
            }
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
        }

        public bool Remove(TKey key)
        {
            return _dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            MaterializeIfRequired(key);
            return _dictionary.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _dictionary.Add(item);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            Materialize();
            return _dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Materialize();
            _dictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            Materialize();
            return _dictionary.Remove(item);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            Materialize();
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Materialize();
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
