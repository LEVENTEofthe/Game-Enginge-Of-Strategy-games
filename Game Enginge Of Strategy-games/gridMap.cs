using Game_Enginge_Of_Strategy_games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridbaseBattleSystem
{
    internal class gridMap
    {
        //private int[,] mapObject;   //do we really need a mapObject?

        public gridMap(int width, int height, mapEvent[] mapEvents)
        {
            this.Width = width;
            this.Height = height;
            this.MapEvents = mapEvents;
            //A map is basically this array called mapObject, which will be created upon making an object of this class. Come to think of it, do we even need it?
            //this.mapObject = new int[this.Width, this.Height];
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public mapEvent[] MapEvents { get; set; }
    }
}
