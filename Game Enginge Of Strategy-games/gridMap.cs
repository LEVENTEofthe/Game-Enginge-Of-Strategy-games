using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridbaseBattleSystem
{
    internal class gridMap
    {
        private int width;
        private int height;
        private int[,] mapObject;

        public gridMap(int width, int height)
        {
            this.width = width;
            this.height = height;
            //A map is basically this array called mapObject, which will be created upon making an object of this class
            this.mapObject = new int[this.width, this.height];
        }

        public int Width { 
            get { return width; } 
            set { width = value; } 
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }
    }
}
