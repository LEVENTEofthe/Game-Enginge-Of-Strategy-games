using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using SRPG_library.actors;

namespace SRPG_library
{
    public class Actor
    {
        public string Name { get; set; }
        public string Image { get; set; }   //name of the image in GEOS data library/assets/actor textures
        public int HP { get; set; }
        public int TurnSpeed { get; set; }
        public List<ISingleAction> ActionSet { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public ActorAI AI { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }

        [JsonConstructor]
        public Actor(string Name, string Image, int HP, int TurnSpeed, List<ISingleAction> ActionSet, Dictionary<string, object> Variables, ActorAI AI, int Column, int Row)
        {
            this.Name = Name;
            this.Image = Image;
            this.HP = HP;
            this.TurnSpeed = TurnSpeed;
            this.ActionSet = ActionSet;
            this.Variables = Variables;
            this.AI = AI;
            this.Column = Column;
            this.Row = Row;
        }
        public Actor(Tile tile)
        {
            Name = tile.ActorStandsHere.Name;
            Image = tile.ActorStandsHere.Image;
            HP = tile.ActorStandsHere.HP;
            TurnSpeed = tile.ActorStandsHere.TurnSpeed;
            ActionSet = tile.ActorStandsHere.ActionSet;
            Variables = tile.ActorStandsHere.Variables;
            AI = tile.ActorStandsHere.AI;
            Column = tile.ActorStandsHere.Column;
            Row = tile.ActorStandsHere.Row;
        }
        public Actor() { }

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
            return $"{Name}, HP: {HP}, Col: {Column}, Row: {Row}";
            //return $"{Name}, {string.Join(", ", ActionSet)}";
        }
    }
}