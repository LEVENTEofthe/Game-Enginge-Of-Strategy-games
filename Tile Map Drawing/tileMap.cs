using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Map_Drawing
{
    public class Map
    {
        public int Columns { get; set; }
        public int Rows { get; set; }
        public string Tileset { get; set; }  //the filename
        public int[][] TileData { get; set; }    //[,] doesn't work with json so we use [][]


    }
}
