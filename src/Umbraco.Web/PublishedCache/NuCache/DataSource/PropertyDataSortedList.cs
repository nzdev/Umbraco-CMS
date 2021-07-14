using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Web.PublishedCache.NuCache.DataSource
{
    internal class PropertyDataSortedList : IDictionary<string, PropertyData[]>
    {
        private SortedList<short, PropertyData[]> _sortedList;
        private static short NextShortKey = 0;
        private static object NextShortKeyLock = new object();
        private static ConcurrentDictionary<string, short> KeyMap = new ConcurrentDictionary<string, short>(StringComparer.InvariantCultureIgnoreCase);

        public ICollection<string> Keys => ((IDictionary<string, PropertyData[]>)_sortedList).Keys;

        public ICollection<PropertyData[]> Values => ((IDictionary<string, PropertyData[]>)_sortedList).Values;

        public int Count => ((ICollection<KeyValuePair<string, PropertyData[]>>)_sortedList).Count;

        public bool IsReadOnly => ((ICollection<KeyValuePair<string, PropertyData[]>>)_sortedList).IsReadOnly;

        public PropertyData[] this[string key] { get => _sortedList[MapKey(key)]; set => _sortedList[MapKey(key)] = value; }

        public PropertyDataSortedList()
        {
            _sortedList = new SortedList<short, PropertyData[]>();
        }
        public PropertyDataSortedList(int capacity)
        {
            _sortedList = new SortedList<short, PropertyData[]>(capacity);
        }
        public PropertyDataSortedList(IDictionary<string, PropertyData[]> existing)
        {
            _sortedList = new SortedList<short, PropertyData[]>(existing.ToDictionary(x => MapKey(x.Key), x => x.Value));
        }
        public bool ContainsKey(string key)
        {
            if (KeyMap.TryGetValue(key, out short shortKey))
            {
                return _sortedList.ContainsKey(shortKey);
            }
            return false;
        }
        private static short NextKey()
        {
            lock (NextShortKeyLock)
            {
                return NextShortKey++;
            }
        }
        internal static short MapKey(string key)
        {
            return KeyMap.GetOrAdd(key, x => NextKey());
        }
        internal static string ReverseKey(short key)
        {
            return KeyMap.FirstOrDefault(x=> x.Value == key).Key;
        }
        public void Add(string key, PropertyData[] value)
        {
            _sortedList.Add(MapKey(key), value);
        }


        public bool Remove(string key)
        {
            return _sortedList.Remove(MapKey(key));
        }

        public bool TryGetValue(string key, out PropertyData[] value)
        {
            return _sortedList.TryGetValue(MapKey(key), out value);
        }

        public void Add(KeyValuePair<string, PropertyData[]> item)
        {
            _sortedList.Add(MapKey(item.Key), item.Value);
        }

        public void Clear()
        {
            _sortedList.Clear();
        }

        public bool Contains(KeyValuePair<string, PropertyData[]> item)
        {
            return _sortedList.Contains(new KeyValuePair<short, PropertyData[]>(MapKey(item.Key), item.Value));
        }

        public void CopyTo(KeyValuePair<string, PropertyData[]>[] array, int arrayIndex)
        {
            throw new NotSupportedException();
            //_sortedList.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, PropertyData[]> item)
        {
            return _sortedList.Remove(MapKey(item.Key));
        }

        public IEnumerator<KeyValuePair<string, PropertyData[]>> GetEnumerator()
        {
            return new PropertyDataSortedListEnumerator(_sortedList.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new PropertyDataSortedListEnumerator(_sortedList.GetEnumerator());
        }
    }
    internal class PropertyDataSortedListEnumerator : IEnumerator<KeyValuePair<string, PropertyData[]>>
    {
        private readonly IEnumerator<KeyValuePair<short, PropertyData[]>> _enumerator;

        public PropertyDataSortedListEnumerator(IEnumerator<KeyValuePair<short, PropertyData[]>> enumerator)
        {
            _enumerator = enumerator;
        }

        public KeyValuePair<string, PropertyData[]> Current => new KeyValuePair<string, PropertyData[]>(PropertyDataSortedList.ReverseKey(_enumerator.Current.Key), _enumerator.Current.Value);

        object IEnumerator.Current => ((IEnumerator)_enumerator).Current;

        public void Dispose()
        {
            _enumerator.Dispose();
        }

        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }

        public void Reset()
        {
            _enumerator.Reset();
        }
    }
}
