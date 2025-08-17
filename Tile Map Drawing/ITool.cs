using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRPG_library;

namespace Tile_Map_Drawing
{
    public interface ITool
    {
        void HandleMouseClick(MouseEventArgs e, Tile[,] mapData, ToolContext toolContext);
        void HandleMouseMove(MouseEventArgs e);
    }

    public class TileDrawingTool : ITool
    {
        private int tileSize;
        private int selectedTileIndex;

        public TileDrawingTool(int tileSize, int selectedTileIndex)
        {
            this.tileSize = tileSize;
            this.selectedTileIndex = selectedTileIndex;
        }

        public void HandleMouseClick(MouseEventArgs e, Tile[,] mapData, ToolContext toolContext)
        {
            int x = e.X / tileSize;     //It would be nice if we could make it so the tiles had built in mouse hovering function, like it would feel better than these calculations of their location. Though I can see that it would might be less optimal. 
            int y = e.Y / tileSize;

            if (x >= 0 && y >= 0 && x < mapData.GetLength(0) && y < mapData.GetLength(1))    //Looking at it, it is kinda funny how we take in both the mapData and (rows/cols) as parameter, like the mapData is most probably has the rows/cols inside it hence they are not needed to take in (I think)
            {
                mapData[x, y].TilesetIndex = toolContext.PickedTileIndex;
            }
        }

        public void HandleMouseMove(MouseEventArgs e)
        {

        }
    }

    public class EventDrawingTool : ITool
    {
        private int tileSize;
        //private string EventID;

        public EventDrawingTool(int tileSize/*, string EventID*/)
        {
            this.tileSize = tileSize;
            //this.EventID = EventID;
        }

        public void HandleMouseClick(MouseEventArgs e, Tile[,] mapData, ToolContext toolContext)
        {
            int x = e.X / tileSize;
            int y = e.Y / tileSize;

            if (x >= 0 && y >= 0 && x < mapData.GetLength(0) && y < mapData.GetLength(1))
            {
                if (toolContext.EventId == "setDirectPlayer")
                {
                    mapData[x, y].ActorStandsHere = UImanager.ActorChooser("C://Users/bakos/Documents/GEOS data library/database/actors/", "C://Users/bakos/Documents/GEOS data library/assets/actor textures");
                    mapData[x, y].ActorStandsHere.Column = x;
                    mapData[x, y].ActorStandsHere.Row = y;
                    return;
                }
                mapData[x, y].Event = toolContext.EventId;
            }
        }

        public void HandleMouseMove(MouseEventArgs e)
        {

        }
    }
}
