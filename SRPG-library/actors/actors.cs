using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
//using System.Windows.Forms.Design;
//using System.Windows.Forms.Design;

namespace SRPG_library
{
    public class actors
    {
        public string Name { get; set; }
        public string Image { get; set; }   //name of the image in GEOS data library/assets/actor textures
        public int MaxHP { get; set; }
        private (int, int) mapPosition { get; set; } //Col, Row
        //public List<characterActions> ActionSet { get; set; }   //recently added, need to update the character creator and json reader

        public actors(string Name, string Image, int MaxHP, (int, int) MapPosition/*, List<characterActions> ActionSet*/)
        {
            this.Name = Name;
            this.Image = Image;
            this.MaxHP = MaxHP;
            this.MapPosition = MapPosition;
            //this.ActionSet = ActionSet;
        }
        public actors(string Name, string Image, int MaxHP)
        {
            this.Name = Name;
            this.Image = Image;
            this.MaxHP = MaxHP;
        }
        public actors() { }

        public (int, int) MapPosition
        {
            get { return mapPosition; }
            set { mapPosition = (value.Item1 - 1, value.Item2 - 1); }
        }
    }
}