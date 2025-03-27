using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LogicalInterpretator
{
    internal static class GlobalDic
    {
        public static MyDictionary<String, Nodes> _Table = new MyDictionary<String, Nodes>();
        public static void Create()
        {
          
        }
        public static MyDictionary<String, Nodes> getTable()
        {
            return _Table;
        }

        public static void Add(Nodes newnode)
        {
            KeyValuePair<String, Nodes> item = new KeyValuePair<String,Nodes>(newnode.Name,newnode);
           
          //  Console.WriteLine(newnode.Name + " added to dictionary \n ");
            _Table.Add(item);
        }

    }
}
