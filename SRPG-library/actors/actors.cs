using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
//using System.Windows.Forms.Design;
//using System.Windows.Forms.Design;

namespace SRPG_library
{
    public class actors
    {
        public string Name { get; set; }
        public string Image { get; set; }   //name of the image in GEOS data library/assets/actor textures
        public int MaxHP { get; set; }
        private int column;
        private int row;
        //public List<characterActions> ActionSet { get; set; }   //recently added, need to update the character creator and json reader

        public actors(string Name, string Image, int MaxHP, int Column, int Row/*, List<characterActions> ActionSet*/)
        {
            this.Name = Name;
            this.Image = Image;
            this.MaxHP = MaxHP;
            this.Column = Column;
            this.Row = Row;
            //this.ActionSet = ActionSet;
        }
        //public actors(string Name, string Image, int MaxHP)
        //{
        //    this.Name = Name;
        //    this.Image = Image;
        //    this.MaxHP = MaxHP;
        //}
        public actors(tile tile)
        {
            Name = tile.ActorStandsHere.Name;
            Image = tile.ActorStandsHere.Image;
            MaxHP = tile.ActorStandsHere.MaxHP;
            Column = tile.ActorStandsHere.Column;
            Row = tile.ActorStandsHere.Row;
        }
        public actors() { }

        public int Column
        {
            get { return column; }
            set { column = value + 1; }     //+1 because the computer starts indexing from 0 but 1th column makes more sense than 0th column
        }
        public int Row
        {
            get { return row; }
            set { row = value + 1; }
        }

        public void moveToRelativeMapPosition(int addCol, int addRow)
        {
            Column = Column + addCol;
            Row = Row + addRow;
        }

        public override string ToString()
        {
            return $"{Name}, Col: {Column}, Row: {Row}";
        }
    }
}