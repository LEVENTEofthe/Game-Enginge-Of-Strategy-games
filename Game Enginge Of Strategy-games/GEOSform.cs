using GridbaseBattleSystem;
using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace Game_Enginge_Of_Strategy_games
{
    public partial class GEOSform : Form
    {
       //System variables
        private UIManager uiManager;    //big UI = the class itself, small ui = the object(?) we use
        private (actors, Rectangle)[] playerTiles;
        private (actors, Rectangle)[] enemyTiles;
        private int tileSize = 64;
        //moving the screen
        private bool isDragging = false;
        private Point dragStart;
        private int viewOffsetX = 0;
        private int viewOffsetY = 0;

       //Testdata variables
        private match testMatch;
        private tileMap testmap;
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

            //test data
            testmap = new(8, 6, []);
            player1 = new("Index", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder.png"), 10, (7, 6));
            player2 = new("Sarsio", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder.png"), 10, (1, 1));
            player3 = new("Adhela", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder.png"), 10, (1, 3));
            enemy1 = new("Milo", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder2.png"), 10, (4, 3));
            enemy2 = new("Edmond", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder2.png"), 10, (4, 5));

            testMatch = new(testmap, [player1, player2, player3], [enemy1, enemy2]);

            //this is apparently a constructor
            this.DoubleBuffered = true; // Makes drawing smoother
            this.Paint += new PaintEventHandler(GEOSform_Paint); // Hook into the Paint event(??)
            this.MouseWheel += GEOSform_MouseWheel;
            //moving the screen
            this.MouseDown += GEOSform_MouseDown;
            this.MouseUp += GEOSform_MouseUp;
            this.MouseMove += GEOSform_MouseMove;
        }

        private void GEOSform_Load(object sender, EventArgs e)
        {

        }

        private void GEOSform_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //creating, positioning the grid map
            int centerX = (this.ClientSize.Width) / 2 - (testMatch.Map.Rows * tileSize / 2) + viewOffsetX;
            int centerY = (this.ClientSize.Height) / 2 - (testMatch.Map.Columns * tileSize / 2) + viewOffsetY;

            for (int x = 0; x < testMatch.Map.Rows; x++)
            {
                for (int y = 0; y < testMatch.Map.Columns; y++)
                {
                    //This will dynamically update the location of the tile the player in for when we want to click on it
                    Rectangle tileRect = new Rectangle(centerX + x * tileSize, centerY + y * tileSize, tileSize, tileSize);
                    g.DrawRectangle(Pens.White, tileRect);
                }
            }

            //Putting the actors on the grid
            (int, int)[] playerPositions = new (int, int)[testMatch.PlayerTeam.Length];
            (int, int)[] enemyPositions = new (int, int)[testMatch.EnemyTeam.Length];

            //+ viewOffsetX / tileSize is used in positioning the player when scrolling
            foreach (actors i in testMatch.PlayerTeam)
            {
                g.DrawImage(i.Image, centerX + i.MapPosition.Item1 * tileSize, centerY + i.MapPosition.Item2 * tileSize, tileSize, tileSize);
            }

            foreach (actors i in testMatch.EnemyTeam)
            {
                g.DrawImage(i.Image, centerX + i.MapPosition.Item1 * tileSize, centerY + i.MapPosition.Item2 * tileSize, tileSize, tileSize);
            }


            playerTiles = new (actors, Rectangle)[testMatch.PlayerTeam.Length];
            enemyTiles = new (actors, Rectangle)[testMatch.EnemyTeam.Length];
            int counter = 0;
            foreach (actors act in testMatch.PlayerTeam)
            {
                playerTiles[counter] = (act, new Rectangle(centerX + act.MapPosition.Item1 * tileSize + viewOffsetX / tileSize, centerY + act.MapPosition.Item2 * tileSize + viewOffsetY / tileSize, tileSize, tileSize));
                counter++;
            }


            counter = 0;
            foreach (actors act in testMatch.EnemyTeam)
            {
                enemyTiles[counter] = (act, new Rectangle(centerX + act.MapPosition.Item1 * tileSize + viewOffsetX / tileSize, centerY + act.MapPosition.Item2 * tileSize + viewOffsetY / tileSize, tileSize, tileSize));
                counter++;
            }

            //debugging
            howManyPlayerCharacters.Text = $"Number of player characters: {Convert.ToString(playerTiles.Length)}";
        }

        private void Zoom(int size)
        {
            if (size > 19 && size < 101)
            {
                tileSize = size;
                Invalidate();
            }
        }

        private void GEOSform_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta /*fire power!!*/ = e.Delta;

            if (delta > 0)
            {
                //the + {x} means that it will zoom by {x} much after every mousewheel rub. Math.Min is used for setting a limit
                tileSize = Math.Min(tileSize + 4, 128);
            }
            else
                tileSize = Math.Max(tileSize - 4, 20);

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
                //difference between previous and current mouse position
                int deltaX = e.X - dragStart.X;
                int deltaY = e.Y - dragStart.Y;
                //how much the screen will move (when dragging on it)
                viewOffsetX += deltaX;
                viewOffsetY += deltaY;
                int tileOffsetX = viewOffsetX / tileSize;
                int tileOffsetY = viewOffsetY / tileSize;

                //player.MapPosition = (player.MapPosition.Item1 - tileOffsetX, player.MapPosition.Item2 - tileOffsetY);

                dragStart = e.Location;

                //Setting a border to scrolling
                if (viewOffsetX > 250)
                    viewOffsetX = 250;
                if (viewOffsetX < -250)
                    viewOffsetX = -250;
                if (viewOffsetY > 250)
                    viewOffsetY = 250;
                if (viewOffsetY < -250)
                    viewOffsetY = -250;

                //Syncing the scrollbar to dragscroll
                xScrollBar.Value = viewOffsetX;
                yScrollBar.Value = viewOffsetY;

                Invalidate();
            }

            mouseCoordinates.Text = e.Location.ToString();
        }

        private void GEOSform_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void xScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            viewOffsetX = xScrollBar.Value;

            Invalidate();

            //(int px, int py) = player.MapPosition;    //I'm not sure if this line is needed
        }

        private void yScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            viewOffsetY = yScrollBar.Value;

            Invalidate();

            //(int px, int py) = player.MapPosition;    //I'm not sure if this line is needed
        }
    }
}
