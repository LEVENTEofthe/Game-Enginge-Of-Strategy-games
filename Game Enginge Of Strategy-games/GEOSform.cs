using GridbaseBattleSystem;
using System;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using System.Windows.Forms;
using SRPG_library;
using SRPG_library.actors;

namespace Game_Enginge_Of_Strategy_games
{
    public partial class GEOSform : Form
    {
        #region System variables
        //System variables
        private UIManager uiManager;
        private CameraManager cameraManager;
        private int defaultTileSize = 16;
        private (actors, Rectangle)[] playerTiles;
        private (actors, Rectangle)[] enemyTiles;
        Bitmap tilesetImage;
        //moving the screen
        private bool isDragging = false;
        private Point dragStart;
        private (int, int) tileUnderCursorHighlight;
        private match Match;
        private tileMap Map;
        private actors player1;
        private actors player2;
        private actors player3;
        private actors enemy1;
        private actors enemy2;
        //UI
        private HScrollBar xScrollBar;
        private VScrollBar yScrollBar;
        #endregion


        public GEOSform()
        {
            //What does initialize component do anyway?
            InitializeComponent();
            cameraManager = new CameraManager(defaultTileSize);
            uiManager = new UIManager(this, cameraManager);

            string mapjson = File.ReadAllText("C:/Users/bakos/Documents/GEOS data library/database/maps/map6.json");
            Map = JsonSerializer.Deserialize<tileMap>(mapjson);

            tilesetImage = new Bitmap(Map.Tileset);   //apparently, you can only set only one tileset at the moment, so we should later make it so each map/match can have different tilesets or something

            #region Test data
            //test data
            //WE DON'T YET USE THE JSON FILES DUDE DO MAKE THAT WORK
            player1 = new("Index", "C:/Users/bakos/Documents/GEOS data library/assets/actor textures/palaceholder.png", 10, (1,2));
            enemy1 = new("Milo", "C:/Users/bakos/Documents/GEOS data library/assets/actor textures/palaceholder2.png", 10, (1, 4));

            Match = new(Map, [player1], [enemy1]);
            #endregion

            //this is apparently a constructor
            this.DoubleBuffered = true; // Makes drawing smoother
            this.Paint += new PaintEventHandler(GEOSform_Paint); // Hook into the Paint event(??)
            this.MouseWheel += GEOSform_MouseWheel;
            //moving the screen
            this.MouseDown += GEOSform_MouseDown;
            this.MouseUp += GEOSform_MouseUp;
            this.MouseMove += GEOSform_MouseMove;

            #region UI
            yScrollBar = new();
            yScrollBar.Location = new Point(1147, 242);
            yScrollBar.Size = new(26, 172);
            yScrollBar.Minimum = -100;
            yScrollBar.Maximum = 500;
            yScrollBar.Value = 0;
            this.Controls.Add(yScrollBar);
            yScrollBar.Scroll += yScrollBar_Scroll;

            xScrollBar = new();
            xScrollBar.Location = new Point(508, 618);
            xScrollBar.Size = new(172, 26);
            xScrollBar.Minimum = -100;
            xScrollBar.Maximum = 500;
            xScrollBar.Value = 0;
            this.Controls.Add(xScrollBar);
            xScrollBar.Scroll += xScrollBar_Scroll;
            #endregion
        }

        private void GEOSform_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            #region Drawing the map
            int tilesPerRow = tilesetImage.Width / defaultTileSize;

            for (int r = 0; r < Match.Map.Rows; r++)
            {
                for (int c = 0; c < Match.Map.Columns; c++)
                {
                    tile Tile = Match.Map.MapObject[c, r];
                    int tileIndex = Tile.TilesetIndex;

                    int sx = (tileIndex % tilesPerRow) * defaultTileSize;
                    int sy = (tileIndex / tilesPerRow) * defaultTileSize;
                    Rectangle tilesetSrc = new Rectangle(sx, sy, defaultTileSize, defaultTileSize);

                    float worldX = c * cameraManager.TileSize;
                    float worldY = r * cameraManager.TileSize;
                    (float, float) screenPos = cameraManager.WorldToScreen(worldX, worldY);     //
                    float size = cameraManager.TileSize * cameraManager.Zoom;

                    RectangleF tileHitbox = new RectangleF(screenPos.Item1, screenPos.Item2, size, size);
                    g.DrawImage(tilesetImage, tileHitbox, tilesetSrc, GraphicsUnit.Pixel);
                }
            }

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(60, Color.Cyan)))
            {
                (float, float) screenPos = cameraManager.TileToScreen(tileUnderCursorHighlight.Item1 - 1, tileUnderCursorHighlight.Item2 - 1);      //
                g.FillRectangle(brush, screenPos.Item1, screenPos.Item2 - 1, cameraManager.TileSize, cameraManager.TileSize);
            }
            

            //Putting the actors' textures on the grid
            (int, int)[] playerPositions = new (int, int)[Match.PlayerTeam.Length];
            (int, int)[] enemyPositions = new (int, int)[Match.EnemyTeam.Length];

            foreach (actors i in Match.PlayerTeam)
            {
                float worldX = i.MapPosition.Item1 * cameraManager.TileSize;
                float worldY = i.MapPosition.Item2 * cameraManager.TileSize;
                (float, float) screenPos = cameraManager.WorldToScreen(worldX, worldY);     //
                float size = cameraManager.TileSize * cameraManager.Zoom;

                g.DrawImage(Image.FromFile(i.Image), screenPos.Item1, screenPos.Item2, size, size);
            }

            foreach (actors i in Match.EnemyTeam)
            {
                float worldX = i.MapPosition.Item1 * cameraManager.TileSize;
                float worldY = i.MapPosition.Item2 * cameraManager.TileSize;
                (float, float) screenPos = cameraManager.WorldToScreen(worldX, worldY);     //
                float size = cameraManager.TileSize * cameraManager.Zoom;

                g.DrawImage(Image.FromFile(i.Image), screenPos.Item1, screenPos.Item2, size, size);
            }
            #endregion

            //actor hitboxes
            playerTiles = new (actors, Rectangle)[Match.PlayerTeam.Length];
            enemyTiles = new (actors, Rectangle)[Match.EnemyTeam.Length];
            int counter = 0;
            foreach (actors act in Match.PlayerTeam)
            {
                float worldX = act.MapPosition.Item1 * cameraManager.TileSize;
                float worldY = act.MapPosition.Item2 * cameraManager.TileSize;
                (float, float) screenPos = cameraManager.WorldToScreen(worldX, worldY);     //
                float size = cameraManager.TileSize * cameraManager.Zoom;

                playerTiles[counter] = (act, new Rectangle((int)screenPos.Item1, (int)screenPos.Item2, (int)size, (int)size));
                counter++;
            }

            counter = 0;
            foreach (actors act in Match.EnemyTeam)
            {
                float worldX = act.MapPosition.Item1 * cameraManager.TileSize;
                float worldY = act.MapPosition.Item2 * cameraManager.TileSize;
                (float, float) screenPos = cameraManager.WorldToScreen(worldX, worldY);     //
                float size = cameraManager.TileSize * cameraManager.Zoom;

                enemyTiles[counter] = (act, new Rectangle((int)screenPos.Item1, (int)screenPos.Item2, (int)size, (int)size));
                counter++;
            }
        }

        #region Camera
        private void GEOSform_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPoint = e.Location;
                int dx = currentPoint.X - dragStart.X;
                int dy = currentPoint.Y - dragStart.Y;

                cameraManager.OffsetX += dx;
                cameraManager.OffsetY += dy;

                dragStart = currentPoint;

                Invalidate();
            }

            (decimal, decimal) currentTile = cameraManager.ScreenToTile(e.X, e.Y);
            if (Match.Map.returnTile(currentTile.Item1, currentTile.Item2) != null)
            {
                tileUnderCursorHighlight = (Convert.ToInt32(currentTile.Item1), Convert.ToInt32(currentTile.Item2));
                Invalidate();
            }

            //debugging
            mouseCoordinates.Text = e.Location.ToString();
            tileCoords.Text = cameraManager.ScreenToTile(e.X, e.Y).ToString();
        }

        private void GEOSform_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = e.Delta;

            if (delta > 0)
            {
                //the + {x} means that it will zoom by {x} much after every mousewheel rub and the second number is the limit of zooming
                cameraManager.TileSize = Math.Min(cameraManager.TileSize + 4, 128);
            }
            else
                cameraManager.TileSize = Math.Max(cameraManager.TileSize - 4, 20);

            Invalidate();
        }

        private void GEOSform_MouseDown(object sender, MouseEventArgs e)
        {
            dragStart = e.Location;

            if (clickedOnPlayerCharacter(e.Location) == null)
            {
                isDragging = true;
                dragStart = e.Location;
            }

            if (clickedOnPlayerCharacter(e.Location) != null)
            {
                clickedOnPlayerLabel.Text = $"You have just clicked on {clickedOnPlayerCharacter(e.Location).Name}";
                uiManager.OpenNewPlayerCharacterActionPanel(clickedOnPlayerCharacter(e.Location), e.Location);

            }

            if (clickedOnEnemyCharacter(e.Location) != null)
            {
                clickedOnPlayerLabel.Text = $"You have cilcked on an enemy, {clickedOnEnemyCharacter(e.Location).Name}";
            }

            //debug
            tilePicker.Text = Match.Map.returnTile(cameraManager.ScreenToTile(e.X, e.Y).Item1, cameraManager.ScreenToTile(e.X, e.Y).Item2)?.ToString();
        }

        private void GEOSform_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void xScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            cameraManager.OffsetX = xScrollBar.Value;
            Invalidate();
        }

        private void yScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            cameraManager.OffsetY = yScrollBar.Value;
            Invalidate();
        }
        #endregion

        #region Mouse events
        private actors clickedOnPlayerCharacter(Point mousePosition)  //checking if you are trying to drag on the player
        {
            foreach (var i in playerTiles)
            {
                if (i.Item2.Contains(mousePosition))
                    return i.Item1;
            }
            return null;
        }

        private actors clickedOnEnemyCharacter(Point mousePosition)
        {
            foreach (var i in enemyTiles)
            {
                if (i.Item2.Contains(mousePosition))
                    return i.Item1;
            }
            return null;
        }
        #endregion
    }
}
