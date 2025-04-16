using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    internal class CameraManager
    {
        public float OffsetX { get; set; }
        public float OffsetY { get; set; }
        public float Zoom { get; set; } = 1f;   //what is '= 1f'?


        public PointF WorldToScreen(float x, float y)   //what do these two are needed for?
        {
            return new PointF(
                (x * Zoom) + OffsetX,
                (y * Zoom) + OffsetY
            );
        }

        public PointF ScreenToWorld(float screenX, float screenY)
        {
            return new PointF(
                (screenX - OffsetX) / Zoom,
                (screenY - OffsetY) / Zoom
            );
        }
    }
}
