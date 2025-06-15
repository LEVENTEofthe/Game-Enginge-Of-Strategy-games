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


        #region screenConvertion
        public (float, float) WorldToScreen(float x, float y)   //converting in-game map coordinates to window coordinates
        {
            (float, float) p = (
                (x * Zoom) + OffsetX,
                (y * Zoom) + OffsetY
            );

            return p;
        }
        public (float, float) WorldToScreen((float, float) point)   //converting in-game map coordinates to window coordinates
        {
            (float, float) p = (
                (point.Item1 * Zoom) + OffsetX,
                (point.Item2 * Zoom) + OffsetY
            );
            return p;
        }

        public (float, float) ScreenToWorld(float screenX, float screenY)   //converting window coordinates to map-relative coordinates
        {
            (float, float) p = (
                (screenX - OffsetX) / Zoom,
                (screenY - OffsetY) / Zoom
            );

            return p;
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
            decimal col = decimal.Truncate(Convert.ToDecimal(ScreenToWorld(screenX, screenY).Item1 / TileSize + 1));
            decimal row = decimal.Truncate(Convert.ToDecimal(ScreenToWorld(screenX, screenY).Item2 / TileSize + 1));

            return (col, row);
        }
        public decimal ScreenToTile(int screen)
        {
            return decimal.Truncate(Convert.ToDecimal(ScreenToWorld(screen / TileSize + 1)));
        }

        public (float, float) TileToWorld(decimal Col, decimal Row)
        {
            (float, float) p = (
                (float)Col * TileSize,
                (float)Row * TileSize
            );

            return p;
        }
        
        public (float, float) TileToScreen(decimal Col, decimal Row)
        {
            (float, float) World = TileToWorld(Col, Row);
            return WorldToScreen(World);
        }
        #endregion

        public bool IsInsideMap(int col, int row, int maxColumns, int maxRows)
        {
            return col >= 1 && col <= maxColumns && row >= 1 && row <= maxRows;
        }

        public (float, float) GetActorScreenPosition(actors actor)     //So I'll transfer this to the UImanager. I wonder if it would be an optimal solution to make it so it doesn't only capable of returning the location of actors, but all game objects that fit into a tile, actors included. So game objects might be an origin class for actors and other things
        {
            float worldX = actor.MapPosition.Item1 + 1;
            float worldY = actor.MapPosition.Item2 + 1;
            return WorldToScreen(worldX, worldY);
        }
    }
}
