using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace LogicalInterpretator
{
    internal class Validate
    {
        public static void HandleCommand(string input)
        {
          
            string copy = input;
           
            copy = StringStuff.MyTrim(copy);
        
            copy = StringStuff.SplitUntil(copy, ' ');
            
            copy =copy.ToLower();
   

    
            switch (copy)
            {
                case "define":
                  
                    Define(input);
                    return;
                case "solve":
                    Solve(input); 
                    return;
                case "all":
                    All(input); 
                    return;
                case "find":
                    Find(input);
                    return;
            }

        }

     

        private static void Define(string input)
        {
            var command = StringStuff.TokenizeForDefine(input);
         
            Console.WriteLine();
            var root = StringStuff.makeroot(command[2], StringStuff.getOperands(command[1]));
           
            root.Name = command[1];
            GlobalDic.Add(root);
            Console.WriteLine(root.Name+" defined");
           
        }
        private static void Solve(string input)
        {
            StringStuff.TokenizeForSolve(input);

        }
        private static void All(string input)
        {
            input = StringStuff.MyTrim(input);
            string temp = StringStuff.SplitUntil(input, ' ');

            input = StringStuff.MyTrim(StringStuff.MySubstring(input, temp.Length, input.Length - temp.Length));

            string key = StringStuff.SplitUntil(input, ')') + ")";

            Console.WriteLine("solving all for "+key);
            GlobalDic.getTable().TryGetValue(key, out var root);
           
            root.SolveAll();
        }
        private static void Find(string input)
        {
            
            input = StringStuff.MyTrim(input);
            string temp = StringStuff.SplitUntil(input, ' ');
            input = StringStuff.MyTrim(StringStuff.MySubstring(input, temp.Length, input.Length - temp.Length));

            if (input[0] == '\"')
            {
                input = StringStuff.MySubstring(input, 1, input.Length - 2);
            }
            Console.WriteLine("\nFinding solution for: " + input);
            if(File.Exists(@input)) {
                StreamReader reader = new StreamReader(@input);
   
                string data = reader.ReadLine();
                int count = 0;
                int size = 1;
                for (int i = 0; i < (data.Length - 1) / 2; i++)
                {
                    size = size * 2;
                }
           
                
                bool[][] bools = new bool[size][];
                int requiredlength = data.Length;
                while (data != null)
                {
                    if (data.Length != requiredlength)
                    {
                        throw new ArgumentException("provided table is invalid");
                    }
                    bool[] tbool = new bool[(data.Length + 1) / 2];
                    int index = 0;
                    
                    for (int i = 0; i < data.Length; i += 2)
                    { 

                        if (data[i] == '1')
                        {
                            tbool[index] = true;
                            index++;
                        } else
                        {
                            tbool[index]= false;
                            index++;
                        }

                    }
                    bools[count] = tbool;
                    count++;

                    
                    data = reader.ReadLine();
                }

                if (count != size)
                {
                    throw new ArgumentException("provided table is invalid");
                }
                // list.CopyTo(values, 0);    


                //Console.WriteLine(count);
                
                Logic.FindFunction(bools);

            }
           
        }

    }
}
