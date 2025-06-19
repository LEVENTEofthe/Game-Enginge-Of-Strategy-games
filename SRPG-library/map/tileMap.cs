using Game_Enginge_Of_Strategy_games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace GridbaseBattleSystem
{
    public class tileMap
    {
        public int Columns { get; private set; }
        public int Rows { get; private set; }
        public string Tileset { get; set; }
        public int[][] TileData { get; set; }
        [JsonIgnore]    //MapObject is irrelevant for the MapEditor so we need to ignore it when building the map json files
        public tile[,] MapObject { get; private set; }  //Col, Row

        //Looking at it, I'm not sure if it has any protection agains having TileData with unmatching row/col dimensions loaded.
        public tileMap(int Columns, int Rows, string Tileset, int[][] TileData)
        {
            this.Columns = Columns;
            this.Rows = Rows;
            this.Tileset = Tileset;
            this.TileData = TileData;
            MapObject = new tile[Columns, Rows];
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Columns; c++)
                    MapObject[c, r] = new tile(c, r, TileData[r][c]);
        }

        public tile? returnTile(decimal column, decimal row)
        {
            foreach (var tile in MapObject)
            {
                if (tile.Column == column && tile.Row == row)
                    return tile;
            }
            return null;
        }

        public override string ToString()
        {
            return $"map dimensions: {MapObject[0,0]}";
        }
    }
}
