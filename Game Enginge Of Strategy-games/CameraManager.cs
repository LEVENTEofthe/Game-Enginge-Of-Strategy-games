using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SRPG_library;

namespace Game_Enginge_Of_Strategy_games
{
    public static class CameraManager
    {
        public static float OffsetX { get; set; } = 255f;
        public static float OffsetY { get; set; } = 200f;   //The default position of the camera
        public static float Zoom { get; set; } = 1f;
        public static int TileSize { get; set; } = 64;      //How zoomed in the map is by default
        

        public static void SetCameraPosition(float offsetX, float offsetY, float zoom)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
            Zoom = zoom;
        }

        #region screenConvertion
        public static (float, float) WorldToScreen(float x, float y)   //converting in-game map coordinates to window coordinates
        {
            (float, float) p = (
                (x * Zoom) + OffsetX,
                (y * Zoom) + OffsetY
            );

            return p;
        }
        public static (float, float) WorldToScreen((float, float) point)   //converting in-game map coordinates to window coordinates
        {
            (float, float) p = (
                (point.Item1 * Zoom) + OffsetX,
                (point.Item2 * Zoom) + OffsetY
            );
            return p;
        }

        public static (float, float) ScreenToWorld(float screenX, float screenY)   //converting window coordinates to map-relative coordinates
        {
            (float, float) p = (
                (screenX - OffsetX) / Zoom,
                (screenY - OffsetY) / Zoom
            );

            return p;
        }
        public static float ScreenToWorld(float screen)
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

        public static (decimal, decimal) ScreenToTile(int screenX, int screenY)    //converting window coordinates to tile row/column coordinates
        {
            decimal col = decimal.Truncate(Convert.ToDecimal(ScreenToWorld(screenX, screenY).Item1 / TileSize + 1));
            decimal row = decimal.Truncate(Convert.ToDecimal(ScreenToWorld(screenX, screenY).Item2 / TileSize + 1));

            return (col, row);
        }
        public static decimal ScreenToTile(int screen)
        {
            return decimal.Truncate(Convert.ToDecimal(ScreenToWorld(screen / TileSize + 1)));
        }

        public static (float, float) TileToWorld(decimal Col, decimal Row)
        {
            (float, float) p = (
                (float)Col * TileSize,
                (float)Row * TileSize
            );

            return p;
        }
        
        public static (float, float) TileToScreen(decimal Col, decimal Row)
        {
            (float, float) World = TileToWorld(Col, Row);
            return WorldToScreen(World);
        }
        #endregion

        public static bool IsInsideMap(int col, int row, int maxColumns, int maxRows)
        {
            return col >= 1 && col <= maxColumns && row >= 1 && row <= maxRows;
        }
    }
}
