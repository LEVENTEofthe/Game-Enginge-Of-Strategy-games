using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator
{
    internal class actors
    {
        public string Name { get; set; }
        public string Image { get; set; }   //the filename
        public int MaxHP { get; set; }

        public actors(string Name, string Image, int MaxHP)
        {
            this.Name = Name;
            this.Image = Image;
            this.MaxHP = MaxHP;
        }
    }
}

