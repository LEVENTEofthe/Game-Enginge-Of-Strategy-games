using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Diagnostics;
//using System.Drawing;

namespace SRPG_library
{
    public class TileMap
    {
        public int Columns { get; private set; }
        public int Rows { get; private set; }
        public string Tileset { get; set; }
        public Tile[][] TileData { get; set; }   //We are using jagged array instead of a simple 2D array because it works better with json serialization. Though I can imagine that in the future, we'd want to have it also converted to a 2D array for stuff that works less good with jagged
        [JsonIgnore]    //MapObject is irrelevant for the MapEditor so we need to ignore it when building the map json files        
        public Tile[,] MapObject { get; set; }  //Col, Row

        //Looking at it, I'm not sure if it has any protection agains having TileData with unmatching row/col dimensions loaded.
        public TileMap(int Columns, int Rows, string Tileset, Tile[][] TileData)
        {
            this.Columns = Columns;
            this.Rows = Rows;
            this.Tileset = Tileset;
            this.TileData = TileData;
            MapObject = new Tile[Columns, Rows];
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Columns; c++)
                    if (TileData[r][c].ActorStandsHere == null)
                        MapObject[c, r] = new Tile(c, r, TileData[r][c].TilesetIndex, null, TileData[r][c].Event);
                    else
                        MapObject[c, r] = new Tile(c, r, TileData[r][c].TilesetIndex, TileData[r][c].ActorStandsHere = new Actor(TileData[r][c]), TileData[r][c].Event);
        }

        public Tile? returnTile((decimal, decimal) point)   //I wonder if there is any reason to keep it decimal instead of int
        {
            foreach (var tile in MapObject)
            {
                if (tile.Column == point.Item1 && tile.Row == point.Item2)
                    return tile;
            }
            return null;
        }

        public void placeActor(Actor actor, int column, int row)
        {
            if (MapObject[column -1, row -1].ActorStandsHere != null)
            {
                Debug.WriteLine($"There is already an actor standing on ({column},{row}) hence {actor.Name} can't be planted");
                return;
            }
            actor.Column = column;
            actor.Row = row;
            MapObject[actor.columnIndex, actor.rowIndex].ActorStandsHere = actor;
            return;
        }

        public override string ToString()
        {
            return $"map dimensions: {MapObject[0,0]}";
        }
    }
}
