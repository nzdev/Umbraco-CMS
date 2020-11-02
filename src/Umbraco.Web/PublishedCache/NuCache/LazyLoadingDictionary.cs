using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Web.PublishedCache.NuCache
{
    public class LazyLoadingDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        private IReadOnlyDictionary<TKey, TValue> _dictionary;
        private IReadOnlyDictionary<TKey, bool> _loadedProperties;
        private Func<IReadOnlyDictionary<TKey,TValue>> _load;
        private bool _materialized;
        private object _loadLock = new object();
        public LazyLoadingDictionary(Dictionary<TKey, TValue> propertiesDictionary, IReadOnlyDictionary<TKey, bool> loadedStateDictionary, Func<IReadOnlyDictionary<TKey, TValue>> load, bool materialized)
        {
            _dictionary = propertiesDictionary;
            _load = load;
            _materialized = materialized;
            _loadedProperties = loadedStateDictionary;
        }

        public TValue this[TKey key] => GetByKey(key);

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

        private TValue GetByKey(TKey key)
        {
            MaterializeIfRequired(key);
            return ((IReadOnlyDictionary<TKey, TValue>)_dictionary)[key];
        }

        public IEnumerable<TKey> Keys => ((IReadOnlyDictionary<TKey, TValue>)_dictionary).Keys;

        public IEnumerable<TValue> Values => GetValues();

        private IEnumerable<TValue> GetValues()
        {
            Materialize();
            return ((IReadOnlyDictionary<TKey, TValue>)_dictionary).Values;
        }

        public int Count => ((IReadOnlyCollection<KeyValuePair<TKey, TValue>>)_dictionary).Count;

        public bool ContainsKey(TKey key)
        {
            return ((IReadOnlyDictionary<TKey, TValue>)_dictionary).ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            Materialize();
            return ((IEnumerable<KeyValuePair<TKey, TValue>>)_dictionary).GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            Materialize();
            return ((IReadOnlyDictionary<TKey, TValue>)_dictionary).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Materialize();
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
