using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GD12_1133_Lab1_Cho_Beomjun.Scripts
{
    public class DieRoller
    {
        Random rnd = new Random();
        int rounds = 0;

        public void Intro(Player user) // Intro messages, Logo and rules
        {
            string askii = @"
________  .__                       ________  .__        
\______ \ |__| ____   ___  ________ \______ \ |__| ____  
 |    |  \|  |/ __ \  \  \/ /  ___/  |    |  \|  |/ __ \ 
 |    `   \  \  ___/   \   /\___ \   |    `   \  \  ___/ 
/_______  /__|\___  >   \_//____  > /_______  /__|\___  >
        \/        \/            \/          \/        \/ 
                                                         
                                                                                                   
                                                         
            ";
            Console.WriteLine(askii);
            Console.WriteLine("Welcome to Die vs Die!\nFirst of all, I want to know your name. What is your name?\n");
            user.userName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nWelcome " + user.userName + "!");
            Console.ResetColor();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("\nRule:");
            Console.WriteLine(user.userName + ", you are going to compete with CPU.");
            Console.WriteLine("-Turn order would be decided randomly for each round.");
            Console.WriteLine("-Pick the dice in your turn! Then, roll the dice.");
            Console.WriteLine("-Game master will compare each score and let you know who is the winner on the round.");
            Console.WriteLine("Winner of the round will get points(higher roll number - lower roll number).");
            Console.WriteLine("-Total Rounds are 4 rounds and player who has more points will be final winner.\nPlus, there would be summery of your play at the end of this game.\nDon't miss it!!!\n");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }

        public void GamePlayLoop(Player user, Player cpu) // Main game play loop
        {
            for (int i = 0; i < 4; i++)
            {
                rounds++;
                Console.WriteLine("\n+++++++++This is Round " + (i + 1) + "+++++++++");

                int turnOrder = rnd.Next(1, 3); // turn decided every round

                if (turnOrder == 1) // First turn order
                {
                    Console.WriteLine("You are going to roll the dice firstly.");

                    userSequence(user);

                    Console.WriteLine("\n[[[Now it is Cpu turn!]]]");

                    cpuSequence(cpu);

                    compare(user, cpu); // Compare user score and cpu score
                }

                else // Second turn order
                {
                    Console.WriteLine("You are going to roll the dice Secondly.");

                    cpuSequence(cpu);

                    Console.WriteLine("\n[[[Now it is your turn!]]]");

                    userSequence(user);

                    compare(user, cpu); // Compare user score and cpu score
                }
            }
            // Final summary and ask user if they want to play again
            finalSummary(user, cpu);
            playAgain(user, cpu);
        }

        void userSequence(Player user) // show remaining dices and do user sequence
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n---------------------------------------------------------------------");
            Console.WriteLine("             Your dices                     ");
            Console.WriteLine(string.Join(", ", user.strDices));
            Console.WriteLine("---------------------------------------------------------------------\n");
            Console.ResetColor();
            dicePick(user); // user dice pick
            roll(user.DiceSides, user); // user roll
            evenOddCheck(user); // check the score is even or odd
            sequenceText(user);  // user sequence text
        }

        void cpuSequence(Player cpu) // cpu roll and show what cpu get to user
        {
            CpuDicePickRoll(cpu); //Cpu roll
            sequenceText(cpu); //cpu sequence text
        }

        void sequenceText(Player player) // Same sequence texts for all player
        {
            Console.WriteLine("\n" + player.userName + " chose ( " + player.DiceSides + " ) sides Dice");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("----------------------------------------------------@");
            Console.WriteLine("Rolling Dice...");
            Console.WriteLine("----------------------------------------------------@");
            Console.ResetColor();
            Console.WriteLine(player.userName + " 's dice score: [" + player.score + "]");
        }

        void finalSummary(Player user, Player cpu)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@");
            Console.WriteLine("\nYou just finished 4 rounds.");
            Console.WriteLine("\n" + user.userName + " Total Points: " + user.total);
            Console.WriteLine("\nCpu Total Points: " + cpu.total + "\n");
            Console.WriteLine("@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@\n");
            Console.ResetColor();

            if (user.total > cpu.total)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("You are the final winner. Congratuation!");
                Console.ResetColor();
                Console.WriteLine($"Your total dice roll is... {user.scoreSum}");
                Console.WriteLine($"Total dice roll average is... {(float)user.scoreSum / rounds}.");
                Console.WriteLine($"{user.userName} rolled {user.evenNum} even and {user.oddNum} odd numbers in this game.");

            }
            else if (user.total < cpu.total)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("It was close! Cpu is the winner. Try again!");
                Console.ResetColor();
                Console.WriteLine($"Your total dice roll is... {user.scoreSum}");
                Console.WriteLine($"Total dice roll average is... {(float)user.scoreSum / rounds}.");
                Console.WriteLine($"{user.userName} rolled {user.evenNum} even and {user.oddNum} odd numbers in this game.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Draw! Great match.");
                Console.ResetColor();
                Console.WriteLine($"Your total dice roll is... {user.scoreSum}");
                Console.WriteLine($"Total dice roll average is... {(float)user.scoreSum / rounds}.");
                Console.WriteLine($"{user.userName} rolled {user.evenNum} even and {user.oddNum} odd numbers in this game.");
            }

        }

        void playAgain(Player user, Player cpu)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Do you want to play again?(Yes: anykey, No: type no)\n");
            Console.ResetColor();
            string playAgain = Console.ReadLine().ToLower();
            if (playAgain == "no")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Thank you for playing this game, {user.userName}.");
                Console.WriteLine("I will look forward to meet you again.");
                Console.WriteLine("Game Over");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Good choice {user.userName}! Let's get started again!");
                Console.ResetColor();
                resetDices(user, cpu); // reset dices for new game
                GamePlayLoop(user, cpu);
            }
        } // Asking user if they want to play again

        void dicePick(Player player) // Pick the dice and check if it is used
        {
            Console.WriteLine("Pick the dice to roll.(Type number)");
            string userChoice = Console.ReadLine();
           
            int diceSidesNum;
            bool diceCheck = int.TryParse(userChoice, out diceSidesNum);
            if (diceSidesNum == 6 || diceSidesNum == 8 || diceSidesNum == 12 || diceSidesNum == 20) // Check if user choose right dice
            {
                if (diceSidesNum == 6 && player.haveDice6 == true) // User chose 6 sides dice AND has never used 6 sides dice
                {
                    player.numDices.Remove(diceSidesNum);
                    string diceSidesStr = $"{userChoice} sides dice";
                    player.strDices.Remove(diceSidesStr);
                    player.haveDice6 = false;
                    player.DiceSides = diceSidesNum;

                }
                else if (diceSidesNum == 8 && player.haveDice8 == true)
                {
                    player.numDices.Remove(diceSidesNum);
                    string diceSidesStr = $"{userChoice} sides dice";
                    player.strDices.Remove(diceSidesStr);
                    player.haveDice8 = false;
                    player.DiceSides = diceSidesNum;
                }
                else if (diceSidesNum == 12 && player.haveDice12 == true)
                {
                    player.numDices.Remove(diceSidesNum);
                    string diceSidesStr = $"{userChoice} sides dice";
                    player.strDices.Remove(diceSidesStr);
                    player.haveDice12 = false;
                    player.DiceSides = diceSidesNum;
                }
                else if (diceSidesNum == 20 && player.haveDice20 == true)
                {
                    player.numDices.Remove(diceSidesNum);
                    string diceSidesStr = $"{userChoice} sides dice";
                    player.strDices.Remove(diceSidesStr);
                    player.haveDice20 = false;
                    player.DiceSides = diceSidesNum;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You already used this dice. Please pick another one.");
                    Console.ResetColor();
                    dicePick(player);
                }
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You typed wrong number. Try again");
                Console.WriteLine("No dice selected.");
                Console.ResetColor();
                dicePick(player);
            }
        }

        public int roll(int diceSideNum, Player player) // generate random number and return it
        {
            Random rnd = new Random();
            int RanNum = rnd.Next(1, diceSideNum + 1);
            player.score = RanNum;
            player.scoreSum += player.score;
            return RanNum;
        }

        void CpuDicePickRoll(Player cpu) //Cpu pick the dice and roll it 
        {
            cpu.DiceSides = cpu.numDices[rnd.Next(cpu.numDices.Count)];
            cpu.score = rnd.Next(1, cpu.DiceSides + 1);
            cpu.numDices.Remove(cpu.DiceSides); // Delete previous dice in the list.
        } 


        void compare(Player user, Player cpu) // compare scores of players and add points in player instance
        {
            if (user.score > cpu.score)
            {
                int points = user.score - cpu.score;
                user.total += points;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nYou win! You got <" + points + "> points in your Total.");
                Console.WriteLine("User Total: " + user.total);
                Console.WriteLine("Cpu Total: " + cpu.total);
                Console.ResetColor();
            }
            else if (user.score < cpu.score)
            {
                int points = cpu.score - user.score;
                cpu.total += points;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou lose!\nCpu got <" + points + "> points in Total.");
                Console.WriteLine("User Total: " + user.total);
                Console.WriteLine("Cpu Total: " + cpu.total);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nDraw! No points for each players.");
                Console.WriteLine("User Total: " + user.total);
                Console.WriteLine("Cpu Total: " + cpu.total);
                Console.ResetColor();
            }

        }

        void evenOddCheck(Player user) // check even odd number and add player instance
        {
            if (user.score % 2 == 1)
            { 
                user.oddNum++;
            }
            else
            {
                user.evenNum++;
            }
        }

        void resetDices(Player user, Player cpu) // reset all parameters in player instance
        {
            cpu.numDices.Add(6);
            cpu.numDices.Add(8);
            cpu.numDices.Add(12);
            cpu.numDices.Add(20);
            user.numDices.Add(6);
            user.numDices.Add(8);
            user.numDices.Add(12);
            user.numDices.Add(20);
            user.haveDice6 = true;
            user.haveDice8 = true;
            user.haveDice12 = true;
            user.haveDice20 = true;
            user.strDices.Add("6 sides dice");
            user.strDices.Add("8 sides dice");
            user.strDices.Add("12 sides dice");
            user.strDices.Add("20 sides dice");
            user.total = 0;
            user.oddNum = 0;
            user.evenNum = 0;
            user.scoreSum = 0;
        }

    }
}
