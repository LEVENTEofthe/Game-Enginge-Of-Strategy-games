using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Map_Drawing
{
    public class ToolContext
    {
        public int PickedTileIndex { get; set; }    //The index of the tile you cliced on from the active tileset in TileDrawing
        public string EventId { get; set; }
    }
}
