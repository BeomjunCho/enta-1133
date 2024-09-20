using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD12_1133_Lab1_Cho_Beomjun.Scripts
{
    public class DieRoller
    {
        public int numberOfSides = 6;

        public void Description()
        {
            Console.WriteLine("Welcome! Jun 240919");
            Console.WriteLine("This is a game about rolling dice with " + numberOfSides + " sides");
            Console.WriteLine("Min Roll:");
            Console.WriteLine(1);
            Console.WriteLine("Max Roll:");
            Console.WriteLine(numberOfSides);
        }

        public int RollDice()
        {
            Random rnd = new Random();
            int RanNum = rnd.Next(1, numberOfSides + 1);
            return RanNum;
        }

        public int TotalScores()
        {
            int Total = 0;
            int Num = 1;
            for (int i = 0; i < 4; i++)
            {   
                Total += RollDice();
                Console.WriteLine("Dice number " + (Num + i) + ":" + RollDice());
            }
            return Total;
        }
    }
}

