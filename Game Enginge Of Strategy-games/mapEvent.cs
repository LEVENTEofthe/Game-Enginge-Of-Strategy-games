using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    internal class mapEvent //Interactable things that occupy tiles
    {
        private mapEventTypes mapEventType;
        private bool canStandOnIt;
        private (int, int) mapPosition;

        public mapEventTypes MapEventType { 
            get { return mapEventType; } 
            set { value = mapEventType; } 
        }

        public bool CanStandOnIt
        {
            get { return canStandOnIt; }
            set { canStandOnIt = value; }
        }

        public (int, int) MapPosition
        {
            get { return mapPosition; } 
            set { mapPosition = value; }
        }
    }
}
