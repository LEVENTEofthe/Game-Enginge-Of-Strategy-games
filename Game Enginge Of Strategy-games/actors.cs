using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GridbaseBattleSystem
{
    internal class actors
    {
        private string name;
        private Image image;
        private int agility;
        private (int, int) mapPosition;

        public actors(string name, Image image, int agility, (int, int) mapPosition)
        {
            this.name = name;
            this.image = image;
            this.agility = agility;
            this.mapPosition = mapPosition;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        public int Agility
        {
            get { return agility; }
            set { agility = value; }
        }

        public (int, int) MapPosition
        {
            get { return mapPosition; }
            set { mapPosition = value; }
        }
    }
}