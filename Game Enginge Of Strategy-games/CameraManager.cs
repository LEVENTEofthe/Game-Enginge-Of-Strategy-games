using GridbaseBattleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    public class CameraManager
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
        public PointF WorldToScreen(PointF point)   //converting in-game map coordinates to window coordinates
        {
            return new PointF(
                (point.X * Zoom) + OffsetX,
                (point.Y * Zoom) + OffsetY
            );
        }

        public PointF ScreenToWorld(float screenX, float screenY)   //converting window coordinates to map-relative coordinates
        {
            return new PointF(
                (screenX - OffsetX) / Zoom,
                (screenY - OffsetY) / Zoom
            );
        }
        public float ScreenToWorld(float screen)
        {
            return (screen - OffsetY) / Zoom;
        }

        //public (int, int) ScreenToTile(float screenX, float screenY)     //it basically returns the row, column of the tile the cursor is on
        //{
        //    float worldX = ScreenToWorld(screenX, screenY).X;
        //    float worldY = ScreenToWorld(screenX, screenY).Y;

        //    int tileColumn = (int)(worldX / TileSize);
        //    int tileRow = (int)(worldY / TileSize);

        //    return (tileColumn, tileRow);
        //}

        public (decimal, decimal) ScreenToTile(int screenX, int screenY)    //converting window coordinates to tile row/column coordinates
        {
            decimal col = decimal.Truncate(Convert.ToDecimal(ScreenToWorld(screenX, screenY).X / TileSize + 1));
            decimal row = decimal.Truncate(Convert.ToDecimal(ScreenToWorld(screenX, screenY).Y / TileSize + 1));

            return (col, row);
        }
        public decimal ScreenToTile(int screen)
        {
            return decimal.Truncate(Convert.ToDecimal(ScreenToWorld(screen / TileSize + 1)));
        }

        public PointF TileToWorld(decimal Col, decimal Row)
        {
            float worldX = (float)Col * TileSize;
            float worldY = (float)Row * TileSize;

            return new PointF(worldX, worldY);
        }

        public PointF TileToScreen(decimal Col, decimal Row)
        {
            PointF World = TileToWorld(Col, Row);
            return WorldToScreen(World);
        }

        public bool IsInsideMap(int col, int row, int maxColumns, int maxRows)
        {
            return col >= 1 && col <= maxColumns && row >= 1 && row <= maxRows;
        }
    }
}
