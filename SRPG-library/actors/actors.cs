using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SRPG_library
{
    public class Actors
    {
        public string Name { get; set; }
        public string Image { get; set; }   //name of the image in GEOS data library/assets/actor textures
        public int MaxHP { get; set; }
        public int Movement { get; set; }
        //public int TurnSpeed { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public List<SingleAction> ActionSet { get; set; }   //recently added, need to update the character creator and json reader //For now it's only SingleAction instead of Events

        [JsonConstructor]
        public Actors(string Name, string Image, int MaxHP, int Movement, int Column, int Row, List<SingleAction> ActionSet)
        {
            this.Name = Name;
            this.Image = Image;
            this.MaxHP = MaxHP;
            this.Movement = Movement;
            this.Column = Column;
            this.Row = Row;
            this.ActionSet = ActionSet;
        }
        public Actors(Tile tile)
        {
            Name = tile.ActorStandsHere.Name;
            Image = tile.ActorStandsHere.Image;
            MaxHP = tile.ActorStandsHere.MaxHP;
            Column = tile.ActorStandsHere.Column;
            Row = tile.ActorStandsHere.Row;
        }
        public Actors() { }

        public int columnIndex      //The columns/rows are one-indexed, but since C# arrays are zero-indexed, we'd need an instance of Zero-indexed columns&rows
        {
            get { return Column - 1; }
            set { Column = value + 1; }    
        }
        public int rowIndex
        {
            get { return Row - 1; }
            set { Row = value + 1; }
        }

        public override string ToString()
        {
            return $"{Name}, Col: {Column}, Row: {Row}";
        }
    }
}