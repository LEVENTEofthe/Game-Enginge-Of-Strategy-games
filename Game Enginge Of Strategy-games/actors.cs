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
        private (int, int) mapPosition { get; set; } //Col, Row

        public actors(string Name, Image Image, int Agility, (int, int) MapPosition)
        {
            this.Name = Name;
            this.Image = Image;
            this.Agility = Agility;
            this.MapPosition = MapPosition;
        }

        public (int, int) MapPosition
        {
            get { return mapPosition; }
            set { mapPosition = (value.Item1 - 1, value.Item2 - 1); }
        }

        public PointF GetScreenPosition(CameraManager cameraManager, int tileSize)
        {
            float worldX = MapPosition.Item1 * tileSize;
            float worldY = MapPosition.Item2 * tileSize;
            return cameraManager.WorldToScreen(worldX, worldY);
        }
    }
}