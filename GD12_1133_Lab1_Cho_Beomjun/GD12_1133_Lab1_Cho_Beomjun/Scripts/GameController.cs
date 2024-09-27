using GD12_1133_Lab1_Cho_Beomjun.Scripts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GD12_1133_Lab1_Cho_Beomjun.Scripts
{
    
}

internal class GameController
{
    public static void ProgramStart()
    {
        //List
        var cpuDices = new List<int> { 6, 8, 12, 20 }; // CPU dices

        //Instances
        DieRoller dieRoller = new DieRoller();
        Player user = new Player();
        Player cpu = new Player();
        Random rnd = new Random();

        //Data
        int UserDice;
        int CpuDice;
        int UserScore;
        int CpuScore;
        int gameRound = 0;

        // Functions
        void UserDicePick() // Pick the dice and check if it is used
        {
            UserDice = dieRoller.getNumber();   // User decides dice sides and change it(String) to int
            if (UserDice == 6 || UserDice == 8 || UserDice == 12 || UserDice == 20) // Check if user choose right dice
            {
                if (UserDice == 6 && user.haveDice6) // User chose 6 sides dice AND has never used 6 sides dice
                {
                    user.haveDice6 = false; // 6 sides dice is used in user instance
                    UserDice = 6; // Set user dice to 6 sides dice
                }
                else if (UserDice == 8 && user.haveDice8)
                {
                    user.haveDice8 = false;
                    UserDice = 8;
                }
                else if (UserDice == 12 && user.haveDice12)
                {
                    user.haveDice12 = false;
                    UserDice = 12;
                }
                else if (UserDice == 20 && user.haveDice20)
                {
                    user.haveDice20 = false;
                    UserDice = 20;
                }
                else
                {
                    Console.WriteLine("You already used this dice. Please pick another one.");
                    UserDicePick();
                }
            }

            else
            {
                Console.WriteLine("You typed wrong number. Try again");
                Console.WriteLine("No dice selected.");
                UserDicePick();
            }
        } // Pick the dice and check if it is used

        void DiceRoll() // Roll dice
        {
            UserScore = dieRoller.roll(ref UserDice);    // Generate random number in dice(Roll the dice)
            user.score = UserScore;    // Set score value in user instance
        } // Roll dice

        void CpuDicePickRoll() //Cpu pick the dice and roll it 
        {
            CpuDice = cpuDices[rnd.Next(cpuDices.Count)];
            CpuScore = dieRoller.roll(ref CpuDice);
            cpu.score = CpuScore;
            cpuDices.Remove(CpuDice); // Delete previous dice in the list.
        } //Cpu pick the dice and roll it

        //Intro
        dieRoller.intro();

        //Game play loop until round 4
        while (gameRound < 4) 
        {   
            gameRound++; // Round + 1
            Console.WriteLine("\n+++++++++This is Round " + gameRound + "+++++++++");
            Console.WriteLine("\nWe need to choose turn order.");
            Console.WriteLine("Do you want to roll the dice firstly or Secondly? (Fisrt: 1 /// Second: 2)");
            string turnOrder = Console.ReadLine(); // Choose turn order

            if (turnOrder == "1") // Firstly roll
            {
                Console.WriteLine("You are going to roll the dice firstly.");
                UserDicePick(); // User roll
                DiceRoll();
                Console.WriteLine("You chose ( " + UserDice + " ) sides Dice");
                Console.WriteLine("Rolling Dice...");
                Console.WriteLine("\nYour dice score: [" + user.score + "]");
                Console.WriteLine("\nNow it is Cpu turn!\nDo you want cpu to roll the dice? (Yes: 1 /// End this game: Anykey)");
                string LetsGo = Console.ReadLine();

                if (LetsGo == "1") // Keep playing
                {
                    CpuDicePickRoll(); //Cpu roll
                    Console.WriteLine("Cpu chose ( " + CpuDice + " ) sides Dice");
                    Console.WriteLine("Rolling Dice...");
                    Console.WriteLine("\nCpu dice score: [" + cpu.score + "]");

                    int Winner = dieRoller.compare(user.score, cpu.score); // Compare scores and decide who is winner
                    if (Winner == user.score)
                    {
                        int points = user.score - cpu.score;
                        user.total += points;
                        Console.WriteLine("\nYou win! You got <" + points + "> points in your Total.");
                        Console.WriteLine("User Total: " + user.total);
                        Console.WriteLine("Cpu Total: " + cpu.total);
                    }
                    else if (Winner == cpu.score)
                    {
                        int points = cpu.score - user.score;
                        cpu.total += points;
                        Console.WriteLine("\nYou lose!\nCpu got <" + points + "> points in Total.");
                        Console.WriteLine("User Total: " + user.total);
                        Console.WriteLine("Cpu Total: " + cpu.total);
                    }
                    else
                    {
                        Console.WriteLine("\nDraw! No points for each players.");
                        Console.WriteLine("User Total: " + user.total);
                        Console.WriteLine("Cpu Total: " + cpu.total);
                    }
                }
                else // Stop playing
                {
                    Console.WriteLine("\nYou stopped this game.");
                    gameRound = 100;
                }
                   
            }

            if (turnOrder == "2") // Secondly roll
            {
                Console.WriteLine("You are going to roll the dice Secondly."); 
                CpuDicePickRoll(); //Cpu roll
                Console.WriteLine("Cpu chose ( " + CpuDice + " ) sides Dice");
                Console.WriteLine("Rolling Dice...");
                Console.WriteLine("\nCpu dice score: [" + cpu.score + "]");
                Console.WriteLine("\nNow it is your turn!\nDo you want to roll the dice? Yes: 1 /// End this game: Anykey");
                string LetsGo = Console.ReadLine();

                if (LetsGo == "1") // Keep playing
                {
                    UserDicePick(); //User roll
                    DiceRoll();
                    Console.WriteLine("You chose ( " + UserDice + " ) sides Dice");
                    Console.WriteLine("Rolling Dice...");
                    Console.WriteLine("\nYour dice score: [" + user.score + "]");

                    int Winner = dieRoller.compare(user.score, cpu.score); // Compare scores and decide who is winner
                    if (Winner == user.score)
                    {
                        int points = user.score - cpu.score;
                        user.total += points;
                        Console.WriteLine("\nYou win! You got <" + points + "> points in your Total.");
                        Console.WriteLine("User Total: " + user.total);
                        Console.WriteLine("Cpu Total: " + cpu.total);
                    }
                    else if (Winner == cpu.score)
                    {
                        int points = cpu.score - user.score;
                        cpu.total += points;
                        Console.WriteLine("\nYou lose!\nCpu got <" + points + "> points in Total.");
                        Console.WriteLine("User Total: " + user.total);
                        Console.WriteLine("Cpu Total: " + cpu.total);
                    }
                    else
                    {
                        Console.WriteLine("\nDraw! No points for each players.");
                        Console.WriteLine("User Total: " + user.total);
                        Console.WriteLine("Cpu Total: " + cpu.total);
                    }
                }
                else // Stop playing
                {
                    Console.WriteLine("You stopped this game.");
                    gameRound = 100;
                }
            }
        } 
        
        //Final total scores
        Console.WriteLine("\nUser Total Final: " + user.total);
        Console.WriteLine("Cpu Total Final: " + cpu.total);
        
        // Compare totals and writeline
        if (user.total > cpu.total)
        {
            Console.WriteLine("\nYou are the final winner. Congratuation!");
        }
        else if (user.total < cpu.total)
        {
            Console.WriteLine("\nIt was close! Cpu is the winner. Try again!");
        }
        else
        {
            Console.WriteLine("\nDraw! Great match.");
        }
        Console.WriteLine("\n------------------------------");
        Console.WriteLine("\nGameOver\nGood Bye!");
    }

}


