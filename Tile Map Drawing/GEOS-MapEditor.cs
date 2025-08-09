using System.Reflection.Metadata;
using System.Text.Json;
using Tile_Map_Drawing.MenuRibbons;
using SRPG_library;

namespace Tile_Map_Drawing
{
    public partial class TileMapEditor : Form
    {
        #region submenu instances
        private Top_MapParametersUC topMapParametersRibbon;
        private int MapParameterColumns = 2;
        private int MapParameterRows = 2;

        private Side_MapParametersUC sideMapParametersRibbon;
        private Top_TileDrawingUC topTileDrawingRibbon;
        private Side_TileDrawingUC sideTileDrawingRibbon;
        string tilesetImageSource = "C:/Users/bakos/Documents/GEOS data library/assets/tilesets/tileset-16px-2x3.png";  //We have to change so it will use the same source Side_TileDrawingUC does
        string eventTilesetImageSource = "C:/Users/bakos/Documents/GEOS data library/assets/tilesets/events1-16px-2x3.png";
        Bitmap tilesetImage;

        private Top_EventsUC topEventsRibbon;
        #endregion

        Size tilesetSizeMustBe = new Size(32, 48);
        int tileSize = 16;

        tile[,] mapData;
        public int columns = 2;
        int rows = 2;
        string Event;

        private ToolContext toolContext = new();
        private ITool? activeTool; 

        public TileMapEditor()
        {
            InitializeComponent();

            ShowSubmenu("MapParameters");   //Default
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
                    int tileIndex = 0;
                    string eventID = "";
                    tileIndex = mapData[x, y].TilesetIndex;
                    eventID = mapData[x, y].Event;
                    int sourcex = (tileIndex % tilesPerRow) * tileSize;
                    int sourcey = (tileIndex / tilesPerRow) * tileSize;

                    Rectangle src = new Rectangle(sourcex, sourcey, tileSize, tileSize);
                    Rectangle tileHitbox = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);

                    g.DrawImage(tilesetImage, tileHitbox, src, GraphicsUnit.Pixel);
                    g.DrawRectangle(Pens.Gray, tileHitbox);
                }
            }
        }

        private void MapDrawingField_MouseClick(object sender, MouseEventArgs e)
        {
            if (activeTool == null) return;

            activeTool.HandleMouseClick(e, mapData, toolContext);

            MapDrawingField.Invalidate();
        }

        private tile[][] MapToJaggedArray()
        {
            tile[][] jagged = new tile[rows][];

            for (int y = 0; y < rows; y++)
            {
                jagged[y] = new tile[columns];
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
            if (sender is Top_MapParametersUC Ribbon)
            {
                columns = MapParameterColumns = Ribbon.mapColumns;
                rows = MapParameterRows = Ribbon.mapRows;
                mapData = new tile[columns, rows];

                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < columns; x++)
                    {
                        mapData[x, y] = new tile(x, y, null);
                    }
                }

                MapDrawingField.Width = columns * tileSize;
                MapDrawingField.Height = rows * tileSize;

                tilesetImage = new Bitmap(tilesetImageSource);

                MapDrawingField.Invalidate();
            }
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

        #region Submenu Switching
        private void ShowSubmenu(string SubmenuName)
        {
            Top_SubmenuPanel.Controls.Clear();
            Side_SubmenuPanel.Controls.Clear();

            UserControl TopUC = null;
            UserControl SideUC = null;

            switch (SubmenuName)
            {
                case "MapParameters":
                    var topribbon = new Top_MapParametersUC(MapParameterColumns, MapParameterRows);
                    var sideribbon = new Side_MapParametersUC();
                    //subscribing to events     //I wonder if we need to do unsubscribing as well
                    topribbon.clickedInvalidateMapBt += clickedInvalidateMapBtn;

                    TopUC = topribbon;
                    SideUC = sideribbon;
                    break;

                case "Draw":
                    var drawSideRibbon = new Side_TileDrawingUC(toolContext);

                    TopUC = new Top_TileDrawingUC();
                    SideUC = drawSideRibbon;

                    sideTileDrawingRibbon = drawSideRibbon;
                    break;

                case "Events":
                    TopUC = new Top_EventsUC(toolContext);
                    break;
            }

            if (TopUC != null)
            {
                TopUC.Dock = DockStyle.Fill;
                Top_SubmenuPanel.Controls.Add(TopUC);
            }
            if (SideUC != null)
            {
                SideUC.Dock = DockStyle.Fill;
                Side_SubmenuPanel.Controls.Add(SideUC);
            }
        }

        private void mapParameterMenuBtn_Click(object sender, EventArgs e)
        {
            ShowSubmenu("MapParameters");
        }

        private void drawMenuBtn_Click(object sender, EventArgs e)
        {
            ShowSubmenu("Draw");
            activeTool = new TileDrawingTool(tileSize, toolContext.PickedTileIndex);
        }

        private void eventMenuBtn_Click(object sender, EventArgs e)
        {
            ShowSubmenu("Events");
            activeTool = new EventDrawingTool(tileSize);
        }
        #endregion
    }
}
