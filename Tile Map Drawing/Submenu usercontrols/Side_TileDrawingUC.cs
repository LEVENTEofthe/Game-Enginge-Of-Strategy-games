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
        private Bitmap tilesetImage;
        private string tilesetImageSource = "C:/Users/bakos/Documents/GEOS data library/assets/tilesets/tileset-16px-2x3.png";  //default
        private ToolContext ToolContext { get; set; }
        public int tileSize = 16;   //only sets the tile size inside the tile picker //still, there might be a chance that we will need to make this be syncthronized with the variable of the same name on the main form

        public Side_TileDrawingUC(ToolContext toolContext)
        {
            InitializeComponent();

            this.ToolContext = toolContext;
            tilesetImage = new Bitmap(tilesetImageSource);
            TilesetPanel.Image = tilesetImage;
        }

        private void TilesetPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (tilesetImage == null) return;

            int tilesPerRow = tilesetImage.Width / tileSize;
            int SelX = (ToolContext.PickedTileIndex % tilesPerRow) * tileSize;
            int SelY = (ToolContext.PickedTileIndex / tilesPerRow) * tileSize;

            g.DrawRectangle(Pens.Black, SelX, SelY, tileSize, tileSize);
        }

        private void TilesetPanel_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / tileSize;
            int y = e.Y / tileSize;

            int tilesPerRow = tilesetImage.Width / tileSize;
            ToolContext.PickedTileIndex = y * tilesPerRow + x;

            TilesetPanel.Invalidate();
        }
    }
}
