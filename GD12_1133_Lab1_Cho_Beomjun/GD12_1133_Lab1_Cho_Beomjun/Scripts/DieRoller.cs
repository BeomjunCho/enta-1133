using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GD12_1133_Lab1_Cho_Beomjun.Scripts
{
    public class DieRoller
    {
        Random rnd = new Random();

        public void intro() // Intro messages, Logo and rules
        {
            Console.WriteLine("Welcome to Die vs Die!\nWhat is your name?");
            string userName = Console.ReadLine();
            Console.WriteLine("\nWelcome " + userName + "!");
            Console.WriteLine("\nRule:");
            Console.WriteLine("You are going to compete with CPU.");
            Console.WriteLine("First, You should decide a turn order.");
            Console.WriteLine("Pick the dice in your turn! Then, roll the dice.");
            Console.WriteLine("Game master will compare each score and let you know who win this game.\n");
            Console.WriteLine("Let's get started!");
        }

        public int getNumber() // Get number from user and change it to int. Then, return it
        {
            Console.WriteLine("Pick the dice to roll.(Type number // 6 sides, 8 sides, 12 sides, 20 sides)");
            string userChoice = Console.ReadLine();
           
                int diceSidesNum;
                bool diceCheck = int.TryParse(userChoice, out diceSidesNum);

                return diceSidesNum;  
        }

        public int roll(ref int diceSideNum) // generate random number and return it
        {
            Random rnd = new Random();
            int RanNum = rnd.Next(1, diceSideNum + 1);
            return RanNum;
        }

        public int compare(int a, int b) // compare a b and return winner's value
        {
            int A = a;
            int B = b;
            int draw = 0;

            if (A > B)
            {
                return a;
            }
            else if (A < B)
            {
                return b;
            }
            else
            {
                return draw;
            }

        }
    }
}
