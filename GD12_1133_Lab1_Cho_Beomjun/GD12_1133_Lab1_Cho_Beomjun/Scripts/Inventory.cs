using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GD12_1133_Lab1_Cho_Beomjun.Scripts.Items;

namespace GD12_1133_Lab1_Cho_Beomjun.Scripts
{
    public class Inventory
    {
        Dagger dagger = new Dagger();
        LongSword longSword = new LongSword();
        DragonSword dragonSword = new DragonSword();
        HealingPotion healPotion = new HealingPotion();
        FireScroll fireScroll = new FireScroll();
        ShieldScroll shieldScroll = new ShieldScroll();
        Random rnd = new Random();

        //Inventory list
        public List<Item> inventory = new List<Item>();
        protected List<Item> itemsList = new List<Item>();
        protected List<Item> treasureList = new List<Item>();

        // Dictionary to track items that can have multiple quantities, like healing potions
        protected Dictionary<int, int> itemQuantities = new Dictionary<int, int>();

        public Inventory()
        {
            inventory.Add(dagger);
            inventory.Add(healPotion);
            itemsList.Add(healPotion);
            itemsList.Add(dagger);
            itemsList.Add(longSword);
            itemsList.Add(dragonSword);
            itemsList.Add(fireScroll);
            itemsList.Add(shieldScroll);
            treasureList.Add(longSword);
            treasureList.Add(dragonSword);
            treasureList.Add(fireScroll);
            treasureList.Add(shieldScroll);
            itemQuantities[healPotion.ID] = 1; // heal potion id 1 : key / 1 is value
            itemQuantities[fireScroll.ID] = 0;
            itemQuantities[shieldScroll.ID] = 0;
        }
        public void AddItem(Item item)
        {
            if (item is Consumable) // check if the item is consumable
            {
                if (itemQuantities.ContainsKey(item.ID)) // Check if item ID exists in the dictionary
                {
                    itemQuantities[item.ID]++; // If it does, increment the quantity
                }
                else
                {
                    itemQuantities[item.ID] = 1; // If it doesn't, create the item ID to the dictionary with quantity 1
                }
                
                // Add the item to the inventory if it's not already there
                if (!DoesPlayerHave(item.ID))
                {
                    inventory.Add(item);
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{item} added to inventory.");
                Console.WriteLine($"You have {itemQuantities[item.ID]} {item} in your inventory");
                Console.ResetColor();
            }
            else // item is not consumable
            {
                if (DoesPlayerHave(item.ID)) // check if player already has this item ID
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"You already have {item}.\nNothing is added to your inventory.");
                    Console.ResetColor();
                }
                else
                {
                    inventory.Add(item); // Add non-consumable item only if not already in inventory
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{item} added to inventory.");
                    Console.ResetColor();
                }
            }
        }

        public bool DoesPlayerHave(int itemId) // check if the item is stored in inventory by using item id
        {
            foreach (var item in inventory)
            {
                if (item.ID == itemId)
                {
                    return true; // Found the item
                }
            }
            return false; // Item not found
        }
        public void AddRndItem() // add random item in inventory
        {
            int index = rnd.Next(1, 101); // 20% dagger, 10% shield scroll, 10% fire scroll, 20% long sword, 10% dragon sword, 30% healing potion
            if (index <= 20)
            {
                AddItem(dagger);
            }
            else if (index > 20 && index <= 30)
            {
                AddItem(shieldScroll);
            }
            else if (index > 30 && index <= 40)
            {
                AddItem(fireScroll);
            }
            else if (index > 40 && index <= 60)
            {
                AddItem(longSword);
            }
            else if (index > 60 && index <= 70)
            {
                AddItem(dragonSword);
            }
            else if (index > 70)
            {
                AddItem(healPotion);
            }
        }

        public void TreasureItemAdd() 
        {
            int index = rnd.Next(0, treasureList.Count); 

            Item rndItem = treasureList[index];

            if (rndItem is Consumable)
            {
                if (itemQuantities.ContainsKey(rndItem.ID))
                {
                    itemQuantities[rndItem.ID]++; // Increment the quantity
                }
                else
                {
                    itemQuantities[rndItem.ID] = 1; // Initialize quantity
                }
                // Add the item to the inventory if it's not already there
                if (!DoesPlayerHave(rndItem.ID))
                {
                    inventory.Add(rndItem);
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You found {rndItem} from the treasure chest! You now have {itemQuantities[rndItem.ID]} {rndItem}(s).");
                Console.ResetColor();
            }
            else // item is not consumable
            {
                if (DoesPlayerHave(rndItem.ID))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"You found {rndItem} from treasure chest but you already have it.\nNothing is added to your inventory.");
                    Console.ResetColor();
                }

                else
                {
                    inventory.Add(rndItem);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"You found {rndItem} from treasure chest!\n{rndItem} added to inventory.");
                    Console.ResetColor();
                }
            }
        }

        public void TradeItemAdd() //add dragon sword to inventory 
        {
            Item Item = dragonSword;

            if (DoesPlayerHave(Item.ID))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You got {Item} from trade demon but you already have it.\nNothing is added to your inventory.");
                Console.ResetColor();
            }

            else
            {
                inventory.Add(Item);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You got {Item} from trade demon!\n{Item} added to inventory.");
                Console.ResetColor();
            }
        }
        public void RemoveItem(Item item) // Remove specific item
        {
            if (item is Consumable)
            {
                if (itemQuantities.ContainsKey(item.ID) && itemQuantities[item.ID] > 0) // item key is stored in dictionary and item quantity is over 0
                {
                    itemQuantities[item.ID]--; // item quantity -1

                    if (itemQuantities[item.ID] == 0) // if item quantity is 0
                    {
                        for (int index = inventory.Count - 1; index >= 0; index--) // remove all items which has same id with parameter item since parameter item can not be same instance in this file.
                        {
                            if (inventory[index].ID == item.ID) // check id
                            {
                                inventory.RemoveAt(index); // remove item which is placed at "inventory[index]"
                            }
                        }
                        itemQuantities.Remove(item.ID); // remove item information in dictionary
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Removed {item} from inventory.");
                        Console.ResetColor();
                    }
                    else // item is still in inventory
                    {
                        Console.WriteLine($"Used one {item}. {itemQuantities[item.ID]} remaining.");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"No {item} left in inventory.");
                    Console.ResetColor();
                }
            }
            else // item is not consumable
            {
                if (inventory.Remove(item))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Removed {item} from inventory.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Item not found in inventory.");
                    Console.ResetColor();
                }
            }
        }
        public void ShowInventory() // display all items in the inventory
        {
            Console.WriteLine("\nInventory:"); // Print a heading for the inventory display

            foreach (var item in inventory)
            {
                // Only display items that are not consumables
                if (!(item is Consumable))
                {
                    Console.WriteLine($"  Item: {item}");
                }
            }
            foreach (var entry in itemQuantities)
            {
                Item? consumableItem = null; // a variable to store the found item

                foreach(var item in inventory)
                {
                    if (item.ID == entry.Key && item is Consumable)
                    {
                        consumableItem = item;
                        break;
                    }
                }
                // If a matching consumable item was found, display it with its quantity
                if (consumableItem != null)
                {
                    Console.WriteLine($"  Item: {consumableItem}, : {entry.Value} quantity.");
                }
            }
        }

        public void UseItem(Item item, Monster.BaseMonster monster, Player user) // use item
        {
            if (!DoesPlayerHave(item.ID)) // user doesn't have the item
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You don't have {item} in your list.");
                Console.ResetColor();
            }
            else // user have item so use item
            {
                if (item.ID == dagger.ID) // check item id with dagger id
                {
                    dagger.Attack(monster);
                }
                else if (item.ID == longSword.ID)
                {
                    longSword.Attack(monster);
                }
                else if (item.ID == dragonSword.ID)
                {
                    dragonSword.Attack(monster);
                }
                else if(item.ID == healPotion.ID)
                {
                    healPotion.Heal(user);
                    RemoveItem(item);
                }
                else if (item.ID == fireScroll.ID)
                {
                    fireScroll.Cast(monster);
                    RemoveItem(item);
                }
                else if (item.ID == shieldScroll.ID)
                {
                    shieldScroll.Cast(user);
                    RemoveItem(item);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("It is not item in the itemsList"); 
                    Console.ResetColor();
                }
            }
        }

    }
}
