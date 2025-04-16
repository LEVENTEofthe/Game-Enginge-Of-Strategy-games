using Game_Enginge_Of_Strategy_games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace GridbaseBattleSystem
{
    internal class actors
    {
        public string Name { get; set; }
        public Image Image { get; set; }
        public int Agility { get; set; }
        public tile MapPosition { get; set; }

        public actors(string Name, Image Image, int Agility, tile MapPosition)
        {
            this.Name = Name;
            this.Image = Image;
            this.Agility = Agility;
            this.MapPosition = MapPosition;
        }

        public PointF GetScreenPosition(CameraManager cameraManager, int tileSize)
        {
            float worldX = MapPosition.Column * tileSize;
            float worldY = MapPosition.Row * tileSize;
            return cameraManager.WorldToScreen(worldX, worldY);
        }

        //public (int, int) MapPosition
        //{
        //    get { return mapPosition; }
        //    set { mapPosition.Item1 = value.Item1 - 1;  //-1 because it starts counting from 0 instead of 1
        //          mapPosition.Item2 = value.Item2 - 1; }
        //}
    }
}