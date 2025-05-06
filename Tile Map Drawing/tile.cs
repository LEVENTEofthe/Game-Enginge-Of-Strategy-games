using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Map_Drawing
{
    internal class tile
    {
        private int column;
        private int row;
        public int TilesetIndex { get; set; }     //which texture from the tileset is used
        //public mapObject MapObject { get; set; }
        //public actors ActorStandsHere { get; set; }

        public tile(int Column, int Row, int TilesetIndex)
        {
            this.Column = Column;
            this.Row = Row;
            this.TilesetIndex = TilesetIndex;
        }

        public int Column
        {
            get { return column; }
            set { column = value - 1; }    //-1 because otherwise we would have to count rows and columns starting from number 0
        }
        public int Row
        {
            get { return row; }
            set { row = value - 1; }
        }

        //public bool CanStepHere()
        //{
        //    bool actorStandsHere = ActorStandsHere != null;
        //    bool MapObjectCantBeStandedOn = MapObject != null && !MapObject.CanStandOnIt;

        //    return !actorStandsHere && !MapObjectCantBeStandedOn;
        //}
    }
}
