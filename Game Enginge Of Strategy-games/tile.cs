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
        public mapObject MapEvent { get; set; }
        public actors ActorStandsHere { get; set; }

        
        public bool CanStepHere()
        {
            bool actorStandsHere = ActorStandsHere != null;
            bool MapEventCantBeStandedOn = MapEvent
        }
    }
}
