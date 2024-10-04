using GD12_1133_Lab1_Cho_Beomjun.Scripts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GD12_1133_Lab1_Cho_Beomjun.Scripts
{

    internal class GameController
    {

        public void ProgramStart()
        {
            // Instances
            DieRoller dieRoller = new DieRoller();
            Player user = new Player(); 
            Player cpu = new Player();
            Random rnd = new Random();

            //Data
            bool UserReady = false; // User will be asked to change it to true and play the game
            cpu.userName = "cpu"; // Set cpu player name

            // Start game and loop to check if user is ready
            dieRoller.Intro(user);
            while (!UserReady)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nARE YOU READY? (Yes: ready / No: no)");
                Console.ResetColor();
                string ready = Console.ReadLine().ToLower();

                if (ready == "ready")
                {
                    Console.WriteLine("\nThat's great. Let's get started!\n");
                    UserReady = true;
                    dieRoller.GamePlayLoop(user, cpu);
                }

                else if (ready == "no")
                {
                    Console.WriteLine("Ok. Maybe Next time! Bye!");
                    UserReady = true;
                    return;
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You typed wrong texts. Try again.");
                    Console.ResetColor();
                    UserReady = false;
                }
               
            }
        
        }

    }

}


