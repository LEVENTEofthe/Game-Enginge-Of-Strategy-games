using System;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using System.Windows.Forms;
using SRPG_library;
using System.Diagnostics.Tracing;

namespace Game_Enginge_Of_Strategy_games
{
    public partial class GEOSform : Form
    {
        #region System variables
        //System variables
        private int tilesetSize = 16;   //the dimension of a single tile in the tileset image, in pixels
        private (Actors, Rectangle)[] playerTiles;
        private (Actors, Rectangle)[] enemyTiles;
        Bitmap tilesetImage;
        //moving the screen
        private bool isDragging = false;
        private Point dragStart;
        private (int, int) tileUnderCursorHighlight;
        private Match match;
        //private TileMap map;
        //private List<actors> actorList;
        private Actors player1;
        private Actors player2;
        private Actors player3;
        private Actors enemy1;
        private Actors enemy2;
        //UI
        private HScrollBar xScrollBar;
        private VScrollBar yScrollBar;
        #endregion


        public GEOSform()
        {
            //What does initialize component do anyway?
            InitializeComponent();

            string mapjson = File.ReadAllText("C:/Users/bakos/Documents/GEOS data library/database/maps/map2.json");
            TileMap map = JsonSerializer.Deserialize<TileMap>(mapjson);

            tilesetImage = new Bitmap(map.Tileset);   //apparently, you can only set only one tileset at the moment, so we should later make it so each map/match can have different tilesets or something

            match = new(map);

            this.DoubleBuffered = true; // Makes drawing smoother
            this.Paint += new PaintEventHandler(GEOSform_Paint); // Hook into the Paint event
            //moving the screen
            this.MouseWheel += GEOSform_MouseWheel;
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

        public static List<Actors> ImportActors(string folderpath)  //Is this even useful?
        {
            List<Actors> redActors = new List<Actors>();

            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
                return redActors;
            }

            string[] jsonContent = Directory.GetFiles("C://Users/bakos/Documents/GEOS data library/database/actors/", "*.actor.json");

            bool wasThereError = false;
            string errorMessage = "Failed to read the file(s):\n";

            foreach (var file in jsonContent)
            {
                try
                {
                    string json = File.ReadAllText(file);
                    Actors? deserialized = JsonSerializer.Deserialize<Actors>(json);

                    if (deserialized != null)
                    {
                        redActors.Add(deserialized);
                    }
                }
                catch (Exception ex)
                {
                    wasThereError = true;
                    errorMessage += $"{Path.GetFileName(file)}: {ex.Message}\n\n";
                }
            }

            if (wasThereError)
                MessageBox.Show(errorMessage);

            return redActors;
        }

        public static List<Actors> LoadActorsToMatch(IEnumerable<string> actorsFilePath, Match match)   //Is this even useful?
        {
            var roster = new List<Actors>(capacity: match.PlayerTeam.Length);

            foreach (string path in actorsFilePath.Take(match.PlayerTeam.Length))
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"Actor file not found: {path}", path);
                }

                try
                {
                    var json = File.ReadAllText(path);
                    var actor = JsonSerializer.Deserialize<Actors>(json);

                    if (actor == null)
                    {
                        throw new InvalidDataException($"Actors file '{path}' could not be deserialised (null result).");
                    }

                    roster.Add(actor);
                }
                catch (JsonException jx)    //For when the json syntax is wrong
                {
                    throw new InvalidDataException($"Actor file '{path}' contains invalid JSON: {jx.Message}", jx);
                }
                catch (Exception ex)    //For everything else
                {
                    Debug.WriteLine($"[ActorLoader] {ex.Message}");
                    throw;  //this throw might be dangerous because it terminates the software
                }
            }
            return roster;
        }

        private void GEOSform_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            #region Drawing the map
            int tilesPerRow = tilesetImage.Width / tilesetSize;

            for (int r = 0; r < match.Map.Rows; r++)
            {
                for (int c = 0; c < match.Map.Columns; c++)
                {
                    Tile Tile = match.Map.MapObject[c, r];
                    int tileIndex = Tile.TilesetIndex;

                    int sx = (tileIndex % tilesPerRow) * tilesetSize;
                    int sy = (tileIndex / tilesPerRow) * tilesetSize;
                    Rectangle tilesetSrc = new Rectangle(sx, sy, tilesetSize, tilesetSize);

                    float worldX = c * CameraManager.TileSize;
                    float worldY = r * CameraManager.TileSize;
                    (float, float) screenPos = CameraManager.WorldToScreen(worldX, worldY);
                    float size = CameraManager.TileSize * CameraManager.Zoom;

                    RectangleF tileHitbox = new RectangleF(screenPos.Item1, screenPos.Item2, size, size);
                    g.DrawImage(tilesetImage, tileHitbox, tilesetSrc, GraphicsUnit.Pixel);
                    if (Tile.ActorStandsHere != null)
                        g.DrawImage(Image.FromFile(Tile.ActorStandsHere.Image), screenPos.Item1, screenPos.Item2, size, size);
                }
            }

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(60, Color.Cyan)))
            {
                (float, float) screenPos = CameraManager.TileToScreen(tileUnderCursorHighlight.Item1 - 1, tileUnderCursorHighlight.Item2 - 1);
                g.FillRectangle(brush, screenPos.Item1, screenPos.Item2, CameraManager.TileSize, CameraManager.TileSize);
            }
            #endregion

            //actor hitboxes
            /*
            playerTiles = new (actors, Rectangle)[Match.PlayerTeam.Length];
            enemyTiles = new (actors, Rectangle)[Match.EnemyTeam.Length];
            int counter = 0;
            foreach (actors act in Match.PlayerTeam)
            {
                float worldX = act.MapPosition.Item1 * CameraManager.TileSize;
                float worldY = act.MapPosition.Item2 * CameraManager.TileSize;
                (float, float) screenPos = CameraManager.WorldToScreen(worldX, worldY);
                float size = CameraManager.TileSize * CameraManager.Zoom;

                playerTiles[counter] = (act, new Rectangle((int)screenPos.Item1, (int)screenPos.Item2, (int)size, (int)size));
                counter++;
            }

            counter = 0;
            foreach (actors act in Match.EnemyTeam)
            {
                float worldX = act.MapPosition.Item1 * CameraManager.TileSize;
                float worldY = act.MapPosition.Item2 * CameraManager.TileSize;
                (float, float) screenPos = CameraManager.WorldToScreen(worldX, worldY);
                float size = CameraManager.TileSize * CameraManager.Zoom;

                enemyTiles[counter] = (act, new Rectangle((int)screenPos.Item1, (int)screenPos.Item2, (int)size, (int)size));
                counter++;
            }
            */
        }

        #region Camera
        private void GEOSform_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPoint = e.Location;
                int dx = currentPoint.X - dragStart.X;
                int dy = currentPoint.Y - dragStart.Y;

                CameraManager.OffsetX += dx;
                CameraManager.OffsetY += dy;

                dragStart = currentPoint;

                Invalidate();
            }

            (decimal, decimal) currentTile = CameraManager.ScreenToTile(e.X, e.Y);
            if (match.Map.returnTile(currentTile) != null)
            {
                tileUnderCursorHighlight = (Convert.ToInt32(currentTile.Item1), Convert.ToInt32(currentTile.Item2));
                Invalidate();
            }

            //debugging
            mouseCoordinates.Text = e.Location.ToString();
            tileCoords.Text = CameraManager.ScreenToTile(e.X, e.Y).ToString();
        }

        private void GEOSform_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = e.Delta;

            if (delta > 0)
            {
                //the + {x} means that it will zoom by {x} much after every mousewheel rub and the second number is the limit of zooming
                CameraManager.TileSize = Math.Min(CameraManager.TileSize + 4, 128);
            }
            else
                CameraManager.TileSize = Math.Max(CameraManager.TileSize - 4, 20);

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
                UIManager.OpenNewPlayerCharacterActionPanel(this, clickedOnPlayerCharacter(e.Location), e.Location);

            }

            if (clickedOnEnemyCharacter(e.Location) != null)
            {
                clickedOnPlayerLabel.Text = $"You have cilcked on an enemy, {clickedOnEnemyCharacter(e.Location).Name}";
            }

            //debug
            tilePicker.Text = match.Map.returnTile(CameraManager.ScreenToTile(e.Location))?.ToString();
            tileInfoLabel.Text = $"Actor stands here: {match.Map.returnTile(CameraManager.ScreenToTile(e.Location))?.ActorStandsHere}, Event: {match.Map.returnTile(CameraManager.ScreenToTile(e.Location))?.Event}, Can step here: {match.Map.returnTile(CameraManager.ScreenToTile(e.Location))?.CanStepHere()} position: {match.Map.returnTile(CameraManager.ScreenToTile(e.Location))?.returnTilePosition()}";
        }

        private void GEOSform_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void xScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            CameraManager.OffsetX = xScrollBar.Value;
            Invalidate();
        }

        private void yScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            CameraManager.OffsetY = yScrollBar.Value;
            Invalidate();
        }
        #endregion

        #region Mouse events
        private Actors clickedOnPlayerCharacter(Point mousePosition)  //checking if you are trying to drag on the player
        {
            if (match.Map.returnTile(CameraManager.ScreenToTile(mousePosition))?.ActorStandsHere != null)
            {
                actorMoveToRelativeMapPosition(match.Map.MapObject, match.Map.returnTile(CameraManager.ScreenToTile(mousePosition))?.ActorStandsHere, 2, 2);
                ////return match.Map.returnTile(CameraManager.ScreenToTile(mousePosition))?.ActorStandsHere;
            }
            return null;
        }

        private Actors clickedOnEnemyCharacter(Point mousePosition)
        {

            return null;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        //To the events
        public void actorMoveToRelativeMapPosition(Tile[,] mapObject, Actors actor, int addCol, int addRow)
        {
            Debug.WriteLine($"column: {actor.Column}, row: {actor.Row}, mapObject length: {mapObject.Length}");

            //if (mapObject[actor.Column, actor.Row].ActorStandsHere == actor)
            //    mapObject[actor.Column, actor.Row].ActorStandsHere = null;

            //actor.Column += addCol;
            //actor.Row += addRow;

            //mapObject[actor.Column, actor.Row].ActorStandsHere = actor;
                
        }
    }
}
