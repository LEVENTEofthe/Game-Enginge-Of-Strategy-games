using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library
{
    public class mapObject //Interactable things that occupy tiles
    {
        public mapObjectTypes MapEventType { get; set; }
        public tile MapPosition { get; set; }
        public bool CanStandOnIt { get; set; }
    }
}
