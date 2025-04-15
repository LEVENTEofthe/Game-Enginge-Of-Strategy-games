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
    public class actors
    {
        private (int, int) mapPosition;

        public actors(string name, Image image, int agility, (int, int) mapPosition)
        {
            this.Name = name;
            this.Image = image;
            this.Agility = agility;
            this.MapPosition = mapPosition;
        }

        //this one will come with already defined actions
        public actors(string name, Image image, int agility, (int, int) mapPosition, List<characterActions> Actions)
        {
            this.Name = name;
            this.Image = image;
            this.Agility = agility;
            this.MapPosition = mapPosition;
            this.Actions = Actions;
        }

        public string Name { get; set; }

        public Image Image { get; set; }

        public int Agility { get; set; }

        public (int, int) MapPosition
        {
            get { return mapPosition; }
            set { mapPosition.Item1 = value.Item1 - 1;  //-1 because it starts counting from 0 instead of 1
                  mapPosition.Item2 = value.Item2 - 1; }
        }

        public List<characterActions> Actions { get; private set; } = new();
    }
}