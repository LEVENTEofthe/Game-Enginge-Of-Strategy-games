using System.Reflection.Metadata;
using System.Text.Json;
using Tile_Map_Drawing.MenuRibbons;
using SRPG_library;
using System.Diagnostics;

namespace Tile_Map_Drawing
{
    public partial class TileMapEditor : Form   //We'd need to implement that actors are not hardcoded into the map, but hold as references. Meaning, if I put actor A into a tile in a map, that tile wouldn't statically have all of actor A's data, but a reference to their JSON. Meaning, if I were to edit actor A's JSON file later, their data on the map would not needed to be updated. Holding the actors statically wouldn't work anyway cuz you couldn't have stuff like leveling up
    {                                           //But again, I'm not certain that harcoding the actors into the maps would be an issue. About leveling up, I'd imagine that this system would work as there being a class for it, and each actor would hold an object of it in one of it's field. This object holds what and how does change when the actor levels up. It would make it fine if the actor JSONs only held the base stats of the actor in question, becasue as a match starts, we'd have each actor's stats get up to date with this level manager class by storing the actor's level and extracting each upgrade up to that level just before the start of the game.
                                                //And you would only ever place down an actor statically anyway if you are sure about their stats, and editing those stats will be so much easier once map importing becomes a thing.
        #region submenu instances
        private Top_PropertiesUC topPropertiesRibbon;
        private int mapColumns = 2;
        private int mapRows = 2;

        private Side_PropertiesUC sidepropertiesRibbon;
        private Top_TileDrawingUC topTileDrawingRibbon;
        private Side_TileDrawingUC sideTileDrawingRibbon;
        string tilesetImageSource = "C:/Users/bakos/Documents/GEOS data library/assets/tilesets/tileset-16px-2x3.png";  //We have to change so it will use the same source Side_TileDrawingUC does
        string eventTilesetImageSource = "C:/Users/bakos/Documents/GEOS data library/assets/tilesets/events1-16px-2x3.png";
        Bitmap tilesetImage;

        private Top_EventsUC topEventsRibbon;
        #endregion

        Size tilesetSizeMustBe = new Size(32, 48);
        int tileSize = 16;

        Tile[,] mapData;
        public int columns = 2;
        int rows = 2;
        string Event;

        private ToolContext toolContext = new();
        private ITool? activeTool;

        public TileMapEditor()
        {
            InitializeComponent();

            ShowSubmenu("Properties");   //Default

            EventTileGraphics.LoadImages("C:\\Users\\bakos\\Documents\\GEOS data library\\assets\\event textures");
        }

        private void MapDrawingField_Paint(object sender, PaintEventArgs e)     //Gotta copy here the GEOS map drawing
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

                    if (mapData[x, y].Event != null)
                    {
                        var img = EventTileGraphics.GetImage(mapData[x, y].Event);
                        g.DrawImage(img, tileHitbox);
                    }

                    if (mapData[x, y].ActorStandsHere != null)      //About drawing the actors on the map, should we stick to this "ActorStandsHere is only a string value that is a referece to the JSON's name" solution, one thing I could imagine for optimalization is that all relevant actor images are retrieved from their files as the stuff starts up, so there won't be constant serialization stuff.
                        g.DrawImage(Image.FromFile(mapData[x, y].ActorStandsHere.Image), tileHitbox, src, GraphicsUnit.Pixel);

                    //If more than one item (like actor + event or more than one event) are on the same tile, the graphics should show that somehow. Like some red mark on the upper right corner
                }
            }
        }

        private void MapDrawingField_MouseClick(object sender, MouseEventArgs e)
        {
            if (activeTool == null) return;

            activeTool.HandleMouseClick(e, mapData, toolContext);

            MapDrawingField.Invalidate();
        }

        private Tile[][] MapToJaggedArray()
        {
            Tile[][] jagged = new Tile[rows][];

            for (int y = 0; y < rows; y++)
            {
                jagged[y] = new Tile[columns];
                for (int x = 0; x < columns; x++)
                {
                    jagged[y][x] = mapData[x, y];
                }
            }
            return jagged;
        }


        private void clickedInvalidateMapBtn(object sender, EventArgs e)
        {
            if (sender is Top_PropertiesUC Ribbon)
            {
                columns = mapColumns = Ribbon.mapColumns;
                rows = mapRows = Ribbon.mapRows;
                mapData = new Tile[columns, rows];

                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < columns; x++)
                    {
                        mapData[x, y] = new Tile(x, y, null);
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
        private void ExportMap(string filePath)
        {
            TileMap export = new TileMap
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

        private void exportMapToolStripMenuItem_Click(object sender, EventArgs e)
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

        #region Submenu Switching
        private void ShowSubmenu(string SubmenuName)
        {
            Top_SubmenuPanel.Controls.Clear();
            Side_SubmenuPanel.Controls.Clear();

            UserControl TopUC = null;
            UserControl SideUC = null;

            switch (SubmenuName)
            {
                case "Properties":
                    var topribbon = new Top_PropertiesUC(mapColumns, mapRows);
                    var sideribbon = new Side_PropertiesUC();
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

        private void propertiesBtn_Click(object sender, EventArgs e)
        {
            ShowSubmenu("Properties");
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
