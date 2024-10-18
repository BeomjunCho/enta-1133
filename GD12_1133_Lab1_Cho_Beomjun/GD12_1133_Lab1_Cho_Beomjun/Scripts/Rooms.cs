using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static GD12_1133_Lab1_Cho_Beomjun.Scripts.Monster;
using static GD12_1133_Lab1_Cho_Beomjun.Scripts.Items;

namespace GD12_1133_Lab1_Cho_Beomjun.Scripts
{
    public class Rooms
    {
        
        public abstract class Room
        {
            protected AsciiArt ascii = new AsciiArt();
            public bool IsVisited { get; set; } = false; // check if this room is visited

            // Room connection
            public Room North = null!;
            public Room South = null!;
            public Room East = null!;
            public Room West = null!;
            // Action in the room
            public abstract void OnRoomEntered(Player user);
            public abstract void OnRoomSearched(Player user);
            public abstract void OnRoomExit(Player user);

            // Default representation
            public override string ToString()
            {
                return "Room"; 
            }
        }

        //In this room user have 
        public class StartingRoom : Room
        {
            
            public override string ToString()
            {
                return "Starting Room"; //representation
            }
            public override void OnRoomEntered(Player user)
            {
                Console.WriteLine("--------------------------------------------------------------------------You entered Starting Room.");
                ascii.PrintAscii(ascii.startingRoom);
                Console.WriteLine("This is a damp, slightly dark room.\n(Enter anykey)");
                Console.ReadLine();
            }

            public override void OnRoomSearched(Player user)
            {

                if (!IsVisited) // is this room visited?
                {
                    Console.WriteLine("--------------------------------------------------------------------------You are searching Starting Room.");
                    Console.WriteLine("There are dagger and potion on the floor\n(Enter anykey)");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("--------------------------------------------------------------------------You are searching Starting Room.");
                    Console.WriteLine("Nothing is here.");
                }
                user.ShowPlayerState();
                Console.WriteLine("Do you want to leave this room?\n(Enter anykey)");
                Console.ReadLine();
                Console.Clear();

            }

            public override void OnRoomExit(Player user)
            {
                Console.WriteLine("--------------------------------------------------------------------------You are leaving from Starting Room");
                IsVisited = true; // Mark the room as visited
            }
        }

        //In this room, user gets random items
        public class TreasureRoom : Room
        {

            public override string ToString()
            {
                return "Treasure Room"; //representation
            }
            public override void OnRoomEntered(Player user)
            {
                if (!IsVisited) // is this room visited?
                {
                    Console.WriteLine("--------------------------------------------------------------------------You entered Treasure Room");
                    ascii.PrintAscii(ascii.treasureRoom);
                    Console.WriteLine("There is one treasure chest on center of this room.\n(Enter anykey)");
                    Console.ReadLine();

                }
                else
                {
                    Console.WriteLine("--------------------------------------------------------------------------You entered Treasure Room");
                    Console.WriteLine("You have returned to the Treasure Room. The room looks the same as before except for opened chest.");
                }
            }

            public override void OnRoomSearched(Player user)
            {
                if (!IsVisited) // is this room visited?
                {
                    Console.WriteLine("--------------------------------------------------------------------------You are searching Treasure Room");
                    Console.WriteLine("You opened the chest and there is one item!");
                    ascii.PrintAscii(ascii.treasure);
                    user.inventory.TreasureItemAdd();
                    
                }
                else
                {
                    Console.WriteLine("--------------------------------------------------------------------------You are searching Treasure Room");
                    Console.WriteLine("There is nothing left to search in this room.");
                }
                user.ShowPlayerState();
                Console.WriteLine("Do you want to leave this room?\n(Enter anykey)");
                Console.ReadLine();
                Console.Clear();
            }

            public override void OnRoomExit(Player user)
            {
                Console.WriteLine("--------------------------------------------------------------------------You are leaving from Treasure Room");
                IsVisited = true; // Mark the room as visited

            }
        }

        //In this room, user fights with normal monster
        public class CombatRoom : Room
        {
            public override string ToString()
            {
                return "Combat Room"; //representation
            }
            public override void OnRoomEntered(Player user)
            {
                if (!IsVisited) // is this room visited?
                {
                    Console.WriteLine("--------------------------------------------------------------------------You entered Combat Room.");
                    ascii.PrintAscii(ascii.combatRoom);
                    Console.WriteLine("You’ve encountered a fierce Minotaur. Prepare for battle!\n(Enter anykey)");
                    Console.ReadLine();
                    NormalMonster monster = new NormalMonster();
                    DieRoller dieRoller = new DieRoller();
                    ascii.PrintAscii(ascii.enemy);
                    dieRoller.GamePlayLoop(user, monster); // dice roll battle starts
                }
                else
                {
                    Console.WriteLine("--------------------------------------------------------------------------You entered Combat Room.");
                    Console.WriteLine("You have returned to the Combat Room. There is a dead monster.");
                }
            }

            public override void OnRoomSearched(Player user)
            {
                if (!IsVisited) // is this room visited?
                {
                    Console.WriteLine("--------------------------------------------------------------------------You are searching Combat Room.");
                    Console.WriteLine("You found one item from dead monster.");
                    user.inventory.AddRndItem();
                }
                else
                {
                    Console.WriteLine("--------------------------------------------------------------------------You are searching Combat Room.");
                    Console.WriteLine("There is nothing left to search in this room.");
                }
                user.ShowPlayerState();
                Console.WriteLine("Do you want to leave this room?\n(Enter anykey)");
                Console.ReadLine();
                Console.Clear();
            }

            public override void OnRoomExit(Player user)
            {               
                Console.WriteLine("--------------------------------------------------------------------------You are leaving from Combat Room");
                IsVisited = true; // Mark the room as visited
            }
        }

        //In this room, user fight with boss monster
        public class BossRoom : Room
        {
            public override string ToString()
            {
                return "Boss Room"; //representation
            }
            public override void OnRoomEntered(Player user)
            {
                Console.WriteLine("--------------------------------------------------------------------------You entered Boss Room.");
                ascii.PrintAscii(ascii.bossRoom);
                Console.WriteLine("You found a boss monster! Ready to fight!\n(Enter anykey)");
                Console.ReadLine();
                BossMonster monster = new BossMonster();
                DieRoller dieRoller = new DieRoller();
                ascii.PrintAscii(ascii.boss);
                Console.WriteLine("Pumpking wants to kill you.");
                dieRoller.GamePlayLoop(user, monster); // dice roll battle starts
            }

            public override void OnRoomSearched(Player user)
            {
                user.ShowPlayerState();
                Console.WriteLine("Do you want to leave this room?\n(Enter anykey)");
                Console.ReadLine();
                Console.Clear();
            }

            public override void OnRoomExit(Player user)
            {
                Console.WriteLine("--------------------------------------------------------------------------You left from Boss Room");
            }
        }

        //In this room, user can get healed 
        public class HealingRoom : Room
        {
            public override string ToString()
            {
                return "Healing Room"; //representation
            }
            public override void OnRoomEntered(Player user)
            {
                Console.WriteLine("--------------------------------------------------------------------------You entered Healing Room.");
                ascii.PrintAscii(ascii.healingRoom);
                if (!IsVisited) // is this room visited?
                {
                    Console.WriteLine("You found a statue which is glowing with a sacred light.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("--------------------------------------------------------------------------You entered Healing Room.");
                    Console.WriteLine("You have returned to the Healing Room. The statue doesn't looks sacred anymore.\nPress anykey");
                    Console.ReadLine();
                }
            }

            public override void OnRoomSearched(Player user)
            {
                if (!IsVisited) // is this room visited?
                {
                    Console.WriteLine("--------------------------------------------------------------------------You are searching Healing Room.");
                    ascii.PrintAscii(ascii.healRoomEffect);
                    Console.WriteLine("You approached a statue of a goddess. It has a sacred aura.\nYou begin to pray.");
                    if (user.hp == 10)
                    {
                        Console.WriteLine("You are already fully healed. You don't get healed");
                    }
                    else if (user.hp >= 5 && user.hp <= 9)
                    {
                        int healAmount = 10 - user.hp;
                        user.hp += healAmount;
                        Console.WriteLine($"You got healed for {healAmount}.");
                    }
                    else
                    {
                        int healAmount = 5;
                        user.hp += healAmount;
                        Console.WriteLine($"You got healed for {healAmount}.");
                    }
                }
                else
                {
                    Console.WriteLine("--------------------------------------------------------------------------You are searching Healing Room.");
                    Console.WriteLine("There’s nothing to be found here except for a simple stone statue.");
                }
                user.ShowPlayerState();
                Console.WriteLine("Do you want to leave this room?\n(Enter anykey)");
                Console.ReadLine();
                Console.Clear();
            }

            public override void OnRoomExit(Player user)
            {
                Console.WriteLine("--------------------------------------------------------------------------You are leaving from Healing Room");
                IsVisited = true; // Mark the room as visited
            }

        }

        //In this room, user can exchange their hp to dragon sword
        public class TradeRoom : Room
        {
            public override string ToString()
            {
                return "Trade Room"; //representation
            }
            public override void OnRoomEntered(Player user)
            {
                Console.WriteLine("--------------------------------------------------------------------------You entered Trade Room.");
                ascii.PrintAscii(ascii.tradeRoom);
                Console.WriteLine("You can see a demon on center of this room. He looks friendly\nPress anykey");
                Console.ReadLine();
            }

            public override void OnRoomSearched(Player user)
            {
                Console.WriteLine("--------------------------------------------------------------------------You are searching Trade Room.");
                ascii.PrintAscii(ascii.tradeDemon);
                Console.WriteLine("You meet trade demon.");
                Console.WriteLine("Trade demon wants to exchange your 5 HP points to one Legendary item.");
                user.ShowPlayerState();
                bool validAnswer = false;
                while (!validAnswer)
                {
                    Console.WriteLine("Do you want to trade? (yes or no)");
                    string answer = Console.ReadLine() ?? string.Empty ;
                    
                    if (answer == "yes")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You lost -5 HP points");
                        Console.ForegroundColor= ConsoleColor.Blue;
                        Console.WriteLine("You got one item.");
                        Console.ResetColor();
                        user.hp -= 5;
                        user.inventory.TradeItemAdd(); // user get dragon sword
                        validAnswer = true;
                    }
                    else if (answer == "no")
                    {
                        validAnswer = true;
                        Console.WriteLine("Trade demon looks sad.");
                    }
                    else
                    {
                        Console.WriteLine("Please answer correctly.(yes or no)");
                    }
                }
                user.ShowPlayerState();
                Console.WriteLine("Do you want to leave this room?\n(Enter anykey)");
                Console.ReadLine();
                Console.Clear();
            }

            public override void OnRoomExit(Player user)
            {
                Console.WriteLine("--------------------------------------------------------------------------You are leaving from Trade Room");
            }
        }

        //In this room, user can get scrolls depends on their luck
        public class LibraryRoom : Room
        {
            public override string ToString()
            {
                return "Library Room"; // representation
            }

            public override void OnRoomEntered(Player user)
            {
                Console.WriteLine("--------------------------------------------------------------------------You entered Library Room.");
                ascii.PrintAscii(ascii.library); // Add a corresponding ASCII art for library if available
                Console.WriteLine("The room is filled with ancient books and scrolls. There's a mysterious aura in the air.\n(Enter anykey)");
                Console.ReadLine();
            }

            public override void OnRoomSearched(Player user)
            {
                if (!IsVisited) // is this room visited?
                {
                    Console.WriteLine("--------------------------------------------------------------------------You are searching the Library Room.");
                    Console.WriteLine("You scan the shelves and find an old tome with strange symbols.");

                    Random rnd = new Random();
                    int index = rnd.Next(1, 4);
                    switch (index) // randomly get fire scroll, shield scroll, nothing
                    {
                        case 1:
                            Console.WriteLine("You discover a scroll with a fire spell you can use in battle.");
                            FireScroll fireScroll = new FireScroll();
                            user.inventory.AddItem(fireScroll);
                            break;
                        case 2:
                            Console.WriteLine("You discover a scroll with a shield spell you can use in battle.");
                            ShieldScroll shieldScroll = new ShieldScroll();
                            user.inventory.AddItem(shieldScroll);
                            break;
                        default:
                            Console.WriteLine("You found some interesting lore, but it doesn’t seem immediately useful.");
                            break;
                    }
                   
                }
                else
                {
                    Console.WriteLine("--------------------------------------------------------------------------You are searching the Library Room.");
                    Console.WriteLine("There doesn't seem to be anything new to find here.");
                }

                user.ShowPlayerState();
                Console.WriteLine("Do you want to leave this room?\n(Enter anykey)");
                Console.ReadLine();
                Console.Clear();
            }

            public override void OnRoomExit(Player user)
            {
                Console.WriteLine("--------------------------------------------------------------------------You are leaving the Library Room.");
                IsVisited = true; // Mark the room as visited
            }
        }


    }
}
