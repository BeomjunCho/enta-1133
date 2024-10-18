using GD12_1133_Lab1_Cho_Beomjun.Scripts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static GD12_1133_Lab1_Cho_Beomjun.Scripts.Rooms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static GD12_1133_Lab1_Cho_Beomjun.Scripts.Monster;
using static GD12_1133_Lab1_Cho_Beomjun.Scripts.Items;

namespace GD12_1133_Lab1_Cho_Beomjun.Scripts
{

    internal class GameController
    {
        public void ProgramStart()
        {
            //instance
            Random rnd = new Random(); 
            Player user = new Player();
            AsciiArt ascii = new AsciiArt();
            StartingRoom emptyRoom = new StartingRoom();
            Room currentRoom;
            NormalMonster monster = new NormalMonster();
            FireScroll fireScroll = new FireScroll();
            ShieldScroll shieldScroll = new ShieldScroll();
            
           

            //funtion
            Map? Intro(Player user)
            {
                string userName = "";
                bool validName = false;
                bool validDiff = false;
                Map? map = null;

                ascii.PrintAscii(ascii.diceDungeon);
                Console.WriteLine("Welcome to dice dungeon!");
                Console.WriteLine("What is your name?");
                // ask username until getting proper name
                while (!validName)
                {
                    user.userName = Console.ReadLine() ?? string.Empty;
                    if (user.userName == "") // prevent user to type nothing
                    {
                        Console.WriteLine("Please enter a valid name.");
                        validName = false;
                    }
                    else
                    {
                        userName = user.userName;
                        validName = true;
                    }
                }
                //Rule
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nWelcome, {userName}, to the Dice Dungeon!");
                Console.WriteLine($"{userName}, you've been kidnapped and brought to the Dice Dungeon.");
                Console.WriteLine("This place is filled with rooms, each with its own challenges and mysteries.");
                Console.WriteLine("To survive, you’ll need to find hidden items in these rooms that will help you face the monsters ahead.");
                Console.WriteLine("Every item is tied to a different type of die.");
                Console.WriteLine("The effectiveness of these items depends entirely on your luck with the roll.");
                Console.WriteLine("For instance, a healing potion might be linked to a six-sided die.");
                Console.WriteLine("Roll the die, and the number you get determines how much health you recover.");
                Console.WriteLine("Your goal is to make it through the dungeon and defeat the boss monster to escape.");
                Console.WriteLine($"Good luck, {userName}!\n");
                Console.ResetColor();
                // ask difficulty level until getting proper level
                while (!validDiff)
                {
                    Console.WriteLine("Choose your difficulty - (easy = 3 x 3 map, medium = 6 x 6 map, hard = 9 x 9 map)");
                    Console.WriteLine("Enter 3 or 6 or 9 to proceed.");
                    string userChoice = Console.ReadLine() ?? string.Empty;
                    int difficulty;
                    if (int.TryParse(userChoice, out difficulty) && (difficulty == 3 || difficulty == 6 || difficulty == 9))
                    {
                        map = new Map(difficulty); // create map size based on difficulty choice
                        Console.WriteLine($"You've selected {userChoice} x {userChoice} map.");
                        validDiff = true;
                    }
                    else
                    {
                        Console.WriteLine("Please select a valid difficulty level. Enter 3 or 6 or 9");
                        validDiff = false;
                    }
                }
                return map;
            } // Explain game rule and get user name, difficulty level and return map instance

            //Main game loop
            void DiceDungeonGame(Map map)
            {
                while (user.hp > 0)
                {
                    map.MoveRoom(user);
                    currentRoom = map.currentRoom;
                    map.EnterRoom(currentRoom, user);
                    
                    if (currentRoom == map.lastRoom && user.hp > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nYou just defeated the boss monster and stepped through the door that was hidden behind it. \nCongratulations on conquering the dungeon!");
                        ascii.PrintAscii(ascii.escape);
                        ascii.PrintAscii(ascii.angel);
                        Console.ResetColor();
                        break;
                    }
                    else if (user.hp <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("-----------You Died-------------");
                        ascii.PrintAscii(ascii.rip);
                        ascii.PrintAscii(ascii.gameOver);
                        Console.ResetColor();
                        break;
                    }
                }
            }

            //------------------Game Start---------------------
            
            Map? map = Intro(user); // execute intro() and catch the returned map instance
            if (map != null) // Check if map is null
            {
                currentRoom = map.currentRoom;
                map.EnterRoom(currentRoom, user);
                DiceDungeonGame(map); // Main game loop
            }
            else
            {
                Console.WriteLine("Failed to initialize map. Exiting the game.");
            }


        }


    }

}


