using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    public class mapObject //Interactable things that occupy tiles
    {
        public mapObjectTypes MapEventType { get; set; }
        public tile MapPosition { get; set; }
        public bool CanStandOnIt { get; set; }
    }
}
