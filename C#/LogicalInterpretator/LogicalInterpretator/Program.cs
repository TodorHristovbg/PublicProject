using LogicalInterpretator;
using System.Security.Cryptography.X509Certificates;

class Prgoram
{

    static void Main()
    {
        

        

        String newfunction = "DEFINE JLOL(a, b, c): \"a & b | c \"";
        String testing = "   DEFINE func3(a, b, c, d): \"a & b | (c | a | a) & !d\"";
        String moretesting = "   DEFINE func4(a, b, c, d): \"( ((!a & !c & d ) | ( !a & b & d ) | ( !a & b & c ) | ( !b & !d ) | (c & !d )))\"";

         Validate.HandleCommand(newfunction);
         Validate.HandleCommand(testing);
         Validate.HandleCommand(moretesting);


        // GlobalDic.getTable().TryGetValue("func3(a, b, c, d)", out var root);


        // Validate.HandleCommand("ALL JLOL(a, b, c)");
        // Validate.HandleCommand("ALl func3(a, b, c, d)");
        // Validate.HandleCommand(" FIND \"C:\\Users\\Acer\\Desktop\\tablewithstuff.txt\"");



        // Validate.HandleCommand(moretesting);

      
        GlobalDic.getTable().TryGetValue("JLOL(a, b, c)", out var jlol);
        GlobalDic.getTable().TryGetValue("func3(a, b, c, d)", out var root);
         GlobalDic.getTable().TryGetValue("func4(a, b, c, d)", out var secondroot);
        bool[] values = new bool[4];
        values[0] = true;
        values[1] = true;
        values[2] = false;
        values[3] = true;
       
        Logic.DrawTree(root);
        Logic.DrawSolvedTree(root, values);

        bool reading = true;
        while (reading)
        {
            string input = Console.ReadLine();
            if (input == "-1")
            {
                reading = false;
                break;
            }
                Validate.HandleCommand(input);
            
        }
    }
   

}