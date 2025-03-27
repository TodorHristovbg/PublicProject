using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace LogicalInterpretator
{
    internal class QMCnode
    {
        internal MyList<int> coverage { get; set; }
        internal bool?[] values { get; set; }

        internal bool necessary { get; set; } = true;

        internal QMCnode(int Coverage, bool?[] Values)
        {
            coverage = new MyList<int>();
            coverage.Add(Coverage);
            values = Values;
          //  Console.WriteLine("Node {0} created",coverage);
        }
        internal void Print()
        {
            for(int i=0; i<coverage.Count; i++)
            {
                Console.Write(coverage[i] + " ");
            }
           
            for(int i = 0; i < values.Length; i++)
            {
                if (values[i] == null)
                {
                    Console.Write("null ");
                }
                Console.Write(values[i]+" ");
            }
            Console.WriteLine();
        }
        internal static bool QMCcompare(QMCnode first, QMCnode second)
        {
            
            if (first == null || second == null || (first.values.Length != second.values.Length))
            {
              //  Console.WriteLine("DEFAULT FALSE");
                return false;
            }

            for (int i = 0; i < first.values.Length; i++)
            {

                if (first.values[i] != second.values[i])
                {
                    return false;
                }
            }
        
            return true;
        }

        internal QMCnode(MyList<int> Coverage, bool?[] Values)
        {
            coverage=Coverage;
            values = Values;
            //  Console.WriteLine("Node {0} created",coverage);
        }
        internal static QMCnode QmcMerge(QMCnode first, QMCnode second,int index)
        {
         
            MyList<int> ints = new MyList<int>(first.coverage.Count);
            bool?[] newvalues = new bool?[first.values.Length];
            for(int i = 0; i < first.coverage.Count; i++)
            {
                ints.Add(first.coverage[i]);
            }
            for(int i = 0; i < first.values.Length;i++)
            {
                newvalues[i] = first.values[i];
            }
            newvalues[index] = null;
            QMCnode node = new QMCnode(ints,newvalues);
            foreach(int item in second.coverage)
            {
                if(!node.coverage.Contains(item)) {
                
                node.coverage.Add(item);
                }
            }
       
          //  node.Print();
            return node;
        }
        internal string ToString()
        {
            string output = "";
            for (int i = 0; i < coverage.Count; i++)
            {

                output += coverage[i] + " ";
            }

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == null)
                {

                    output += "null ";
                }

                output += values[i] + " ";
            }
            return output;
        }

    }
    }
   
