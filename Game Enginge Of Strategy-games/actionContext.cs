using GridbaseBattleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    public class actionContext    //This class contains various data that an action might or might not need
    {
        public actors Target {  get; set; }     //target can be self; another actor; self and another actor; another actor of specified attributes (like team); area of tiles;
        public tile TargetTile { get; set; }    //can be none if the action has no defined range
        public tile UserMustStandOnSpecificTile { get; set; }
    }
}
