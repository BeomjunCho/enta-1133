using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static GD12_1133_Lab1_Cho_Beomjun.Scripts.Items;

namespace GD12_1133_Lab1_Cho_Beomjun.Scripts
{
    public class Player
    {
        public string userName = "";
        public int hp = 10; // health points
        public int shield = 0; // shield points if it is over 0 then, it takes damage from hp

        // user inventory. It is only one inventory in the game
        public Inventory inventory = new Inventory();

        public void ShowHp() // Show user hp
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nUser hp: {hp}");
            Console.WriteLine($"\nShield amount: {shield}");
            Console.ResetColor();
        }

        public void ShowPlayerState() // Show user hp, shield amount and inventory
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            inventory.ShowInventory();                       // show inventory
            Console.WriteLine($"Your Health point: {hp}"); // hp
            Console.WriteLine($"Shield: {shield}");        //shield amount
            Console.ResetColor();
        }

    }
}
