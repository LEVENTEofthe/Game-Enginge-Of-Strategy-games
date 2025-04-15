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
        public int Width { get; set; }
        public int Height { get; set; }
        public tile[,] Map { get; set; }

        public gridMap(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Map = new tile[width, height];
        }
    }
}
