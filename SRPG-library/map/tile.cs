using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SRPG_library
{
    public class Tile
    {
        private int column;
        private int row;
        public int TilesetIndex { get; set; }           //which texture from the tileset is used
        public string Event {  get; set; }              //Only stores the ID of Events
        public List<string> MoreEvents {  get; set; }   //In case of wanting multiple events on a single tile
        public TileInteractables MapObject { get; set; }        //Not sure we'd be needing map objects now that we have events.
        public Actors ActorStandsHere { get; set; }
        //public string[] Tags { get; set; }            //For variable things

        [JsonConstructor]
        public Tile(int Column, int Row, int TilesetIndex, Actors ActorStandsHere, string Event)
        {
            this.Column = Column;
            this.Row = Row;
            this.TilesetIndex = TilesetIndex;
            this.ActorStandsHere = ActorStandsHere;
            this.Event = Event;
        }
        public Tile(int Column, int Row, string Event)
        {
            this.Column = Column;
            this.Row = Row;
            this.Event = Event;
            TilesetIndex = 0;
        }

        public int Column
        {
            get { return column; }
            set { column = value + 1; }     //+1 because the computer starts indexing from 0, but in the context of a game map, 1th column makes more sense than 0th column. But with this approach, in every code that uses user imput actors/tiles, we have to use -1 for their value.
        }  
        public int Row
        {
            get { return row; }
            set { row = value + 1; }
        }

        public (int, int) returnTilePosition()
        {
            return (column, row);
        }

        public bool CanStepHere()
        {
            bool actorStandsHere = ActorStandsHere != null;
            bool MapObjectCantBeStandedOn = MapObject != null && !MapObject.CanStandOnIt;

            return !actorStandsHere && !MapObjectCantBeStandedOn;
        }

        public override string ToString()
        {
            return $"col: {column}, row: {row}, tileset index: {TilesetIndex}, actor: {ActorStandsHere}";
        }
    }
}
