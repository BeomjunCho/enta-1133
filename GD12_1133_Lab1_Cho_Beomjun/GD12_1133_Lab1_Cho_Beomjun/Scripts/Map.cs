using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GD12_1133_Lab1_Cho_Beomjun.Scripts.Items;
using static GD12_1133_Lab1_Cho_Beomjun.Scripts.Rooms;

namespace GD12_1133_Lab1_Cho_Beomjun.Scripts
{
    public class Map
    {

        public Room currentRoom;
        Room? nextRoom;
        public Room lastRoom; 

        Random rnd = new Random();
        StartingRoom startingRoom = new StartingRoom();
        BossRoom bossRoom = new BossRoom();

        List<Room> roomList = new List<Room> { new TreasureRoom(), new CombatRoom(), new HealingRoom(), new TradeRoom() , new LibraryRoom()};

        List<Room> mapList = new List<Room>();

        private int mapSize;
        private int allRoom;

        public Map(int size) // This is constructer. Codes in here will be initialized when instance is created
        {
            mapSize = size;
            allRoom = (mapSize * mapSize);
            
            BuildRooms(); // add random rooms to mapList
            SetDirection(); // Set directions for each room
            currentRoom = mapList[0]; // set current room to first room
            lastRoom = mapList[mapList.Count - 1];
            //PrintMapDirections(); // Debug code
        }

        
        public void BuildRooms() // build game room. first is starting room and last is boss room always
        {
            mapList.Clear();

            mapList.Add(startingRoom); // Always add starting room first

            for (int i = 1; i < allRoom - 1; i++)
            {
                Room rndRoom = RandomRoomGenerater();
                mapList.Add(rndRoom);
            }

            mapList.Add(bossRoom); // Always add boss room at the end
        }

        public void SetDirection() // set direction for grid map
        {
            for (int i = 0; i < mapList.Count; i++)
            {
                // Set North
                if (i >= mapSize)
                    mapList[i].North = mapList[i - mapSize];

                // Set South
                if (i < allRoom - mapSize)
                    mapList[i].South = mapList[i + mapSize];

                // Set West
                if (i % mapSize != 0)
                    mapList[i].West = mapList[i - 1];

                // Set East
                if ((i + 1) % mapSize != 0)
                    mapList[i].East = mapList[i + 1];
            }
        }

        public void MoveRoom(Player user) // Prepare to enter room
        {
            bool checkDirection = false;
            string direction = "";
            while (!checkDirection) // loop for get to exist next room
            {
                direction = WhichDirection(); // get direction from user
                NextRoom(currentRoom, direction); // set next room from current room on the direction. ex) currentRoom.North
                if (nextRoom != null) // Check if nextRoom is not null before proceeding
                {
                    checkDirection = true; // loop end
                }
                else // if there is no room in that direction, loop again
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("There is no room in that direction. Please choose another direction.");
                    Console.ResetColor();
                }
            }
            currentRoom = nextRoom!; // move to next room
        }

        public void NextRoom(Room currentRoom, string direction) // set next room from current room on the direction. ex) currentRoom.North
        {
            switch (direction)
            {
                case "north":
                    nextRoom = currentRoom.North;
                    break;
                case "south":
                    nextRoom = currentRoom.South;
                    break;
                case "west":
                    nextRoom = currentRoom.West;
                    break;
                case "east":
                    nextRoom = currentRoom.East;
                    break;
                default:
                    Console.WriteLine("Invalid direction."); // check if user entered right text
                    nextRoom = null;
                    break;
            }
        }

        public void EnterRoom(Room room, Player user) // do room action and show minimap
        {
            currentRoom = room;
            if (user.hp > 0)
            {
                MiniMap();
                room.OnRoomEntered(user); // room entered action execute
            }
            if (user.hp > 0)
            {
                room.OnRoomSearched(user); // room searched action execute
            }
            if (user.hp > 0)
            {
                if (currentRoom != lastRoom)
                {
                    MiniMap();
                }
                room.OnRoomExit(user); // room exit action execute
                user.shield = 0;// shield is vanished when player goes out the room
            }
        }


        public string WhichDirection() // ask dirction to user
        {
            bool validDirec = false;
            string userDirection = "";
            while (!validDirec) // ask same question unil user five proper answer
            {
                Console.WriteLine("Which direction do you want to go?");
                Console.WriteLine("Enter one of directions\n(North, South, East, West)");
                userDirection = Console.ReadLine() ?? string.Empty.ToLower();
                if ((userDirection == "north" || userDirection == "south" || userDirection == "east" || userDirection == "west"))
                {
                    validDirec = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You cannot go in that direction. Please enter valid direction.");
                    Console.ResetColor();
                }
            }
            Console.WriteLine($"You chose {userDirection} direction");
            return userDirection; // return direction user chose
        }


        public Room RandomRoomGenerater() // generate random room from roomList
        {
            int roomIndex = rnd.Next(roomList.Count);
            Room newRoom;

            switch (roomIndex)
            {
                case 0:
                    newRoom = new TreasureRoom();
                    break;
                case 1:
                    newRoom = new CombatRoom();
                    break;
                case 2:
                    newRoom = new HealingRoom();
                    break;
                case 3:
                    newRoom = new TradeRoom();
                    break;
                case 4:
                    newRoom = new LibraryRoom();
                    break;
                default:
                    newRoom = new TreasureRoom();
                    break;
            }

            return newRoom;
        }

        public void MiniMap() // minimap
        {
            char[,] miniMap = new char[mapSize, mapSize]; // create 2d character map

            for (int i = 0; i < mapList.Count; i++)
            {
                // choose row and col
                int row = i / mapSize; // first row is 0. and 0,1,2 / 3 = 0...etc
                int col = i % mapSize; // first column is 0 and 0,3,6 % 3 = 0...etc 
                //store character for each rooms at row, col in 2d array
                if (mapList[i] == currentRoom)
                {
                    miniMap[row, col] = '@'; 
                }
                else if (mapList[i] is StartingRoom)
                {
                    miniMap[row, col] = 'S';
                }
                else if (mapList[i] is BossRoom)
                {
                    miniMap[row, col] = 'B';
                }
                else if (mapList[i] is TreasureRoom)
                {
                    miniMap[row, col] = 'T';
                }
                else if (mapList[i] is CombatRoom)
                {
                    miniMap[row, col] = 'C';
                }
                else if (mapList[i] is HealingRoom)
                {
                    miniMap[row, col] = 'H';
                }
                else if (mapList[i] is TradeRoom)
                {
                    miniMap[row, col] = 'D';
                }
                else if (mapList[i] is LibraryRoom)
                {
                    miniMap[row, col] = 'L';
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nMini Map:"); 
            for (int row = 0; row < mapSize; row++) // print map
            {
                for (int col = 0; col < mapSize; col++)
                {
                    Console.Write(miniMap[row, col] + "  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("@ = You, S = Starting Room, B = Boss Room, T = Treasure Room, C = Combat Room, H = Healing Room, D = Trade Room, L = Library Room\n");
            Console.ResetColor();
        }

        // debug code
        public void PrintMapDirections() // print room number and directions
        {
            foreach (Room room in mapList)
            {
                Console.WriteLine(room.ToString());
            }
            for (int i = 0; i < mapList.Count; i++)
            {
                Room room = mapList[i];
                Console.WriteLine($"Room {i}: {room}");
                Console.WriteLine($"  North: {room.North?.ToString() ?? "None"}");
                Console.WriteLine($"  South: {room.South?.ToString() ?? "None"}");
                Console.WriteLine($"  East: {room.East?.ToString() ?? "None"}");
                Console.WriteLine($"  West: {room.West?.ToString() ?? "None"}");
            }
            Console.WriteLine($"Current Room is {currentRoom}");
        }




    }
}
