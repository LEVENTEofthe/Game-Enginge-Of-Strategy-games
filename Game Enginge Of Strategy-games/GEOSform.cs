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
    public partial class GEOSform : Form        //I would like to implement a gamerules thing that contains how the basic systems of the game program would behave. Using it, the users could edit things like topdown or isometric perspective, when is the camera draggable and when is not, stuff like that which might differ from game to game but would usually stay the same through the entire game (but I would still allow them to be edited midgame)
    {
        #region System variables
        //System variables
        private int tilesetSize = 16;   //the dimension of a single tile in the tileset image, in pixels
        Bitmap tilesetImage;
        //moving the screen
        private bool isDragging = false;
        private Point dragStart;
        private Tile tileUnderCursor;
        private Match match;
        //UI
        private HScrollBar xScrollBar;
        private VScrollBar yScrollBar;
        #endregion
        
        //About the actor actions, what if we had/loaded in a default list of actions that might be used in the match, and when an actor would like to use them, they just call it by the ID and the rest is executed IDK how?
        //We could have them in the Match class object, that would make sense since the actions are only used in matches. The question is, do we put all action logic in the match or we dynamically give them it somehow?
        //As I currently see it, the action's context should be created insinde the GEOSform, cuz here you can reach both the SRPG-library and the UI manager. But it is gettin to spread to too many places.

        //The new approach is that the actor JSONs only contain the ID of each action they possess. We would have an Execute method that takes in the ID and the ActionContext, then a switch case would have all the possible actions along with their logic

        public GEOSform()
        {
            //What does initialize component do anyway?
            InitializeComponent();

            string mapjson = File.ReadAllText("C:\\Users\\bakos\\Documents\\GEOS data library\\database\\maps\\map1010.json");
            TileMap map = JsonSerializer.Deserialize<TileMap>(mapjson);

            tilesetImage = new Bitmap(map.Tileset);   //apparently, you can only set only one tileset at the moment, so we should later make it so each map/match can have different tilesets or something

            EventTileGraphics.LoadImages("C:\\Users\\bakos\\Documents\\GEOS data library\\assets\\event textures");

            match = new(map);



            this.DoubleBuffered = true; // Makes drawing smoother
            this.Paint += new PaintEventHandler(GEOSform_Paint);
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
            yScrollBar.Value = Convert.ToInt32(CameraManager.OffsetY);
            this.Controls.Add(yScrollBar);
            yScrollBar.Scroll += yScrollBar_Scroll;

            xScrollBar = new();
            xScrollBar.Location = new Point(508, 618);
            xScrollBar.Size = new(172, 26);
            xScrollBar.Minimum = -100;
            xScrollBar.Maximum = 500;
            xScrollBar.Value = Convert.ToInt32(CameraManager.OffsetX);
            this.Controls.Add(xScrollBar);
            xScrollBar.Scroll += xScrollBar_Scroll;
            #endregion
        }

        public static List<Actor> ImportActors(string folderpath)  //Is this even useful?
        {
            List<Actor> redActors = new List<Actor>();

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
                    Actor? deserialized = JsonSerializer.Deserialize<Actor>(json);

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

        public static List<Actor> LoadActorsToMatch(IEnumerable<string> actorsFilePath, Match match)   //Is this even useful?
        {
            var roster = new List<Actor>(capacity: match.PlayerTeam.Length);

            foreach (string path in actorsFilePath.Take(match.PlayerTeam.Length))
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"Actor file not found: {path}", path);
                }

                try
                {
                    var json = File.ReadAllText(path);
                    var actor = JsonSerializer.Deserialize<Actor>(json);

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

                    if (Tile.Event != null)
                    {
                        var img = EventTileGraphics.GetImage(Tile.Event);
                        g.DrawImage(img, screenPos.Item1, screenPos.Item2, size, size);
                    }

                    if (Tile.ActorStandsHere != null)
                        g.DrawImage(Image.FromFile(Tile.ActorStandsHere.Image), screenPos.Item1, screenPos.Item2, size, size);
                }
            }
            #endregion

            //highlight tile under cursor
            if (tileUnderCursor != null)
            {
                SolidBrush cursorBrush = new(Color.FromArgb(60, Color.Cyan));
                UIManager.highlightTile(tileUnderCursor.Column, tileUnderCursor.Row, cursorBrush, g);
            }

            //highlight action tiles
            Color highlight = Color.Transparent;
            if (match.SelectedAction != null)
            {
                highlight = match.SelectedAction.SelectableTileColor;
            }
            SolidBrush eventBrush = new(highlight);
            foreach (Tile tile in match.SelectableTargetTiles)
            {
                UIManager.highlightTile(tile, eventBrush, g);
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

                CameraManager.OffsetX += dx;
                CameraManager.OffsetY += dy;

                dragStart = currentPoint;

                Invalidate();
            }

            if (CameraManager.ReturnTileUnderCursor(e.Location, match.Map) != null)
            {
                tileUnderCursor = CameraManager.ReturnTileUnderCursor(e.Location, match.Map);
            }
            else
                tileUnderCursor = null;
            Invalidate();

            //debugging
            mouseCoordinates.Text = e.Location.ToString();
            //tileCoords.Text = CameraManager.ReturnTileUnderCursor(e.Location, match.Map)?.ToString();
            tileCoords.Text = CameraManager.ReturnTileUnderCursor(e.Location, match.Map)?.ActorStandsHere?.ToString();
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
        private void GEOSform_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            dragStart = e.Location;

            if (match.SelectedAction != null) //Action execute phrase
            {
                Tile clickedTile = CameraManager.ReturnTileUnderCursor(e.Location, match.Map);

                if (match.SelectableTargetTiles.Contains(clickedTile))
                {
                    match.SelectedAction.Execute(match.SelectedActor, CameraManager.ReturnTileUnderCursor(e.Location, match.Map), match.Map);

                    match.SelectedAction = null;
                    match.SelectedActor = null;
                    match.SelectableTargetTiles.Clear();

                    UIManager.ClosePlayerCharacterActionPanel(this);
                }
            }

            else    //Everything else phrase
            {
                Actor clickedActor = clickedOnPlayerCharacter(e.Location);

                if (clickedActor == null)
                {
                    //isDragging = true;
                    //dragStart = e.Location;
                }

                if (clickedActor != null)
                {
                    List<Button> buttons = new List<Button>();

                    foreach (var i in clickedActor.ActionSet)
                    {
                        Button button = new Button { Name = i.ID, Text = i.ID, Size = new(90, 27) };
                        button.Click += (s, ev) =>
                        {
                            match.ExecuteSelectedAction(i, clickedActor);
                            Invalidate();
                        };
                        buttons.Add(button);
                    }

                    UIManager.OpenNewPlayerCharacterActionPanel(this, clickedActor, e.Location, match.Map, buttons);

                    //debug
                    clickedOnPlayerLabel.Text = $"You have just clicked on {clickedActor.Name}";
                }

                if (clickedOnEnemyCharacter(e.Location) != null)
                {

                    //debug
                    clickedOnPlayerLabel.Text = $"You have cilcked on an enemy, {clickedOnEnemyCharacter(e.Location).Name}";
                }

                //debug
                tilePicker.Text = match.Map.returnTile(CameraManager.ScreenToTile(e.Location))?.ToString();
                tileInfoLabel.Text = $"Actor stands here: {match.Map.returnTile(CameraManager.ScreenToTile(e.Location))?.ActorStandsHere}, Event: {match.Map.returnTile(CameraManager.ScreenToTile(e.Location))?.Event}, Can step here: {match.Map.returnTile(CameraManager.ScreenToTile(e.Location))?.CanStepHere()} position: {match.Map.returnTile(CameraManager.ScreenToTile(e.Location))?.returnTilePosition()}";
            }
        }

        private void GEOSform_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private Actor clickedOnPlayerCharacter(Point mousePosition)
        {
            if (CameraManager.ReturnTileUnderCursor(mousePosition, match.Map)?.ActorStandsHere != null)
            {
                return CameraManager.ReturnTileUnderCursor(mousePosition, match.Map)?.ActorStandsHere;
            }
            return null;
        }

        private Actor clickedOnEnemyCharacter(Point mousePosition)
        {

            return null;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            List<ISingleAction> actlist = new List<ISingleAction> { new MoveAction(), new AttackAction() };

            Actor act = new("Ene", "C:/Users/bakos/Documents/GEOS data library/assets/actor textures/palaceholder2.png", 12, 4, 2, actlist);
            match.Map.placeActor(act, 5, 5);
            Actor atkTarget = new("dummy", "C:/Users/bakos/Documents/GEOS data library/assets/actor textures/palaceholder.png", 5, 3, 1, actlist);
            match.Map.placeActor(atkTarget, 2, 2);
        }
    }
}
