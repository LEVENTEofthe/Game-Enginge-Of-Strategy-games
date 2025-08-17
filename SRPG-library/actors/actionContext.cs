using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library
{
    public class ActionContext    //This class contains various data that an action might or might not need
    {
        public Actors Target {  get; set; }     //target can be self; another actor; self and another actor; another actor of specified attributes (like team); area of tiles;
        public Tile TargetTile { get; set; }    //can be none if the action has no defined range
        public Tile UserMustStandOnSpecificTile { get; set; }
    }
}
