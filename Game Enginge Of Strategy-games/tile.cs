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
        public int Row { get; set; }
        public int Column { get; set; }
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

        
        public bool CanStepHere()
        {
            bool actorStandsHere = ActorStandsHere != null;
            bool MapObjectCantBeStandedOn = MapObject != null && !MapObject.CanStandOnIt;

            return !actorStandsHere && !MapObjectCantBeStandedOn;
        }
    }
}
