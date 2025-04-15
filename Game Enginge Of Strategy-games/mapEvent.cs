using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    internal class mapEvent //Interactable things that occupy tiles
    {
        public mapEventTypes MapEventType { get; set; }

        public bool CanStandOnIt { get; set; }

        public (int, int) MapPosition { get; set; }
    }
}
