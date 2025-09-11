using SRPG_library;
using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Game_Enginge_Of_Strategy_games
{
    public partial class GEOSform : Form        //I would like to implement a gamerules thing that contains how the basic systems of the game program would behave. Using it, the users could edit things like topdown or isometric perspective, when is the camera draggable and when is not, stuff like that which might differ from game to game but would usually stay the same through the entire game (but I would still allow them to be edited midgame)
    {
        #region System variables
        //System variables
        private int tilesetSize = 16;   //the dimension of a single tile in the tileset image, in pixels
        Bitmap tilesetImage;
        Dictionary<string, Func<Object>> handlers;
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

            #region ActionVarialbeHandlers
            handlers = new Dictionary<string, Func<Object>>();
            handlers["ActorChooser"] = () => UIManager.ActorChooser("C:\\Users\\bakos\\Documents\\GEOS data library\\database\\actors", "C:\\Users\\bakos\\Documents\\GEOS data library\\assets\\actor textures");
            #endregion

            string mapjson = File.ReadAllText("C:\\Users\\bakos\\Documents\\GEOS data library\\database\\maps\\map1.json");
            TileMap map = JsonSerializer.Deserialize<TileMap>(mapjson);

            tilesetImage = new Bitmap(map.Tileset);   //apparently, you can only set only one tileset at the moment, so we should later make it so each map/match can have different tilesets or something

            match = new(map);
            List<IGameState> turnOrder = new List<IGameState> { new PlayerTurn_SelectingAction(this, match, handlers), new PlayerTurn_FinalizingAction(this, match, handlers) };
            match.TurnOrder = turnOrder;

            EventGraphics.LoadImages("C:\\Users\\bakos\\Documents\\GEOS data library\\assets\\event textures");

            

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





            //Debug
            Debug.WriteLine($"We're here and the turn is {match.CurrentTurn}");

            List<ISingleAction> actlist = new List<ISingleAction> { new MoveAction(), new AttackAction(), new AliceAttackAction() };
            Actor Sarsio = new("Sarsio", "C:/Users/bakos/Documents/GEOS data library/assets/actor textures/Sarsio.png", 665, 4, 1, 2, 5, actlist);
            match.Map.placeActor(Sarsio, 2, 3);
            Actor Milo = new("Milo", "C:/Users/bakos/Documents/GEOS data library/assets/actor textures/Milo.png", 20, 4, 3, 7, 5, actlist);
            Milo.Variables.Add("Edmond unit");
            match.Map.placeActor(Milo, 7, 3);
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
                        var img = EventGraphics.GetImage(Tile.Event);
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
            SolidBrush eventBrush = new(Color.FromArgb(120, Color.Purple));
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
            currentTurnLbl.Text = match.CurrentTurn.ToString();
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
            //Actor clickedActor = clickedOnPlayerCharacter(e.Location);

            match.CurrentTurn.MouseDown(e);
            Invalidate();


            //debug
            tilePicker.Text = match.Map.returnTile(CameraManager.ScreenToTile(e.Location))?.ToString();
            tileInfoLabel.Text = $"Actor stands here: {match.Map.returnTile(CameraManager.ScreenToTile(e.Location))?.ActorStandsHere}, Event: {match.Map.returnTile(CameraManager.ScreenToTile(e.Location))?.Event}, Can step here: {match.Map.returnTile(CameraManager.ScreenToTile(e.Location))?.CanStepHere()} position: {match.Map.returnTile(CameraManager.ScreenToTile(e.Location))?.returnTilePosition()}";
        }
        

        private void GEOSform_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        public Actor clickedOnPlayerCharacter(Point mousePosition)
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
            
            Debug.WriteLine("This button is for debugging actions. It doesn't do anything currently though");
        }
    }


    #region GameStates
    public class MatchSetup : IGameState
    {
        public Form ParentForm => throw new NotImplementedException();
        public Match match => throw new NotImplementedException();

        public Dictionary<string, Func<object>> Handlers => throw new NotImplementedException();

        public void MouseDown(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class PlayerTurn_SelectingAction : IGameState
    {
        public Form ParentForm {  get; set; }
        public Match match { get; set; }
        public Dictionary<string, Func<object>> Handlers { get; set; }

        public PlayerTurn_SelectingAction(Form parentForm, Match match, Dictionary<string, Func<object>> handlers)
        {
            Handlers = handlers;
            this.match = match;
            ParentForm = parentForm;
        }

        public void MouseDown(MouseEventArgs e)
        {
            IGameState gameState = this;
            Actor clickedActor = gameState.clickedOnPlayerCharacter(e.Location, match);

            if (clickedActor != null)
            {
                List<Button> buttons = new List<Button>();

                foreach (ISingleAction ActorAction in clickedActor.ActionSet)
                {
                    System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
                    

                    Button button = new Button { Name = ActorAction.ID, Text = ActorAction.ID, Size = new(90, 31), Font = new("Arial", 9) };
                    ToolTip1.SetToolTip(button, ActorAction.Description);

                    button.Click += (s, ev) =>

                    #region clicking a character action button
                    {
                        foreach (string VariableKey in ActorAction.Variables.Keys.ToList())
                        {
                            if (Handlers.TryGetValue(VariableKey, out var method))
                            {
                                ActorAction.Variables[VariableKey] = method();
                            }
                        }

                        match.ExecuteSelectedAction(ActorAction, clickedActor);

                        if (match.CurrentTurn is PlayerTurn_SelectingAction)
                            match.TurnEnd();

                        UIManager.ClosePlayerCharacterActionPanel(ParentForm);
                    };
                    #endregion

                    buttons.Add(button);
                }

                UIManager.OpenNewPlayerCharacterActionPanel(ParentForm, clickedActor, e.Location, match.Map, buttons);
            }
        }

        public override string ToString()
        {
            return "SelectingAction";
        }
    }

    public class PlayerTurn_FinalizingAction : IGameState
    {
        public Form ParentForm { get; set; }
        public Match match { get; set; }
        public Dictionary<string, Func<object>> Handlers { get; set; }

        public PlayerTurn_FinalizingAction(Form parentForm, Match match, Dictionary<string, Func<object>> handlers)
        {
            ParentForm = parentForm;
            this.match = match;
            Handlers = handlers;
        }

        public void MouseDown(MouseEventArgs e)
        {
            if (match.SelectedAction != null) //Action execute phrase
            {
                Tile clickedTile = CameraManager.ReturnTileUnderCursor(e.Location, match.Map);

                if (match.SelectableTargetTiles.Contains(clickedTile))
                {
                    match.SelectedAction.Execute(match.SelectedActor, CameraManager.ReturnTileUnderCursor(e.Location, match.Map), match.Map);

                    //
                    if (match.SelectedAction is AliceAttackAction)
                        UIManager.ShowDamageNumber(ParentForm, new Point(910, 350), 9999, Color.FromArgb(255, Color.Red));

                    match.SelectedAction = null;
                    match.SelectedActor = null;
                    match.SelectableTargetTiles.Clear();

                    UIManager.ClosePlayerCharacterActionPanel(ParentForm);

                    match.TurnEnd();
                }
            }
        }
    }

    public class EnemyTurn : IGameState
    {
        public Form ParentForm => throw new NotImplementedException();

        public Match match => throw new NotImplementedException();

        public Dictionary<string, Func<object>> Handlers => throw new NotImplementedException();

        public void MouseDown(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
