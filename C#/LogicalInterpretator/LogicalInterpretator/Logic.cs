using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LogicalInterpretator
{
    internal class Logic
    {
        
        internal static void FindFunction(bool[][] inputs)      //priemame masiv s masivite ot stoinosti 
        {
       
      
            MyList<QMCnode> filtered = new MyList<QMCnode>();
            for (int i = 0; i < inputs.Length; i++)
            {
               
                if (inputs[i]?[inputs[i].Length - 1] != null && inputs[i][inputs[i].Length - 1] == true)                //filtrirame go do samo tezi s rezultat true
                {
                    bool?[] nodeinputs = new bool?[inputs[i].Length - 1];
                    for (int k = 0; k < inputs[i].Length - 1; k++)
                    {
                        if (inputs[i][k] == true)
                        {
                            nodeinputs[k] = true;
                        }
                        else
                        {
                            if (inputs[i][k] == false)
                            {
                                nodeinputs[k] = false;
                            }
                            else
                            {
                                nodeinputs[k] = null;
                            }
                        }
                    }
                    
                    QMCnode newnode = new QMCnode(i, nodeinputs);

                    filtered.Add(newnode);          //pravim "filtrirana" tablica ot qmc node-ove
                }
            }
            /*
            foreach (QMCnode node in filtered)
            {
                node.Print();
            }
            */

            /*   Console.WriteLine("printing final table");

                foreach (QMCnode node in table)
                {
                 node.Print();
                 }
            */
            // Console.WriteLine();
            var table = QMCFinalTable(filtered);    //metoda koito kombinira input-ite dokolkoto e vuzmojno
            string solution = WorkWithTable(table);

        }
        private static MyList<QMCnode> QMCFinalTable(MyList<QMCnode> inputs) //poluchavame vsichki values za koito rezultata e true kato nodes
        {
            MyList<QMCnode>[] qmctable = new MyList<QMCnode>[inputs[0].values.Length + 1];        //dvete tablici koito shte menkame
            MyList<QMCnode>[] secondtable = new MyList<QMCnode>[inputs[0].values.Length + 1];
            MyList<QMCnode> finaltable = new MyList<QMCnode>();             //krainata tablica koqto shte vurnem
                                                                     // NE e zaduljitelno da e ekvivalentna s tazi ot posledniqt loop


            foreach (QMCnode node in inputs)                //inicializirane na "grupite" po broi edinici i nujnite listi
            {
                int counter = 0;
                for (int i = 0; i < node.values.Length; i++)
                {

                    if (node.values[i] == true)
                    {
                        counter++;
                    }
                    if (qmctable[counter] == null)
                    {
                        //   Console.WriteLine("initializing group " + counter);
                        qmctable[counter] = new MyList<QMCnode>();
                        secondtable[counter] = new MyList<QMCnode>();

                    }
                }
                qmctable[counter].Add(node);
            }

            bool keepgoing = true;          //obrabotvame pone vednuj i produljavame dokato ne napravim loop v koito ne sme 
            while (keepgoing)               // kombinirali qmc node-ove
            {
               
                keepgoing = false;
                for (int i = 0; i < qmctable.Length - 1; i++)
                {
                    //   Console.WriteLine(qmctable[i].Count);
                    if (qmctable[i] != null && qmctable[i + 1] != null)   //qmctable[i] i qmctable[i+1] sa listi
                    {
                        for (int j = 0; j < qmctable[i].Count; j++)      //obhojdame vseki list (grupa s daden broi edinici)
                        {
                            for (int k = 0; k < qmctable[i + 1].Count; k++)
                            {
                             
                                int indexofdiff = -1;                                        //-1 -> default
                                for (int l = 0; l < qmctable[i][j].values.Length; l++)    // l <-> index ako ima razlika samo v edin input
                                {
                                    if (qmctable[i][j].values[l] != qmctable[i + 1][k].values[l])
                                    {

                                        if (indexofdiff == -1)
                                        {
                                            indexofdiff = l;
                                        }
                                        else
                                        {
                                            indexofdiff = -2;           //-2 pri razlika ot poveche ot edin input -> invalid
                                        }
                                    }
                                }
                                if (indexofdiff > -1)
                                {
                                    bool?[] copy = new bool?[qmctable[i][j].values.Length];       //pravim kopie 
                                    qmctable[i][j].values.CopyTo(copy, 0);

                                    QMCnode temp = QMCnode.QmcMerge(qmctable[i][j], qmctable[i + 1][k], indexofdiff);  
                                    bool unique = true;
                                    qmctable[i][j].necessary = false;   //znaem sus sigurnost che ili dobavqme kombinaciqta na dvete ili imame ekvivalentna -> unnecessary
                                    qmctable[i + 1][k].necessary = false;
                                    for (int cnt = 0; cnt < secondtable[i].Count; cnt++)
                                    {
                                        if (QMCnode.QMCcompare(secondtable[i][cnt], temp))      //proverqvame dali imame node sus sushtite values
                                        {
                                            unique = false;
                                            break;
                                        }
                                    }
                                    if (unique)
                                    {
                                        secondtable[i].Add(temp);  //ako e unique go dobavqme i znaem che trqbva da minem tablicata pak
                                       // temp.Print();
                                        keepgoing = true;

                                    }

                                }
                                else if (indexofdiff == -1)
                                {
                                    Console.WriteLine("match");         //ne bi trqbvalo nikoga da stignem tuk
                                }
                                else
                                {
                                    //Console.WriteLine("cant simplify");
                                }
                            }
                        }
                    }
                }

                if (keepgoing)
                {

                    for (int i = 0; i < qmctable.Length; i++)
                    {
                        if (qmctable[i] != null)
                        {
                            for (int k = 0; k < qmctable[i].Count; k++)
                            {
                                if (qmctable[i][k].necessary)       // "necessary" sa node-ove koito ne mojem da kombinirame s nishto,
                                {                                   // no ne sme sigurni dali sme dostignali krainata tablica
                                    {
                                     
                                       // qmctable[i][k].Print();
                                        finaltable.Add(qmctable[i][k]);
                                    }
                                }
                            }
                        }
                    }
                    qmctable = secondtable;                                           //razmenqme tablicite i produljavame      
                    secondtable = new MyList<QMCnode>[inputs[0].values.Length + 1];
                    for (int i = 0; i < qmctable.Length; i++)
                    {
                        secondtable[i] = new MyList<QMCnode>();
                    }
                } else
                {
                    for (int i = 0; i < qmctable.Length; i++)
                    {
                      //  Console.WriteLine(qmctable.Length);
                        for (int k = 0; k < qmctable[i].Count; k++)
                        {
                            finaltable.Add(qmctable[i][k]);
                            //ako sme stignali poslednata tablica dobavqme vsichki novi kombinacii vuv finalnata 
                        }
                    }
                }
            }
            return finaltable;   //vrushtame tablicata s maximalno kombiniranite node-ove


        }

        private static string WorkWithTable(MyList<QMCnode> qmctable)
        {
            //poluchavame vuzmojno nai kombinirata tablica i zapochvame da q obrabotvame

            MyList<QMCnode> uniques = new MyList<QMCnode> ();           //krainiqt spisuk s resheniq    
            MyDictionary<int, int> found = new MyDictionary<int, int>();  //tablica s "inputi" i kolko puti gi pokrivame

            for (int i = 0; i < qmctable.Count; i++)
            {
                for (int j = 0; j < qmctable[i].coverage.Count; j++)              //obhojdame vseki element koito vseki node "pokriva"
                {
                    if (!found.ContainsKey(qmctable[i].coverage[j]))
                    {
                        found.Add(qmctable[i].coverage[j], 1);
                    }
                    else
                    {
                        found.TryGetValue(qmctable[i].coverage[j], out int pair); //ako ne go sreshtame za pruv put update-vame tablicata
                        found.Remove(qmctable[i].coverage[j]);
                        found.Add(qmctable[i].coverage[j], pair + 1);
                    }
                }

            }

            RecursiveMakeSolution(qmctable,uniques,found);
          
            foreach(QMCnode node in qmctable)
            {
                uniques.Add(node);
            }
            /*
            Console.WriteLine("Solution");
            foreach(QMCnode node in uniques)
            {
                Console.WriteLine(node.ToString());
            } */
            Console.WriteLine();
            MakeSolution(uniques);
            return "bob";
        }

        internal static void RecursiveMakeSolution(MyList<QMCnode> qmctable,MyList<QMCnode> uniques,MyDictionary<int,int> found)
        {

            // found.PrintwithValues();
            // Console.WriteLine(found.Count());

            // proverqvame za input-i koito sa pokriti ot samo edin node



            for (int i = 0; i < qmctable.Count; i++)
            {
                for (int j = 0; j < qmctable[i].coverage.Count; j++)
                {
                    int temp1 = qmctable[i].coverage[j];
                    {
                        found.TryGetValue(temp1, out int pair);
                        if (pair == 1)
                        {
                           // Console.WriteLine("calling again after unique");
                          
                            uniques.Add(qmctable[i]);
                            RemoveFromFinalTable(found, qmctable[i], qmctable); //mahame vsichki inputi koito noda pokriva
                            qmctable.RemoveAt(i);         //vkluchitelno ot ostanalite nodeove, tui kato znaem che nqma da sa nujni zaradi tochno tezi inputi
                            RecursiveMakeSolution(qmctable, uniques, found);
                            return;
                        }
                    }
                }
            }
            
            //  found.PrintwithValues();


            for (int i = 0; i < qmctable.Count - 1; i++)      //znaem che ostanalite node-ove se prepokrivat
            {
                for (int j = i + 1; j < qmctable.Count; j++)
                {
                    if (qmctable[i].coverage.Count < qmctable[j].coverage.Count)    //proverqvame vsqka kombinaciq
                    {
                        //ako ima node koito sudurja drug v tova koeto pokriva, mahame po-malkiqt
                        if (MyList<int>.isSubSet(qmctable[i].coverage, qmctable[j].coverage))
                        {
                            for (int k = 0; k < qmctable[i].coverage.Count; k++)
                            {
                                found.TryGetValue(qmctable[i].coverage[k], out int pair);
                                found.Remove(qmctable[i].coverage[k]);
                                found.Add(qmctable[i].coverage[k], pair - 1);
                            }
                            qmctable.RemoveAt(i);
                            continue;
                        }
                    }
                    else
                    {
                        if (MyList<int>.isSubSet(qmctable[j].coverage, qmctable[i].coverage))
                        {
                            for(int k = 0;k < qmctable[j].coverage.Count;k++)
                            {
                                found.TryGetValue(qmctable[j].coverage[k], out int pair); 
                                found.Remove(qmctable[j].coverage[k]);
                                found.Add(qmctable[j].coverage[k], pair - 1);
                            }

                            qmctable.RemoveAt(j);
                            continue;
                        }
                    }
                }
            }

            //ako sme ostanali samo s edin input se vrushtame
            if (qmctable.Count == 1)
            {
                uniques.Add(qmctable[0]);
                return;
            }

            //Console.WriteLine("banger");
            found.PrintwithValues();

            for (int i = 0; i < qmctable.Count-1; i++)    //proverqvame dali ima input pokrit ot vsichki node-ove
            {
                for (int j = 0; j < qmctable[i].coverage.Count; j++)
                {
                    found.TryGetValue(qmctable[i].coverage[j], out int pair);
                   // Console.WriteLine("checkvame za: "+qmctable[i].coverage[j]);
                    if (pair == qmctable.Count)     //ako ima go mahame ot vsichki node-ove i posle i ot tablicata
                    {
                   //     Console.WriteLine("match za " + qmctable[i].coverage[j]);
                        for(int k = i + 1; k < qmctable.Count; k++)
                        {
                      
                            if (qmctable[k].coverage.Contains(qmctable[i].coverage[j]))
                            {
                       //         Console.WriteLine("found match and removing " + qmctable[i].coverage[j]);
                                qmctable[k].coverage.Remove(qmctable[i].coverage[j]);
                                
                            }

                        }
                        //ako sme premahnali kolona proverqvame pak, ako ne - sme gotovi
                        qmctable[i].coverage.RemoveAt(j);
                        found.Remove(qmctable[i].coverage[j]);
                        RecursiveMakeSolution(qmctable, uniques, found);
                      
                    }
                }
            }
        }
        internal static void RemoveFromFinalTable(MyDictionary<int, int> dict,QMCnode node, MyList<QMCnode> list)
        {
            //mahame nujniq node otvsqkude, i mahame pokritite stoinosti ot ostanalite node-ove
            //v ostanalite nodove ostavat samo tezi variacii koito vse oshte ne se pokriti
            for(int i = 0; i < node.coverage.Count; i++)
            {
             
                //Console.WriteLine("removing " + node.coverage[i]);
                dict.Remove(node.coverage[i]);
                
                for(int j = 0; j < list.Count; j++)
                {
                    if (!(list[j] == node))
                    {
                        if (list[j].coverage.Contains(node.coverage[i]))
                        {
                            list[j].coverage.Remove(node.coverage[i]);
                        }
                    }
     
                }
            }
        }

        internal static String MakeSolution(MyList<QMCnode> list)
        {
          
            string function = "(";

            for(int i =0; i < list.Count; i++)  //obhojdame vseki node
            {
                for (int j = 0;j < list[i].values.Length; j++)      //za vseki node obhojdame vseki value
                {
                    if (list[i].values[j] != null)
                    {
                        
                        if (list[i].values[j] == true)
                        {

                            function += (Convert.ToChar(j + 97));
                        } else
                        {
                            function += "!"+ (Convert.ToChar(j + 97));
                        }

                        if (j + 1 != list[i].values.Length)
                          for(int k = j + 1; k < list[i].values.Length; k++)
                        {
                            if (list[i].values[k] != null)
                            {
                                function += " & ";
                            }
                        }
                    }
                    
                }
                if (i + 1 != list.Count)
                {
                    function += ") | (";
                }
            }
            function += ")";
            Console.WriteLine(function);
            return function;
        }

        internal static void DrawTree(Nodes root)
        {
            Application.EnableVisualStyles();
            Tree tree = new Tree(root);
            Nodes.ResetValues(root);
            Application.Run(tree);
        }
        internal static void DrawSolvedTree(Nodes root, bool[] values)
        {
            Nodes.setValues(values,root);
            Tree tree = new Tree(root);
            root.Evaluate();
            Application.Run(tree);
        }
       
       

    }


    }



    
   

