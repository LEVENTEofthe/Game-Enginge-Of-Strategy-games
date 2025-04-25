using GridbaseBattleSystem;
using System;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Numerics;
using System.Text.Json;

namespace Game_Enginge_Of_Strategy_games
{
    public partial class GEOSform : Form
    {
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

       //Testdata variables
        private match Match;
        private tileMap Map;
        private actors player1;
        private actors player2;
        private actors player3;
        private actors enemy1;
        private actors enemy2;


        public GEOSform()
        {
            //What does initialize component do anyway?
            InitializeComponent();
            uiManager = new UIManager(this);
            cameraManager = new CameraManager(defaultTileSize);

            string mapjson = File.ReadAllText("C:/Users/bakos/Documents/GEOS assets/maps/map1.json");
            Map = JsonSerializer.Deserialize<tileMap>(mapjson);
            mapdata.Text =  Map.TileData.ToString();

            tilesetImage = new Bitmap(Map.Tileset);   //apparently, you can only set only one tileset at the moment, so we should later make it so each map/match can have different tilesets or something

            //test data
            player1 = new("Index", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder.png"), 10, new tile(1,2));
            player2 = new("Sarsio", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder.png"), 10, new tile(5, 1));
            player3 = new("Adhela", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder.png"), 10, new tile(1, 3));
            enemy1 = new("Milo", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder2.png"), 10, new tile(4, 3));
            enemy2 = new("Edmond", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder2.png"), 10, new tile(4, 5));

            Match = new(Map, [player1, player2, player3], [enemy1, enemy2]);

            //this is apparently a constructor
            this.DoubleBuffered = true; // Makes drawing smoother
            this.Paint += new PaintEventHandler(GEOSform_Paint); // Hook into the Paint event(??)
            this.MouseWheel += GEOSform_MouseWheel;
            //moving the screen
            this.MouseDown += GEOSform_MouseDown;
            this.MouseUp += GEOSform_MouseUp;
            this.MouseMove += GEOSform_MouseMove;

            //debug
            countRowCol.Text = $"{Match.Map.Rows} rows, {Match.Map.Columns} columns";
        }

        private void GEOSform_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //drawing the tile map
            int tilesPerRow = tilesetImage.Width / defaultTileSize;

            for (int r = 0; r < Match.Map.Rows; r++)
            {
                for (int c = 0; c < Match.Map.Columns; c++)
                {
                    int tileIndex = Match.Map.TileData[r][c];

                    int sx = (tileIndex % tilesPerRow) * defaultTileSize;
                    int sy = (tileIndex / tilesPerRow) * defaultTileSize;
                    Rectangle tilesetSrc = new Rectangle(sx, sy, defaultTileSize, defaultTileSize);

                    float worldX = c * cameraManager.TileSize;
                    float worldY = r * cameraManager.TileSize;
                    PointF screenPos = cameraManager.WorldToScreen(worldX, worldY);
                    float size = cameraManager.TileSize * cameraManager.Zoom;

                    RectangleF tileHitbox = new RectangleF(screenPos.X, screenPos.Y, size, size);
                    g.DrawImage(tilesetImage, tileHitbox, tilesetSrc, GraphicsUnit.Pixel);

                    //g.FillRectangle(Brushes.BlueViolet, screenPos.X, screenPos.Y, size, size);
                    g.DrawRectangle(Pens.Crimson, screenPos.X, screenPos.Y, size, size);
                }
            }
            

            //Putting the actors on the grid
            (int, int)[] playerPositions = new (int, int)[Match.PlayerTeam.Length];
            (int, int)[] enemyPositions = new (int, int)[Match.EnemyTeam.Length];

            foreach (actors i in Match.PlayerTeam)
            {
                float worldX = i.MapPosition.Column * cameraManager.TileSize;
                float worldY = i.MapPosition.Row * cameraManager.TileSize;
                PointF screenPos = cameraManager.WorldToScreen(worldX, worldY);
                float size = cameraManager.TileSize * cameraManager.Zoom;

                g.DrawImage(i.Image, screenPos.X, screenPos.Y, size, size);
            }

            foreach (actors i in Match.EnemyTeam)
            {
                float worldX = i.MapPosition.Column * cameraManager.TileSize;
                float worldY = i.MapPosition.Row * cameraManager.TileSize;
                PointF screenPos = cameraManager.WorldToScreen(worldX, worldY);
                float size = cameraManager.TileSize * cameraManager.Zoom;

                g.DrawImage(i.Image, screenPos.X, screenPos.Y, size, size);
            }

            //this all bellow is still outdated (I think, not 100% sure)
            playerTiles = new (actors, Rectangle)[Match.PlayerTeam.Length];
            enemyTiles = new (actors, Rectangle)[Match.EnemyTeam.Length];
            int counter = 0;
            foreach (actors act in Match.PlayerTeam)
            {
                float worldX = act.MapPosition.Column * cameraManager.TileSize;
                float worldY = act.MapPosition.Row * cameraManager.TileSize;
                PointF screenPos = cameraManager.WorldToScreen(worldX, worldY);
                float size = cameraManager.TileSize * cameraManager.Zoom;

                playerTiles[counter] = (act, new Rectangle((int)screenPos.X, (int)screenPos.Y, (int)size, (int)size));
                counter++;
            }

            counter = 0;
            foreach (actors act in Match.EnemyTeam)
            {
                float worldX = act.MapPosition.Column * cameraManager.TileSize;
                float worldY = act.MapPosition.Row * cameraManager.TileSize;
                PointF screenPos = cameraManager.WorldToScreen(worldX, worldY);
                float size = cameraManager.TileSize * cameraManager.Zoom;

                enemyTiles[counter] = (act, new Rectangle((int)screenPos.X, (int)screenPos.Y, (int)size, (int)size));
                counter++;
            }

            //debugging
            howManyPlayerCharacters.Text = $"Number of player characters: {playerTiles.Length}";
        }

        private void Zoom(int size)
        {
            if (size > 19 && size < 101)
            {
                cameraManager.TileSize = size;
                Invalidate();
            }
        }

        private void GEOSform_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = e.Delta;

            if (delta > 0)
            {
                //the + {x} means that it will zoom by {x} much after every mousewheel rub. Math.Min is used for setting a limit
                cameraManager.TileSize = Math.Min(cameraManager.TileSize + 4, 128);
            }
            else
                cameraManager.TileSize = Math.Max(cameraManager.TileSize - 4, 20);

            Invalidate();
        }

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
        }

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

            PointF currentTile = cameraManager.ScreenToWorld(e.X, e.Y);

            //debugging
            mouseCoordinates.Text = e.Location.ToString();
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
    }
}
