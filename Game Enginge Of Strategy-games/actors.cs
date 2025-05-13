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
        public int MaxHP { get; set; }
        private (int, int) mapPosition { get; set; } //Col, Row
        public List<characterActions> ActionSet { get; set; }   //recently added, need to update the character creator and json reader

        public actors(string Name, Image Image, int MaxHP, (int, int) MapPosition, List<characterActions> ActionSet)
        {
            this.Name = Name;
            this.Image = Image;
            this.MaxHP = MaxHP;
            this.MapPosition = MapPosition;
            this.ActionSet = ActionSet;
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