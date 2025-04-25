using GridbaseBattleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    internal class tile
    {
        private int column;
        private int row;
        public Image Texture { get; set; }
        public mapObject MapObject { get; set; }
        public actors ActorStandsHere { get; set; }

        public tile(int Column, int Row, Image Texture)
        {
            this.Row = Row;
            this.Column = Column;
            this.Texture = Texture;
        }

        public tile(int Column, int Row)
        {
            this.column = Column;
            this.row = Row;
        }

        public int Column
        {
            get { return column; }
            set {  column = value - 1; }    //-1 because otherwise we would have to count rows and columns starting from number 0
        }
        public int Row
        {
            get { return row; }
            set { row = value - 1; }
        }

        public bool CanStepHere()
        {
            bool actorStandsHere = ActorStandsHere != null;
            bool MapObjectCantBeStandedOn = MapObject != null && !MapObject.CanStandOnIt;

            return !actorStandsHere && !MapObjectCantBeStandedOn;
        }
    }
}
