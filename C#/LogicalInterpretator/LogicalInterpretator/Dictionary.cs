using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicalInterpretator
{
    internal class MyDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : notnull
    {
        public MyList<KeyValuePair<TKey, TValue>>?[] _items = new MyList<KeyValuePair<TKey, TValue>>[20];
        private EqualityComparer<TKey> _comparer = EqualityComparer<TKey>.Default;

        private readonly EqualityComparer<KeyValuePair<TKey, TValue>> _paircomparer = EqualityComparer<KeyValuePair<TKey, TValue>>.Default;
        public int Count { get; set; }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

        public MyList<TKey> _keys = new MyList<TKey>();
        public MyList<TValue> _values = new MyList<TValue>();
        public ICollection<TKey> Keys { get; }
        public ICollection<TValue> Values { get; }

        public MyDictionary()
        {
            //   Console.WriteLine("We initialize it");
            //    _items = 

        }
        public MyDictionary(int size)
        {
            //   _items = new MyList<KeyValuePair<TKey, TValue>>[size];

        }
        public TValue this[TKey key] {

            get
            {
                var list = _items[GetIndex(key)];
                if (list is null)
                {
                    throw new KeyNotFoundException();
                }

                foreach (var item in list)
                {
                    if (_comparer.Equals(item.Key, key))
                    {
                        return item.Value;
                    }
                }
                throw new KeyNotFoundException();
            }
            set
            {
                var list = _items[GetIndex(key)];
                if (list is null)
                {
                    Add(key, value);
                    return;
                }

                for (int i = 0; i < list.Count; i++)
                {

                    if (_comparer.Equals(key, list[i].Key))
                    {
                        list[i] = new KeyValuePair<TKey, TValue>(key, value);
                    }

                }
            }
        }

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(TKey key, TValue value)
        {

            if (_keys.Contains(key))
            {
                Console.WriteLine("we already have it from add " + key);
                return;
            }
            //  Console.WriteLine("adding {0} and {1}",key,value);
            int index = GetIndex(key);
            var item = new KeyValuePair<TKey, TValue>(key, value);
            var list = _items[index];

            if (list is null)
            {
                var newList = new MyList<KeyValuePair<TKey, TValue>>();
                _items[index] = newList;
                list = newList;
            }


            _keys.Add(key);
            _values.Add(value);
            list.Add(item);
            Count++;
            //  Console.WriteLine("added to dic : " + item.Key);


        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            int index = GetIndex(item.Key);
            var list = _items[index];
            if (list is null)
            {
                var newList = new MyList<KeyValuePair<TKey, TValue>>();
                _items[index] = newList;
                list = newList;
            }
            _keys.Add(item.Key);
            _values.Add(item.Value);
            list.Add(item);
            Count++;
        }
        public void Clear()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i]?.Clear();

            }
            Count = 0;
        }

        public void Print()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null)
                {
                    foreach (KeyValuePair<TKey, TValue> item in _items?[i])
                    {
                        Console.WriteLine(item.Key);
                    }
                }
            }
        }
        public void PrintwithValues()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null)
                {
                    foreach (KeyValuePair<TKey, TValue> item in _items?[i])
                    {
                        //Console.WriteLine("stuff at index" + i);
                        Console.WriteLine(item.Key + " " + item.Value);

                    }
                }
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            int index = GetIndex(item.Key);
            var list = _items[index];
            if (list is null)
            {
                return false;
            }

            return list.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            // _comparer=EqualityComparer<TKey>.Default;
            if (_keys.Contains(key))
            {
                //   Console.WriteLine("we have it " + key);
                return true;
            }

            int index = GetIndex(key);
            var list = _items[index];
            if (list is null)
            {
                return false;
            }
            for (int i = 0; i < list.Count; i++)
            {
                //   Console.WriteLine("comparing {0} and {1}", list[i].Key, key);
                if (_comparer.Equals(list[i].Key, key))
                {
                    return true;
                }
            }
            //Console.WriteLine("nothing found ");
            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array.Length == 0)
            {
                throw new ArgumentNullException("array is null");

            }

            if (array.Length - arrayIndex < Values.Count())
            {
                throw new ArgumentException("not enough space");
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("starting index cannot be negative");
            }
            int arraycounter = 0;
            for (int i = 0; i < _items.Length; i++)
            {
                for (int j = 0; j < _items[i].Count(); j++)
                {
                    array[arraycounter] = _items[i][j];
                    arraycounter++;
                }
            }


        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {

            if (!ContainsKey(key))
            {
                Console.WriteLine("key is not in dictionary");
                return false;
            }

            int hash = GetIndex(key);
            var list = _items[hash];
            for (int i = 0; i < list.Count; i++)
            {
                if (_comparer.Equals(list[i].Key, key))
                {

                    _keys.Remove(key);
                    _values.Remove(list[i].Value);
                    //  Console.WriteLine("removing value (frequiency) : " + list[i].Value);
                    list.RemoveAt(i);
                    Count--;

                    return true;
                }
            }
            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (!ContainsKey(item.Key))
            {
                return false;
            }
            int hash = GetIndex(item.Key);
            var list = _items[hash];
            for (int i = 0; i < list.Count; i++)
            {
                if (_paircomparer.Equals(list[i], item))
                {
                    list.RemoveAt(i);
                    Count--;
                    return true;
                }
            }
            return false;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (!ContainsKey(key))
            {
                value = default(TValue);
                return false;
            }
            var hash = GetIndex(key);
            var list = _items[hash];
            for (int i = 0; i < list.Count; i++)
            {
                if (_comparer.Equals(list[i].Key, key))
                {
                    value = list[i].Value;
                    return true;
                }
            }
            value = default(TValue);
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        private int GetIndex(TKey key)
        {
            //   _comparer = EqualityComparer<TKey>.Default;
            //int hash = Math.Abs(key.GetHashCode());
            int hash = Math.Abs(_comparer.GetHashCode(key));

            return hash % _items.Length;
        }

        internal MyList<KeyValuePair<TKey,TValue>> toList()
        {
            MyList<KeyValuePair<TKey,TValue>> list = new MyList<KeyValuePair<TKey,TValue>>();
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null)
                {
                    foreach (KeyValuePair<TKey, TValue> item in _items?[i])
                    {
                        //Console.WriteLine("stuff at index" + i);
                        list.Add(item);

                    }
                }
            }
            return list;
        }

    }
}
