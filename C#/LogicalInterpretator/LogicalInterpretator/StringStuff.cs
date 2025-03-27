using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LogicalInterpretator
{
    internal  class StringStuff
    {
        static public MyList<String> TokenizeForDefine(string input)
        {
          
            MyList<String> tokens = new MyList<string>();
            input = MyTrim(input);
            tokens.Add(StringStuff.SplitUntil(input, ' '));
            
            
            input = MyTrim(MySubstring(input, tokens[0].Length, input.Length - tokens[0].Length));
            tokens.Add(StringStuff.SplitUntil(input, ':'));
            CheckInputOrder(tokens[1]);


            input = MyTrim(MySubstring(input, tokens[1].Length+1, input.Length - tokens[1].Length-1));
            input = MySubstring(input, 1, input.Length - 2);
            tokens.Add(input);

            
 
            return tokens;
        }
        static internal MyList<string> TokenizeForSolve(string input)
        {
            MyList<String> tokens = new MyList<string>();
            input = MyTrim(input);
            string temp = StringStuff.SplitUntil(input, ' ');
                    
            input = MyTrim(MySubstring(input, temp.Length, input.Length - temp.Length));
            tokens.Add(StringStuff.SplitUntil(input, '('));
           

            input = MyTrim(MySubstring(input, tokens[0].Length, input.Length - tokens[0].Length));
            tokens.Add(input);

          
            StringBuilder fourthtoken = new StringBuilder(tokens[1]);
            MyList<bool> values = new MyList<bool>();
            char cha = 'a';
            for (int i = 0; i < tokens[1].Length; i++)
            {
               
                if (tokens[1][i] == '1'|| tokens[1][i] == '0')
                {
                    
                    fourthtoken[i] = cha;
                    cha++;
                    if (tokens[1][i] == '1')
                    {
                        values.Add(true);
                    } else
                    {
                        values.Add(false);
                    }

                }
            }
            string final = tokens[0]+fourthtoken.ToString();

            GlobalDic.getTable().TryGetValue(final, out var root);
            Nodes.setValues(values.toArray(), root);
            Console.WriteLine(root.Evaluate());

            foreach (string item in tokens)
            {
                Console.WriteLine(item);
            }
            return tokens;
        }

        static internal string MySubstring(string input, int startindex, int length)
        {
            if (input == null)
            {
               throw new ArgumentNullException("cant be null");
            }
            if (input.Length < startindex + length)
            {
                throw new ArgumentOutOfRangeException("substring goes outside of range");
            }
            

            string output = "";

            for (int i = startindex; i < length + startindex; i++)
            {
                output += input[i];
            }
            return output;
        }
        static internal string MySubstring(string input, int startindex)
        {
            if (input == null)
            {
                throw new ArgumentNullException("cant be null");
            }

            if (input.Length < startindex)
            {
                throw new ArgumentOutOfRangeException("substring goes outside of range");
            }

            if (input.Length == startindex)
            {
                return "";
            }


            string output = "";

            for (int i = startindex; i <input.Length; i++)
            {
                output += input[i];
            }


            return output;
        }
        static internal bool MyContains(string source, string input)
        {
            if (source == null || input == null)
            {
                throw new ArgumentNullException();
            }
           
            if (input.Length > source.Length)
            {
                
                return false;
            }
            if(input.Length == source.Length)
            {
                if (source == input)
                {
                    
                    return true;
                    
                }
               
                return false;
            }
            for(int i =0; i < source.Length - input.Length+1; i++)
            {
               // Console.WriteLine("Comparing with " + MySubstring(source, i, input.Length));
                if (MySubstring(source,i,input.Length) == input)
                {
                    
                    return true;
                }
            }
           
            return false;
        }
        static internal bool MyContains(string source, char input)
        {
            Console.WriteLine(source);
            for (int i = 0; i < source.Length; i++)
            {
                Console.WriteLine(source[i]);   
                if (source[i] == input)
                {
                    Console.WriteLine("comparing: {0} and {1}",source[i],input);
                    return true;
                }
            }
            return false;
        }
        static internal bool MyContains(string source, char input, [MaybeNullWhen(false)] out int index)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == input)
                {
                    index = i;
                    return true;
                }
            }
            index = default;
            return false;
        }
        static internal string[] MyStringSplitIntoarr(string input, char splitter)
        {
            MyList<string> list = new MyList<string>();

            String temp = "";
            while (input != temp && input!="")
            {
                temp = SplitUntil(input, splitter);
                list.Add(temp);
                input = MySubstring(input, temp.Length + 1, input.Length - temp.Length - 1);
            }


            string[] output = new string[list.Count];
       
            
                list.CopyTo(output, 0);
               
           
            return output;
        }
       
        static internal bool ArrContains<T>(T[] array, T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for(int i=0; i<array.Length; i++)
            {
                if (array[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }
        static internal string MyTrim(string input)
        {
            string output;
            int leadingcounter=0;
            int trailingcounter=0;
            if (input[0]==' ')
            {
                int i = 1;
                leadingcounter = 1;
              while(i<input.Length && input[i]==' ')
                {
                    i++;
                    leadingcounter++;
                }
                if (leadingcounter == input.Length)
                {
                    return null;
                }
            }

            if (input[input.Length-1]==' ')
            {
                int i = 2;
                trailingcounter = 1;
                while (input[input.Length-i]==' ')
                {
                    
                  
                    i++;
                    trailingcounter++;
                    
                }
            }

            //   Console.WriteLine("LEADING " + leadingcounter);
            //  Console.WriteLine("TRAILING " + trailingcounter);
            return  MySubstring(input, leadingcounter, input.Length - leadingcounter - trailingcounter);
          
            
            

        }
        static internal string SplitUntil(string input, char end)
        {
            string output = "";

            if (input == null || input.Length == 0)
            {
                throw new ArgumentNullException("input cant be null or empty");
            }
            for (int i = 0; i < input.Length; i++)
            {


                if (input[i] == end)
                {
                    return output;
                }
                output += input[i];
            }
           
            return input;
        }
        static internal MyList<String> MyStringSplit(string input, char splitter)
        {
            MyList<String> tokens = new MyList<string>();
            bool tokenfound = false;
            int indexstart = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == splitter)
                {
                    if (tokenfound == true)
                    {
                       tokens.Add(MySubstring(input, indexstart, i - indexstart));
                       // tokens.Add(input.Substring(indexstart, i - indexstart));
                        tokenfound = false;
                    }
                }
                else
                {
                    if (tokenfound == false)
                    {
                        indexstart = i;
                        tokenfound = true;
                    }
                    tokenfound = true;
                }

            }

            if (tokenfound == true)
            {
                tokens.Add(MySubstring(input, indexstart, input.Length - indexstart));
              //  tokens.Add(input.Substring(indexstart, input.Length - indexstart));
            }
            return tokens;
        }
        static internal string SplitUntil(string input, string end)
        {
            string output = "";

            if (input == null || input.Length == 0)
            {
                throw new ArgumentNullException("input cant be null or empty");
            }
            for (int i = 0; i < input.Length-end.Length; i++)
            {
              //  MySubstring(input, i, end.Length).Equals(end)
              //  input.Substring(i, end.Length.Equals(end))
                if (MySubstring(input, i, end.Length).Equals(end))
                {
                    return output;
                }
                output += input[i];
            }

            return input;
        }

        static internal MyList<Nodes> prettytree(Nodes root)
        {
            MyList<Nodes> nodes = new MyList<Nodes>();

        
            int height = Height(root, 1)+1;
            Console.WriteLine(height);
            return nodes;

        }
      
        static int Height(Nodes node,int height)
        {
            if (node.input1 != null)
            {
                height++;
                Height(node.input1, height);
            }
            return height;
        }
        static internal String getOperands(string input)
        {
            
            string result = "";
            int i = 0;
           
          

           // Console.WriteLine("taking in: " + input);
            MyContains(input, '(', out i);
            MyContains(input, ')', out int k);
            if (i != 0 && k!=0) { 

             input = MySubstring(input, i + 1, k-i);
              //  Console.WriteLine("AFTER SUBSTRING : " + input);
            
            }
            
            for (int j = 0; j < input.Length - 1; j++)
            {
                if (input[j] != ' ' && input[j] != ',')
                {
                    result += input[j];
                }
     
            }
           // Console.WriteLine("ALLOWED INPUTS ARE: "+result);
            return result;
        }
        internal static string HandleBrackets(string input)
        {
            int counter = 0;
            if (input[0] != '(')
            {
                return input;
            }
            for(int i = 0 ; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    counter++;
                }
                if (input[i] == ')')
                {
                    counter--;
                 //   Console.WriteLine("COUNTER IS "+ counter);
                    if (counter == 0)
                    {
                       // Console.WriteLine("RETURNING " + input.Substring(1, i - 1));
                  //      Console.WriteLine("RETURNING " + MySubstring(input,1, i - 1));
                       // return input.Substring(1, i - 1);
                        return MySubstring(input,1, i - 1);

                    }
                }
               
            }
            throw new ArgumentException("INVALID INPUT");
        }
        internal static bool isSubSet(string sub,string main)
        {
            for(int i =0 ; i < sub.Length; i++)
            {
                if(!MyContains(main, sub[i]))
                {
                    return false;
                }
            }
            return true;
        }
        internal static bool CheckInputOrder(string input)
        {
           // Console.WriteLine(input + "  INPUT WE ARE GETTING");
            char start = 'a';
            input=getOperands(input);
            for(int i = 0; i < input.Length; i++)
            {
               
                if(input[i] != start)
                {
                    Console.WriteLine("INCORRECT ORDER OF INPUTS {0} expected after {1}", start, input[i-1]);
                    return false;
                }
                start++;
            }
            return true;
        }
        
        static internal void PrintArray(string[,] input)
        {
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    {
                        Console.Write(input[i, j]+" ");
                    }
                }
                Console.WriteLine();
            }
        }
        static internal Nodes makeroot(string input, string allowedinputs)
        {
          
            Nodes root = new Nodes();
          
      
            while (input.Length > 0)
            {
                input = input.Trim();
                String temptoken = "";
                Nodes tempnode = new Nodes();
                if (input[0] == '(')
                {
                    if (input[input.Length-1] == ')')
                    {
                        String checkforbs = HandleBrackets(input);
                        if (checkforbs.Length + 2 == input.Length && root.operation==null)
                        {
                          //  Console.WriteLine("bullshit found");
                        //    Console.WriteLine(input);
                            input = MySubstring(input, 1, input.Length - 2);
                         //   Console.WriteLine("new input is: " + input);
                            continue;
                        }
                      //  input = MySubstring(input,1, input.Length - 2);
                       // Console.WriteLine("new input is: " + input);
                        //continue;
                    }
                    temptoken = HandleBrackets(input);
                   // Console.WriteLine("INPUT IS NOW{0} AND TEMPTOKEN IS {1}", input, temptoken);
                   
                    input=MySubstring(input,temptoken.Length+2,input.Length-temptoken.Length-2);
                    // Console.WriteLine("INPUT IS NOW{0} AND TEMPTOKEN IS {1}", input, temptoken);
                    if (root.input1 == null)
                    {
                        root.input1 = makeroot(temptoken, allowedinputs);
                        root.input1.parent = root;
                    }
                    else if (root.input2 == null) 
                    {                                                  //izkarva node bez ime ako zapochnem sus skobi ot dvete strani
                        root.input2 = makeroot(temptoken, allowedinputs);
                        root.input2.parent = root;
                    }
                    continue;

                }  else  if(SplitUntil(input, ' ').Length > 2)
                   {

                    temptoken = SplitUntil(input, ")")+')';

                    Console.WriteLine("subfunction " + temptoken + " found in dictonary");
               
                    if (allowedinputs.Length < getOperands(temptoken).Length)
                    {
                        throw new Exception("Subfunction requires more operands than are given");
                    }
                    if (GlobalDic.getTable().TryGetValue(temptoken, out tempnode))
                    {
                        if (root.input1 == null)
                        {
                            root.input1 = tempnode;
                            root.input1.parent = root;
                        }
                        else if (root.input2==null)
                        {
                           
                           root.input2 = tempnode;
                           root.input2.parent = root;
                        } else
                        {
                            throw new Exception("INVALID FUNCTION BODY WHEN USING FUNCTION INSIDE");
                        }

                     
                        input =MyTrim(MySubstring(input,temptoken.Length,input.Length-temptoken.Length));
                       
                        
                        continue;
                    } else
                    {
                        throw new Exception("INVALID FUNCTION IN BODY");
                    }
                }

               
                    temptoken = SplitUntil(input, ' ');
                    input = MySubstring(input, temptoken.Length, input.Length - temptoken.Length);
                    tempnode.Name = temptoken;
                

                
            
                

                if (temptoken[0] == '!')
                {
                    if (root.operation == null)
                    {
                        root.operation = "!";
                        root.Name = "!";
                        root.input1 = new Nodes();
                        root.input1.Name = MySubstring(temptoken, 1);
                        root.input1.parent = root;
                        continue;
                    }
                    else
                    {
                        Nodes NOTnode = new Nodes();
                        NOTnode.Name = "!";
                        NOTnode.operation = "!";
                        NOTnode.input1 = new Nodes();
                        NOTnode.input1.Name = MySubstring(temptoken,1);
                        NOTnode.input1.parent=NOTnode;
   
                        root.input2 = NOTnode;
                        root.input2.parent = root;
                        
                        continue;
                    }
                }

                if (temptoken=="&" || temptoken == "|")
                {
                    if(root.operation== null)
                    {
                        root.operation = temptoken;
                        root.Name = temptoken;
                        continue;
                    } else
                    {
                     //   Console.WriteLine("Found higher operation, setting to " + temptoken);
                      //  Console.WriteLine("first input will be "+ root.operation);
                        tempnode.operation = temptoken;
                        Nodes swap = new Nodes();
                        swap = root;
                        root = tempnode;
                        tempnode.input1 = swap;
                        root.input1.parent = root;
                        

                       // Console.WriteLine("First operand of NEW found function is " + root.input1.operation);
                        continue;
                    }
                }




                if (!MyContains(allowedinputs, tempnode.Name)) 
                {
                    throw new Exception("INVALID OPERAND IN FUNCTION BODY");
                } 
                
                
                    if (root.input1 == null) { 
                        root.input1 = tempnode;
                        root.input1.parent = root;
                    //    Console.WriteLine("Found first operand, setting to " + temptoken);
                        root.input2 = null;
                    }
                    else
                    {
                    if (root.input2 != null && root.input2.operation == null)   
                    {
                       
                        //  Console.WriteLine("input 2 not null what? " + root.input2.Name);
                        //Console.WriteLine("tempnode about to overwrite it is: " + tempnode.Name);


                        throw new Exception("INVALID FUNCTION BODY");
                    }
                   
                    
                      //  Console.WriteLine("Found second operand for " + root.operation + " setting to: " + temptoken);
                        root.input2 = tempnode;
                        root.input2.parent = root;
                    
                    }
                

            }

          
            return root;
        }

    }

}
