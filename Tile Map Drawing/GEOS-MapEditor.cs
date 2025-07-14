using System.Reflection.Metadata;
using System.Text.Json;
using Tile_Map_Drawing.MenuRibbons;
using SRPG_library;
using GridbaseBattleSystem;

namespace Tile_Map_Drawing
{
    public partial class TileMapEditor : Form
    {
        private Top_MapParametersRibbon mapParametersRibbon;

        Size tilesetSizeMustBe = new Size(32, 48);
        int tileSize = 16;

        int tileImageSize = 2;  //not yet used
        string tilesetImageSource = "C:/Users/bakos/Documents/GEOS data library/assets/tilesets/tileset-16px-2x3.png";  //default
        Bitmap tilesetImage;
        int selectedTileIndex = 0;

        int[,] mapData;
        public int columns = 2;
        int rows = 2;

        public TileMapEditor()
        {
            InitializeComponent();

            mapParametersRibbon = new Top_MapParametersRibbon();
            mapParametersRibbon.clickedInvalidateMapBt += clickedInvalidateMapBtn;
            Top_ribbonPanel.Controls.Clear();
            Top_ribbonPanel.Controls.Add(mapParametersRibbon);
        }

        private void MapDrawingField_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (tilesetImage == null) return;

            int tilesPerRow = tilesetImage.Width / tileSize;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    int tileIndex = mapData[x, y];
                    int sx = (tileIndex % tilesPerRow) * tileSize;
                    int sy = (tileIndex / tilesPerRow) * tileSize;

                    Rectangle src = new Rectangle(sx, sy, tileSize, tileSize);
                    Rectangle tileHitbox = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);

                    g.DrawImage(tilesetImage, tileHitbox, src, GraphicsUnit.Pixel);
                    g.DrawRectangle(Pens.Gray, tileHitbox);
                }
            }
        }

        private void TilesetPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (tilesetImage == null) return;

            int tilesPerRow = tilesetImage.Width / tileSize;
            int SelX = (selectedTileIndex % tilesPerRow) * tileSize;
            int SelY = (selectedTileIndex / tilesPerRow) * tileSize;

            g.DrawRectangle(Pens.Black, SelX, SelY, tileSize, tileSize);
        }

        private void TilesetPanel_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / tileSize;
            int y = e.Y / tileSize;

            int tilesPerRow = tilesetImage.Width / tileSize;
            selectedTileIndex = y * tilesPerRow + x;

            //TilesetPanel.Invalidate();    //
        }

        private void MapDrawingField_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / tileSize;
            int y = e.Y / tileSize;

            if (x >= 0 && y >= 0 && x < columns && y < rows)
            {
                mapData[x, y] = selectedTileIndex;
                MapDrawingField.Invalidate();
            }
        }

        private int[][] MapToJaggedArray()
        {
            int[][] jagged = new int[rows][];

            for (int y = 0; y < rows; y++)
            {
                jagged[y] = new int[columns];
                for (int x = 0; x < columns; x++)
                {
                    jagged[y][x] = mapData[x, y];
                }
            }
            return jagged;
        }

        private void ExportMap(string filePath)
        {
            tileMap export = new tileMap
            (
                columns,
                rows,
                tilesetImageSource,
                MapToJaggedArray()
            );

            var options = new JsonSerializerOptions { WriteIndented = true };   //what is this option thing for?
            string json = JsonSerializer.Serialize(export, options);
            File.WriteAllText(filePath, json);
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JSON files (*.json)|*.json";
                sfd.InitialDirectory = @"C:\Users\bakos\Documents\GEOS data library/database/maps/";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportMap(sfd.FileName);
                }
            }
        }

        private void clickedInvalidateMapBtn(object sender, EventArgs e)
        {
            columns = mapParametersRibbon.mapColumns;
            rows = mapParametersRibbon.mapRows;

            mapData = new int[columns, rows];

            MapDrawingField.Width = columns * tileSize;
            MapDrawingField.Height = rows * tileSize;

            tilesetImage = new Bitmap(tilesetImageSource);
            //TilesetPanel.Image = tilesetImage;    //

            MapDrawingField.Invalidate();
        }

        private void tilesetSelectBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image";
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            ofd.InitialDirectory = @"C:/Users/bakos/Documents/GEOS data library/assets/tilesets";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image tryImage = null;
                try
                {
                    tryImage = Image.FromFile(ofd.FileName);

                    if (tryImage.Size == tilesetSizeMustBe)
                    {
                        tilesetImageSource = ofd.FileName;
                        tilesetImage = new Bitmap(tilesetImageSource);
                        //TilesetPanel.Image = tilesetImage;    //
                        MapDrawingField.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show($"Invalid image size: {tryImage.Size}", "Invalid size", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tryImage.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load image: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tryImage?.Dispose();
                }
            }
        }

        #region Ribbon Menu Switching
        private void ShowRibbonTab(string RibbonName)
        {
            Top_ribbonPanel.Controls.Clear();
            Side_ribbonPanel.Controls.Clear();

            UserControl Top_ribbon = null;
            UserControl Side_ribbon = null;

            switch (RibbonName)
            {
                case "MapParameters":
                    Top_ribbon = new Top_MapParametersRibbon();
                    Side_ribbon = new Side_MapParametersRibbon();
                    break;
                case "Draw":
                    Top_ribbon = new Top_TileDrawingRibbon();
                    Side_ribbon = new Side_TileDrawingRibbon();
                    break;
                case "Events":
                    Top_ribbon = new Top_EventsRibbon();
                    break;
            }

            if (Top_ribbon != null)
            {
                Top_ribbon.Dock = DockStyle.Fill;
                Top_ribbonPanel.Controls.Add(Top_ribbon);
            }
            if (Side_ribbon != null)
            {
                Side_ribbon.Dock = DockStyle.Fill;
                Side_ribbonPanel.Controls.Add(Side_ribbon);
            }
        }

        private void mapParameterMenuBtn_Click(object sender, EventArgs e)
        {
            ShowRibbonTab("MapParameters");
        }

        private void drawMenuBtn_Click(object sender, EventArgs e)
        {
            ShowRibbonTab("Draw");
        }

        private void eventMenuBtn_Click(object sender, EventArgs e)
        {
            ShowRibbonTab("Events");
        }
        #endregion
    }
}
