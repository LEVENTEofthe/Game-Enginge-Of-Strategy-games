using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library.events
{
    public class ActionContext
    {
        public Actors User { get; set; }
        public TileMap Map { get; set; }
        public Tile TargetTile { get; set; }
        public Tile[] MultipleTargetTile { get; set; }
        public Actors Target { get; set; }     //target can be self; another actor; self and another actor; another actor of specified attributes (like team); area of tiles;
        public Tile UserMustStandOnSpecificTile { get; set; }
        public (decimal, decimal) MoveBy {  get; set; }

        public ActionContext(Actors user, TileMap map, Tile targetTile, (decimal, decimal) moveBy)
        {
            User = user;
            Map = map;
            TargetTile = targetTile;
            MoveBy = moveBy;
        }
    }
}
