using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    internal class tile
    {
        public int X {  get; set; } //calculated in row position, not in pixel (because like tiles are never alone but are part of a map which is a bundle of em)
        public int Y { get; set; }
        public int Texture { get; set; }
        public bool Occupied { get; set; }
    }
}
