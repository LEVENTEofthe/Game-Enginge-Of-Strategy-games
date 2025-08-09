using GridbaseBattleSystem;
using SRPG_library.actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    public class tile
    {
        private int column;
        private int row;
        public int TilesetIndex { get; set; }           //which texture from the tileset is used
        public string Event {  get; set; }              //Only stores the ID of Events
        public List<string> MoreEvents {  get; set; }   //In case of wanting multiple events on a single tile
        public mapObject MapObject { get; set; }        //Not sure we'd be needing map objects now that we have events.
        public actors ActorStandsHere { get; set; }

        [JsonConstructor]
        public tile(int Column, int Row, int TilesetIndex, string Event)
        {
            this.Column = Column;
            this.Row = Row;
            this.TilesetIndex = TilesetIndex;
            this.Event = Event;
        }
        public tile(int Column, int Row, string Event)
        {
            this.Column = Column;
            this.Row = Row;
            this.Event = Event;
            TilesetIndex = 0;
        }

        public int Column
        {
            get { return column; }
            set { column = value + 1; }  //+1 because otherwise we would have to index rows and columns starting from number 0 (shouldn't this be -1 instead? like the machine starts counting from zero, so if we want to refer to the first object by saying 1 it would need to get 0, so 1 -1)
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
