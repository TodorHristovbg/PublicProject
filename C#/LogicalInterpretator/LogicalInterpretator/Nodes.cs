using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LogicalInterpretator
{
    internal class Nodes
    {

        internal bool? Value { get; set; }
        internal bool ReEval { get; set; } = true;
        public string? Name { get; set; }
        internal string? operation { get; set; }
        internal Nodes? input1 { get; set; }
        internal Nodes? input2 { get; set; }
        internal Nodes? parent { get; set; }

        internal Point location { get; set; } = new Point();
        public Nodes() {



        }
        internal bool Evaluate()
        {
            if (operation != null)
            {
                if (ReEval == true)
                {
                    ReEval = false;
                    switch (operation)
                    {
                        case "&":

                            //        Console.WriteLine("setting value of &");
                            Value = input1.Evaluate() & input2.Evaluate();
                            //         Console.WriteLine("{0} AND {1} -> {2}", input1.Name, input2.Name, Value);
                            //    Console.WriteLine("{0} AND {1} -> {2}", input1.Evaluate(), input2.Evaluate(), Value);
                            return (bool)Value;

                        case "|":

                            //      Console.WriteLine("setting value of |");
                            Value = input1.Evaluate() | input2.Evaluate();
                            //      Console.WriteLine("{0} OR {1} -> {2}", input1.Name, input2.Name, Value);
                            //      Console.WriteLine("{0} OR {1} -> {2}",input1.Evaluate(),input2.Evaluate(),Value);
                            return (bool)Value;

                        case "!":

                            if (input1 == null)
                            {

                                Value = !input2.Evaluate();
                                //        Console.WriteLine("NOT {0} -> {1}", input2.Name, Value);
                                //      Console.WriteLine("NOT {0} -> {1}",input2.Evaluate(), Value);
                                return (bool)Value;
                            }
                            //    Console.WriteLine("setting value of !");
                            Value = !input1.Evaluate();
                            //       Console.WriteLine("NOT {0} -> {1}", input1.Name, Value);
                            //      Console.WriteLine("NOT {0} -> {1}", input1.Evaluate(), Value);
                            return (bool)Value;


                        default:
                            Console.WriteLine("SHOULDNT GET HERE DEFAULT");
                            break;


                    }
                }
                else
                    // Console.WriteLine("Dont need to ReEval");
                    return (bool)Value;
            }

            return (bool)Value;

        }

        internal static void SingleEval(Nodes root)
        {
            Console.WriteLine("input1 Value: " + root.input1.Value);
            Console.WriteLine("input2 Value: " + root.input2.Value);

            if (root.input1.Value != null && root.input2.Value != null)
            {

                switch (root.operation)
                {
                    case "&":
                        Console.WriteLine("input1 Value: " + root.input1.Value);
                        Console.WriteLine("input2 Value: " + root.input2.Value);
                        Console.WriteLine("input1 HasValue: " + root.input1.Value.HasValue);
                        Console.WriteLine("input2 HasValue: " + root.input2.Value.HasValue);
                        Console.WriteLine("Logical AND result: " + (root.input1.Value & root.input2.Value));

                        break;
                    case "|":
                        Console.WriteLine("Operation is | and inputs are " + root.input1.Value + " and " + root.input1.Value + " result is : " + (root.input1.Value | root.input2.Value));

                        break;
                    case "!":



                        break;

                    default:
                        Console.WriteLine("SHOULDNT GET HERE DEFAULT");
                        break;

                }
            }
        }
        internal static void PrintTree(Nodes node)
        {
            Console.WriteLine((node.Name != node.operation && node.operation != null ? node.operation : node.Name) + " Connects to: "
              + (node.input1?.Name ?? "null")
              + " and "
              + (node.input2?.Name ?? "null"));



            if (node.input1 != null)
            {

                PrintTree(node.input1);
            }
            if (node.input2 != null)
            {

                PrintTree(node.input2);
            }
        }

        internal static MyList<Nodes> getTree(Nodes node, MyList<Nodes> tree)
        {

            if (node.input1 != null)
            {

                tree.Add(node.input1);
            }
            if (node.input2 != null)
            {

                tree.Add(node.input2);
            }
            return tree;
        }
        internal static void CheckParents(Nodes root)
        {
            if (root.input1 != null)
            {
                if (root.input1.parent != root)
                {
                    Console.WriteLine("MISMATCH " + root.Name + " and " + root.input1.Name);
                }
                CheckParents(root.input1);
            }
            if (root.input2 != null)
            {
                if (root.input2.parent != root)
                {
                    Console.WriteLine("MISMATCH " + root.Name + " and " + root.input2.Name);
                }
                CheckParents(root.input2);
            }


        }

        internal static void setValues(bool[] values, Nodes root)
        {


            if (root.operation == null)
            {
                //     Console.WriteLine(root.Name + " is being set to index " + (root.Name[0] - 97) + " value: " + (values[root.Name[0]-97]));
                if (root.Value != values[root.Name[0] - 97])
                {
                    Nodes temp = root;
                    root.Value = values[root.Name[0] - 97];
                    while (temp.parent != null)
                    {
                        if (temp.parent.operation != null)
                        {
                            if (temp.parent.ReEval == true)
                            {
                                break;
                            } else
                            {
                                temp.parent.ReEval = true;
                            }
                        }
                        temp = temp.parent;
                    }
                }
            }

            if (root.input1 != null)
            {

                setValues(values, root.input1);
            }
            if (root.input2 != null)
            {
                setValues(values, root.input2);
            }

        }

        internal static void ResetValues(Nodes root)
        {
            root.Value = null;
            if (root.input1 != null)
            {
                ResetValues(root.input1);
            }
            if(root.input2 != null)
            {
                ResetValues(root.input2);
            }
        }
        internal bool[][] SolveAll()
        {
            int length = StringStuff.getOperands(Name).Length;
            bool[] values = new bool[length];
            int size = 1;
            for (int i = 0; i < length; i++)
            {
                size = size * 2;
            }
            //  Console.WriteLine(size);
            //  Console.WriteLine(length * length);
            bool[][] results = new bool[size][];




            RecursiveSolve(this, values, results, 0);

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < length; j++)
                {

                    Console.Write(results[i][j] + " ");
                }
                Console.Write("  -> " + results[i][length]);
            }

            Console.WriteLine();

            return results;
        }

        private void RecursiveSolve(Nodes root, bool[] values, bool[][] results, int index)
        {

            if (index == values.Length)
            {
                setValues(values, root);

                bool[] output = new bool[values.Length + 1];
                for (int i = 0; i < values.Length; i++)
                {
                    output[i] = values[i];
                }
                output[output.Length - 1] = root.Evaluate();

                for (int j = 0; j < results.Length; j++)
                {
                    if (results[j] == null)
                    {
                        results[j] = output;
                        return;
                    }
                }

            }


            values[index] = false;
            RecursiveSolve(root, values, results, index + 1);

            values[index] = true;
            RecursiveSolve(root, values, results, index + 1);

        }

        internal static int height(Nodes root)
        {
            if (root == null)
            {
                return 0;
            }
            else
            {

                // Compute height of each subtree
                int lheight = height(root.input1);
                int rheight = height(root.input2);

                // use the larger one
                if (lheight > rheight)
                {
                    return (lheight + 1);
                }
                else
                {
                    return (rheight + 1);
                }
            }
        }

        



    }
}

