using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library
{
    public class TileInteractables //Interactable things that occupy tiles
    {
        public MapObjectTypes MapEventType { get; set; }
        public Tile MapPosition { get; set; }
        public bool CanStandOnIt { get; set; }
    }
}
