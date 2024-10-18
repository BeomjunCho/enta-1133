using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GD12_1133_Lab1_Cho_Beomjun.Scripts.Items;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GD12_1133_Lab1_Cho_Beomjun.Scripts
{

    public class DieRoller
    {
        Random rnd = new Random();

        Dagger dagger = new Dagger();
        LongSword longSword = new LongSword();
        DragonSword dragonSword = new DragonSword();
        HealingPotion healPotion = new HealingPotion();
        FireScroll fireScroll = new FireScroll();
        ShieldScroll shieldScroll = new ShieldScroll();

        public void GamePlayLoop(Player user, Monster.BaseMonster monster) // Main game play loop
        {
            while(monster.hp > 0 && user.hp > 0) // loop until monster hp or user hp is less than 0
            {
                int turnOrder = rnd.Next(1, 3); // turn decided randomly every time

                if (turnOrder == 1) // First turn order
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // user attack turn
                    Console.WriteLine("\n[[[It is your turn!]]]");
                    Console.ResetColor();
                    monster.ShowHp();
                    user.ShowPlayerState();
                    PickItemUse(user, monster); // user choose and use item

                    if (monster.hp <= 0) // check if monster is dead
                    {
                        Console.WriteLine($"\nYou killed the {monster.ToString()}!");
                        break; 
                    }
                    else
                    {
                        monster.ShowHp();
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow; // monster attack turn
                    Console.WriteLine("\n[[[Now it is Monster turn!]]]");
                    Console.ResetColor();
                    monster.Attack(user);
                    
                    if (user.hp <= 0) // check if user is dead
                    {
                        Console.WriteLine($"\n{monster.ToString()} killed you...\nRIP");
                        break;
                    }
                    else
                    {
                        user.ShowHp();
                    }
                }

                else // Second turn order
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // monster attack turn
                    Console.WriteLine("\n[[[It is Monster turn!]]]");
                    Console.ResetColor();
                    monster.Attack(user);

                    if (user.hp <= 0)
                    {
                        Console.WriteLine($"\n{monster.ToString()} killed you...\nRIP");
                        break;
                    }
                    else
                    {
                        user.ShowHp();

                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n[[[Now, it is your turn!]]]"); //user attack turn
                    Console.ResetColor();
                    monster.ShowHp();
                    user.ShowPlayerState(); 
                    PickItemUse(user, monster); // user choose and use item
                    if (monster.hp <= 0)
                    {
                        Console.ForegroundColor= ConsoleColor.Yellow;
                        Console.WriteLine($"\nYou killed the {monster.ToString()}!");
                        Console.ResetColor();
                        break;
                    }
                    else
                    {
                        monster.ShowHp();
                    }
                }
            }
        }


        public void PickItemUse(Player player, Monster.BaseMonster monster) // Pick the item and use it
        {
            Inventory playerInventory = player.inventory; // call inventory from player instance "user"
            Console.WriteLine("\nPick the item(Enter number of item) \n-----6: Healing Potion, 8: Dagger, 12: Long sword, 20: Dragon Sword, 25: Shield scroll 30: Fire Scroll-----");
            string userChoice = Console.ReadLine() ?? string.Empty;

            if (userChoice == "6" || userChoice == "8" || userChoice == "12" || userChoice == "20" || userChoice == "25" || userChoice == "30") // Check if user choose right dice
            {
                if (userChoice == "6" && player.inventory.DoesPlayerHave(healPotion.ID)) // check and use heal potion
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"Healing potion is linked to {userChoice} sides dice.");
                    Console.ResetColor();
                    player.inventory.UseItem(healPotion, monster, player);
                }
                else if (userChoice == "8" && player.inventory.DoesPlayerHave(dagger.ID)) // check and use dagger
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"Dagger is linked to {userChoice} sides dice.");
                    Console.ResetColor();
                    player.inventory.UseItem(dagger, monster, player);
                }
                else if (userChoice == "12" && player.inventory.DoesPlayerHave(longSword.ID)) // check and use long sword
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"Long sword is linked to {userChoice} sides dice.");
                    Console.ResetColor();
                    player.inventory.UseItem(longSword, monster, player);
                }
                else if (userChoice == "20" && player.inventory.DoesPlayerHave(dragonSword.ID)) // check and use dragon sword
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"Dragon sword is linked to {userChoice} sides dice.");
                    Console.ResetColor();
                    player.inventory.UseItem(dragonSword, monster, player);
                }
                else if (userChoice == "25" && player.inventory.DoesPlayerHave(shieldScroll.ID)) // check and use shield scroll
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"Shield scroll is linked to {userChoice} sides dice.");
                    Console.ResetColor();
                    player.inventory.UseItem(shieldScroll, monster, player);
                }
                else if (userChoice == "30" && player.inventory.DoesPlayerHave(fireScroll.ID)) // check and use fire scroll
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"Fire scroll is linked to {userChoice} sides dice.");
                    Console.ResetColor();
                    player.inventory.UseItem(fireScroll, monster, player);
                }
                else // user selected item which doesn't exist in inventory
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You don't have the selected item. Pick another one.");
                    Console.ResetColor();
                    PickItemUse(player, monster);
                }
            }
            else // user entered wrong text
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Entry is invalid. Try again");
                Console.WriteLine("No item selected.");
                Console.ResetColor();
                PickItemUse(player, monster);
            }

        }
    }
}
