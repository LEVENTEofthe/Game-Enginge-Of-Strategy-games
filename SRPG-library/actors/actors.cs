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
        private int column;
        private int row;
        public List<SingleAction> ActionSet { get; set; }   //recently added, need to update the character creator and json reader //For now it's only SingleAction instead of Events

        [JsonConstructor]
        public Actors(string Name, string Image, int MaxHP, int Column, int Row, List<SingleAction> ActionSet)
        {
            this.Name = Name;
            this.Image = Image;
            this.MaxHP = MaxHP;
            column = Column;    //The actor objects in the JSONs are already 1 indexed, so we don't want the capital Column/Row fields to offset them again
            row = Row;
            this.ActionSet = ActionSet;
        }
        public Actors(Tile tile)
        {
            Name = tile.ActorStandsHere.Name;
            Image = tile.ActorStandsHere.Image;
            MaxHP = tile.ActorStandsHere.MaxHP;
            Column = tile.ActorStandsHere.Column - 1;   //We are indexing the columns & rows from 1, but the computer does from 0, so we have to offset the indexing back
            Row = tile.ActorStandsHere.Row - 1;
        }
        public Actors() { }

        public int Column
        {
            get { return column; }
            set { column = value + 1; }     //+1 because the computer starts indexing from 0, but in the context of a game map, 1th column makes more sense than 0th column. But with this approach, in every code that uses user imput actors/tiles, we have to use -1 for their value.
        }
        public int Row
        {
            get { return row; }
            set { row = value + 1; }
        }

        public override string ToString()
        {
            return $"{Name}, Col: {Column}, Row: {Row}";
        }


        
    }
}