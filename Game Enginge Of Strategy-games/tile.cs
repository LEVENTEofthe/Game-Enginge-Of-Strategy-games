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
        private int row;
        private int column;
        public Image Texture { get; set; }
        public mapObject MapObject { get; set; }
        public actors ActorStandsHere { get; set; }


        public tile(int Row, int Column, Image Texture)
        {
            this.Row = Row;
            this.Column = Column;
            this.Texture = Texture;
        }

        public tile(int Row, int Column)
        {
            this.Row = Row;
            this.Column = Column;
            Texture = Image.FromFile("C:/Users/bakos/Documents/GEOS assets/tiles/placeholder.png");
        }


        public int Row
        {
            get { return row; }
            set { row = value - 1; }    //-1 because otherwise we would have to count rows and columns starting from number 0
        }
        public int Column
        {
            get { return column; }
            set {  column = value - 1; }
        }


        public bool CanStepHere()
        {
            bool actorStandsHere = ActorStandsHere != null;
            bool MapObjectCantBeStandedOn = MapObject != null && !MapObject.CanStandOnIt;

            return !actorStandsHere && !MapObjectCantBeStandedOn;
        }
    }
}
