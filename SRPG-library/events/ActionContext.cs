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
        public List<Tile> MultipleTargetTile { get; set; }
        public Actors Target { get; set; }     //target can be self; another actor; self and another actor; another actor of specified attributes (like team); area of tiles;
        public Tile UserMustStandOnSpecificTile { get; set; }

        public ActionContext() { }

        public void Clear()
        {
            User = null;
            TargetTile = null;
            if (MultipleTargetTile != null)
                MultipleTargetTile.Clear();
            Target = null;
            UserMustStandOnSpecificTile = null;
        }
    }
}
