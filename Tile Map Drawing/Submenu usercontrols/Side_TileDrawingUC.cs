using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tile_Map_Drawing.MenuRibbons
{
    public partial class Side_TileDrawingUC : UserControl
    {
        public Bitmap tilesetImage;
        string tilesetImageSource = "C:/Users/bakos/Documents/GEOS data library/assets/tilesets/tileset-16px-2x3.png";  //default

        public Side_TileDrawingUC()
        {
            InitializeComponent();

            //This, I think this will be moved to somewhere where it is loaded when the app is opened
            tilesetImage = new Bitmap(tilesetImageSource);
            TilesetPanel.Image = tilesetImage;
        }
    }
}
