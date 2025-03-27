using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LogicalInterpretator
{   
    internal class MyList<T> : IList<T> 
    {
        private readonly EqualityComparer<T> comparer = EqualityComparer<T>.Default;
        public int Count { get; set; }  
        T[] _array;
        bool ICollection<T>.IsReadOnly => false;

        int ICollection<T>.Count => throw new NotImplementedException();

        public MyList() 
        {
          //  Console.WriteLine("List created");
            _array = Array.Empty<T>();

        }
        public MyList(int capacity)
        {
            _array = new T[capacity];
            //Count = capacity;
        }
        public MyList(IEnumerable<T> input)
        {
            if(input is ICollection<T> collection)
            {
                _array=new T[collection.Count];
            }
            foreach (T item in input)
            {
                Add(item);
            }

        }
        private void CheckSize()
        {
            if (Count < _array.Length)
            {
                return;
            }
            int newLength;
            if(Count == 0)
            {
                newLength = 4;
            } else
            {
                newLength = Count * 2;
            }
           

            var newarr = new T[newLength];
            Array.Copy(_array,newarr,Count);
            _array = newarr;

        }

        
        public void Add(T item)
        {
            CheckSize();
            _array[Count] = item;
            Count++;

        }
        public bool Contains(T item)
        {
            return IndexOf(item) >= 0;
        }
        public bool Remove(T item)
        {
          

            int index = IndexOf(item);
            if (index < 0)
            {
                return false;
            }
           // Console.WriteLine("removed {0} from the list what?", item);
            Array.Copy(_array, index + 1, _array, index, Count - index - 1);
           
            Count--;
            return true;

        }
        public void RemoveAt(int index)
        {
            for (int i = 0; i < Count; i++)
            {
                
            }
            Array.Copy(_array, index + 1, _array, index, Count - index - 1);
            Count--;
        }


        public int IndexOf(T item)
        {
            if(Count == 0)
            {
                return -1;
            }
            for(int i = 0; i < Count; i++)
            {
                if (comparer.Equals(item, _array[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            CheckSize();

            Array.Copy(_array, index, _array, index + 1, Count - index);

            _array[index] = item;
            Count++;
        }

        public void Clear()
        {
          //  Console.WriteLine("clear called");
           Array.Clear(_array, 0, Count);
            Count = 0;
           // Console.WriteLine("cleared");
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_array, 0, array,arrayIndex, Count);
        }
        internal static bool isSubSet(MyList<T> sub,MyList<T> main)
        {
            if (sub.Count > main.Count)
            {
                return false;
            }
            for(int i=0; i < sub.Count; i++)
            {
                if (!main.Contains(sub[i]))
                {
                    return false;
                }
            }
            return true;
        }

        internal string toString()
        {
            string output = "";
            for(int i = 0; i < Count; i++)
            {
                output += _array[i] + " ";
            }
            return output;
        }
        internal T[] toArray()
        {
            Console.WriteLine("hii");
            T[] arr = new T[Count];
            Console.WriteLine(Count);
            Console.WriteLine(_array.Length);
            for (int i = 0;i < Count; i++)
            {
                arr[i]= _array[i];
            }
            return arr;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
           for ( int i = 0; i<Count; i++)
            {
                yield return _array[i];
            }
        }
      
        public T this[int index]
        {
            get => _array[index];
            set => _array[index] = value;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
   
}