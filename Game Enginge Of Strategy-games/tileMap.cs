using Game_Enginge_Of_Strategy_games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridbaseBattleSystem
{
    internal class tileMap
    {
        public int Columns { get; private set; }
        public int Rows { get; private set; }
        //public tile[,] tileMapObject { get; private set; }
        public string Tileset { get; set; }
        public int[][] TileData { get; set; }

        public tileMap(int Columns, int Rows, string Tileset, int[][] TileData)
        {
            this.Columns = Columns;
            this.Rows = Rows;
            this.Tileset = Tileset;
            this.TileData = TileData;
        }

        //public tileMap(int Rows, int Columns)
        //{
        //    this.Rows = Rows;
        //    this.Columns = Columns;
        //    tileMapObject = new tile[Rows, Columns];
        //    for (int r = 0; r < Rows; r++)
        //        for (int c = 0; c < Columns; c++)
        //            tileMapObject[r,c] = new tile(r,c);
        //}
    }
}
