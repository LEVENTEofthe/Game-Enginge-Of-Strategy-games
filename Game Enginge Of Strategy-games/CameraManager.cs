using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    internal class CameraManager
    {
        public float OffsetX { get; set; }
        public float OffsetY { get; set; }
        public float Zoom { get; set; } = 1f;
        public int TileSize { get; set; }


        public CameraManager(int TileSize)
        {
            this.TileSize = TileSize;
        }


        public PointF WorldToScreen(float x, float y)   //converting in-game map coordinates to window coordinates
        {
            return new PointF(
                (x * Zoom) + OffsetX,
                (y * Zoom) + OffsetY
            );
        }

        public PointF ScreenToWorld(float screenX, float screenY)   //converting window coordinates to in-game map coordinates
        {
            return new PointF(
                (screenX - OffsetX) / Zoom,
                (screenY - OffsetY) / Zoom
            );
        }

        public (int, int) ScreenToTile(float screenX, float screenY)     //it basically returns the row, column of the tile the cursor is on
        {
            float worldX = ScreenToWorld(screenX, screenY).X;
            float worldY = ScreenToWorld(screenX, screenY).Y;

            int tileColumn = (int)(worldX / TileSize);
            int tileRow = (int)(worldY / TileSize);

            return (tileColumn, tileRow);
        }
    }
}
