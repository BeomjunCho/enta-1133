using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GD12_1133_Lab1_Cho_Beomjun.Scripts.Monster;

namespace GD12_1133_Lab1_Cho_Beomjun.Scripts
{
    public class Monster
    {
        public abstract class BaseMonster
        {
            Random rnd = new Random();
            public abstract int hp { get; set; } // monster hp
            public abstract int damage { get; set; } // monster damage
            public abstract void Attack(Player user); //attack method
            public abstract void ShowHp(); // show monster hp method
        }

        public class NormalMonster : BaseMonster
        {
            public override string ToString()
            {
                return "Minotaur"; //representation
            }
            public NormalMonster()
            {
                hp = 10;
                damage = 1;
            }
            public override int hp { get; set; }
            public override int damage { get; set; }

            public override void Attack(Player user)
            {
                if (user.shield == 0) // if user doesn't have shield, give damage to hp
                {
                    user.hp -= damage;
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Minotaur attacked you. You lost 1 hp points");
                    Console.ResetColor();
                }
                else if (user.shield > 0)// if user has shield, give damage to shield
                {
                    user.shield -= damage;
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Minotaur attacked you but it is blocked by your shield!\nShield -1");
                    Console.ResetColor();
                    if (user.shield < 0) // when shield less than 0
                    {
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine("Shield spell is broken!");
                        user.shield = 0; // set shield 0 again
                        Console.ResetColor();
                    }
                }
            }

            public override void ShowHp()
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"\nMoster hp: {hp}");
                Console.ResetColor();
            }

        }

        public class BossMonster : BaseMonster
        {
            public override string ToString()
            {
                return "Pumpking"; //representation
            }
            public BossMonster()
            {
                hp = 30;
                damage = 3;
            }
            public override int hp { get; set; }
            public override int damage { get; set; }

            public override void Attack(Player user)
            {
                if (user.shield == 0)// if user doesn't have shield, give damage to hp
                {
                    user.hp -= damage;
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Pumpking attacked you. You lost 3 hp points");
                    Console.ResetColor();
                }
                else if (user.shield > 0) // if user has shield, give damage to shield
                {
                    user.shield -= damage;
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Pumpking attacked you but it is blocked by your shield!\nShield -3");
                    Console.ResetColor();
                    if (user.shield < 0)// when shield less than 0
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Shield spell is broken!");
                        user.shield = 0;// set shield 0 again
                        Console.ResetColor();
                    }
                }
            }

            public override void ShowHp()
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"\nBoss Moster hp: {hp}");
                Console.ResetColor();
            }

        }
        
        
    }
}
