using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SRPG_library
{
    public class Actor
    {
        public string Name { get; set; }
        public string Image { get; set; }   //name of the image in GEOS data library/assets/actor textures
        public int HP { get; set; }
        //public int Attack {  get; set; } 
        public int Movement { get; set; }
        public int AttackRange { get; set; }
        //public int TurnSpeed { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public List<ISingleAction> ActionSet { get; set; }   //recently added, need to update the character creator and json reader //For now it's only SingleAction instead of Events
        public List<string> Variables { get; set; }

        [JsonConstructor]
        public Actor(string Name, string Image, int HP, int Movement, int AttackRange, int Column, int Row, List<ISingleAction> ActionSet)
        {
            this.Name = Name;
            this.Image = Image;
            this.HP = HP;
            this.Movement = Movement;
            this.AttackRange = AttackRange;
            this.Column = Column;
            this.Row = Row;
            this.ActionSet = ActionSet;

            Variables = new List<string>();
        }
        public Actor(Tile tile)
        {
            Name = tile.ActorStandsHere.Name;
            Image = tile.ActorStandsHere.Image;
            HP = tile.ActorStandsHere.HP;
            Movement = tile.ActorStandsHere.Movement;
            AttackRange = tile.ActorStandsHere.AttackRange;
            Column = tile.ActorStandsHere.Column;
            Row = tile.ActorStandsHere.Row;

            Variables = new List<string>();
        }
        public Actor() { Variables = new List<string>(); }

        [JsonIgnore]
        public int columnIndex      //The columns/rows are one-indexed, but since C# arrays are zero-indexed, we'd need an instance of Zero-indexed columns&rows
        {
            get { return Column - 1; }
            set { Column = value + 1; }    
        }
        [JsonIgnore]
        public int rowIndex
        {
            get { return Row - 1; }
            set { Row = value + 1; }
        }

        public override string ToString()
        {
            //return $"{Name}, Col: {Column}, Row: {Row}";
            //return $"{Name}, {string.Join(", ", ActionSet)}";

            return $"{Name}, HP: {HP}";
        }
    }
}