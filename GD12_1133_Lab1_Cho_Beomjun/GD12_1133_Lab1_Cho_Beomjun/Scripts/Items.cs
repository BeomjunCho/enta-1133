using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD12_1133_Lab1_Cho_Beomjun.Scripts
{
    public class Items
    {
        public abstract class Item
        {
            protected AsciiArt ascii = new AsciiArt();
            public int ID { get; set; } // Item id
            protected int randomNumber(int maxIndex) // random number generator
            {
                Random rnd = new Random();
                int RanNum = rnd.Next(1, maxIndex + 1);
                return RanNum;
            }
        }
        public abstract class Weapon : Item
        {
            // Create an instance of DieRoller in the Weapon class
            public abstract void Attack(Monster.BaseMonster monster);
            public abstract int maxDamage { get; set; }
        }

        public abstract class Consumable : Item
        {
            public abstract int effect { get; set; }

        }

        public class HealingPotion : Consumable
        {
            public override string ToString()
            {
                return "HealingPotion"; //representation
            }
            public HealingPotion()
            {
                ID = 1;
                effect = 6;
            }
            public override int effect { get; set; }
            public void Heal(Player user) // heal user until 10 hp, without exceeding it
            {
                Console.WriteLine("Rolling Dice...");
                int result = randomNumber(effect);
                Console.WriteLine($"Your dice number was {result}!");
                ascii.PrintAscii(ascii.healPortion);
                Console.ForegroundColor = ConsoleColor.Green;

                int potentialNewHp = user.hp + result; // Calculate the potential new hp

                if (user.hp == 10) // when user hp is already full
                {
                    Console.WriteLine($"Your heal potion is enchanted by dice number {result}");
                    Console.WriteLine("You are already fully healed. You don't get healed.");
                }
                else if (potentialNewHp >= 10) // if user hp is potentially over 10
                {
                    int healAmount = 10 - user.hp; // calculate heal amount
                    user.hp = 10; // set user hp to 10
                    Console.WriteLine($"Your heal potion is enchanted by dice number {result}");
                    Console.WriteLine($"You got healed for {healAmount} by using heal potion. Your hp is now full at 10.");
                }
                else
                {
                    user.hp += result; // add result to user hp
                    Console.WriteLine($"Your heal potion is enchanted by dice number {result}");
                    Console.WriteLine($"You got healed for {result} by using heal potion. Your current hp is {user.hp}.");
                }

                Console.ResetColor();
            }
        }

        public class Dagger : Weapon
        {
            public override string ToString()
            {
                return "Dagger"; //representation
            }
            public Dagger()
            {
                ID = 2;
                maxDamage = 8;
            }
            public override int maxDamage { get; set; }
            public override void Attack(Monster.BaseMonster monster) // attack monster for random damage 
            {
                Console.WriteLine("Rolling Dice...");
                int result = randomNumber(maxDamage);
                Console.WriteLine($"Your dice number was {result}!");
                monster.hp -= result;
                ascii.PrintAscii(ascii.dagger);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You attacked the monster with Dagger for {result} damage.");
                Console.ResetColor();
            }
        }

        public class LongSword : Weapon
        {
            public override string ToString()
            {
                return "LongSword"; //representation
            }

            public LongSword()
            {
                ID = 3;
                maxDamage = 12;
            }
            public override int maxDamage { get; set; }

            public override void Attack(Monster.BaseMonster monster) // attack monster for random damage 
            {
                Console.WriteLine("Rolling Dice...");
                int result = randomNumber(maxDamage);
                Console.WriteLine($"Your dice number was {result}!");
                monster.hp -= result;
                ascii.PrintAscii(ascii.longSword);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You attacked the monster with Long Sword for {result} damage.");
                Console.ResetColor();
            }
        }

        public class DragonSword : Weapon
        {
            public override string ToString()
            {
                return "DragonSword"; //representation
            }

            public DragonSword()
            {
                ID = 4;
                maxDamage = 20;
            }
            public override int maxDamage { get; set; }

            public override void Attack(Monster.BaseMonster monster) // attack monster for random damage 
            {
                Console.WriteLine("Rolling Dice...");
                int result = randomNumber(maxDamage);
                Console.WriteLine($"Your dice number was {result}!");
                monster.hp -= result;
                ascii.PrintAscii(ascii.drangonSword);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You attacked the monster with Dragon Sword for {result} damage.");
                Console.ResetColor();
            }
        }

        public class FireScroll : Consumable
        {
            public override string ToString()
            {
                return "FireScroll"; // representation
            }

            public FireScroll()
            {
                ID = 5;
                effect = 30; // Dice roll effect for damage amount
            }
            public override int effect { get; set; }

            public void Cast(Monster.BaseMonster monster) // Casts fire spell to damage monster
            {
                Console.WriteLine("Rolling Dice for Fire Spell...");
                int result = randomNumber(effect);
                Console.WriteLine($"Your dice number was {result}!");
                ascii.PrintAscii(ascii.fireScroll);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You cast a Fire Spell and dealt {result} damage to the monster!");
                monster.hp -= result;
                Console.ResetColor();
            }
        }

        public class ShieldScroll : Consumable
        {
            public override string ToString()
            {
                return "ShieldScroll"; // representation
            }

            public ShieldScroll()
            {
                ID = 6;
                effect = 25; // Dice roll effect for defense boost amount
            }
            public override int effect { get; set; }

            public void Cast(Player user) // Grants temporary defense boost
            {
                Console.WriteLine("Rolling Dice for Shield Spell...");
                int result = randomNumber(effect);
                Console.WriteLine($"Your dice number was {result}!");
                ascii.PrintAscii(ascii.shieldScroll);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You cast a Shield Spell and gained {result} additional shield on your hp!");
                user.shield += result;
                Console.ResetColor();
            }
        }
    }
}
