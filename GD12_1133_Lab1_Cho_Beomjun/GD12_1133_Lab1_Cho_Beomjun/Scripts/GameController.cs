using GD12_1133_Lab1_Cho_Beomjun.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD12_1133_Lab1_Cho_Beomjun.Scripts
{
    /*internal class GameController
    {
        public static void ProgramStart()
        {

        }
    }*/

}

internal class GameController
{
    public static void ProgramStart()
    {
        DieRoller dieRoller = new DieRoller();
        dieRoller.Description();
        int total = dieRoller.TotalScores();
        Console.WriteLine("Your total score is " + total);

        Console.WriteLine(" -----------------------------------------------------");
        Console.WriteLine("|Research and Explanation about aristhmeic operators. |");
        Console.WriteLine(" -----------------------------------------------------");
        Console.WriteLine("Addition operator + : Adds together two values");
        Console.WriteLine("Subtraction operator - : Subtracts one value from another");
        Console.WriteLine("Division operator / : Divides one value by another");
        Console.WriteLine("Multiplication operator * : Multiplies two values");
        Console.WriteLine("Increment operator ++ : Increases the value of a variable by 1");
        Console.WriteLine("Decrement operator -- : Decreases the value of a variable by 1");
        Console.WriteLine("Remainder operator % : Computes the remainder after dividing its left-hand operand by its right-hand operand.");
        Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
        Console.WriteLine("Good bye!");
    }

}

