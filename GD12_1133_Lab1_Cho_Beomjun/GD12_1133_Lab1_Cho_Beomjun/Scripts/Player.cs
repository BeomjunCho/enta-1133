using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GD12_1133_Lab1_Cho_Beomjun.Scripts
{
    public class Player
    {
        public string userName;
        public int score;
        public int total;
        public int DiceSides;
        public int evenNum = 0;
        public int oddNum = 0;
        public int scoreSum = 0;
        public bool haveDice6 = true;
        public bool haveDice8 = true;
        public bool haveDice12 = true;
        public bool haveDice20 = true;

        public List<int> numDices = new List<int> { 6, 8, 12, 20 };
        public List<string> strDices = new List<string> { "6 sides dice", "8 sides dice", "12 sides dice", "20 sides dice" };
    }
}
