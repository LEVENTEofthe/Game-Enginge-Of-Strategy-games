using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tile_Map_Drawing
{
    internal interface ITool
    {
        void HandleMouseClick(MouseEventArgs e, int[,] mapData, int selectedTileIndex);
        void HandleMouseMove(MouseEventArgs e);
    }

    public class TileDrawingTool : ITool
    {
        private int tileSize;
        private int columns;
        private int rows;
        private int selectedTileIndex;

        public TileDrawingTool(int tileSize, int columns, int rows, int selectedTileIndex)
        {
            this.tileSize = tileSize;
            this.columns = columns;
            this.rows = rows;
            this.selectedTileIndex = selectedTileIndex;
        }

        public void HandleMouseClick(MouseEventArgs e, int[,] mapData, int selectedTileIndex)
        {
            int x = e.X / tileSize;     //It would be nice if we could make it so the tiles had built in mouse hovering function, like it would feel better than these calculations of their location. Though I can see that it would might be less optimal. 
            int y = e.Y / tileSize;

            if (x >= 0 && y >= 0 && x < columns && y < rows)    //Looking at it, it is kinda funny how we take in both the mapData and (rows/cols) as parameter, like the mapData is most probably has the rows/cols inside it hence they are not needed to take in (I think)
            {
                mapData[x, y] = selectedTileIndex;
            }
        }

        public void HandleMouseMove(MouseEventArgs e)
        {

        }
    }
}
