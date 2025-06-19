using GridbaseBattleSystem;
using SRPG_library.actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    public class tile
    {
        private int column;
        private int row;
        public int TilesetIndex { get; set; }     //which texture from the tileset is used
        public mapObject MapObject { get; set; }
        public actors ActorStandsHere { get; set; }

        public tile(int Column, int Row, int TilesetIndex)
        {
            this.Column = Column;
            this.Row = Row;
            this.TilesetIndex = TilesetIndex;
        }
        public tile(int Column, int Row)
        {
            this.Column = Column;
            this.Row = Row;
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
