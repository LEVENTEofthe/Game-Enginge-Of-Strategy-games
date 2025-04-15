using GridbaseBattleSystem;
using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace Game_Enginge_Of_Strategy_games
{
    public partial class GEOSform : Form
    {
        private UIManager uiManager;    //big UI = the class itself, small ui = the object(?) we use
        private match testMatch;
        private gridMap testmap;
        private actors player1;
        private actors player2;
        private actors player3;
        private actors enemy1;
        private actors enemy2;
        private (actors, Rectangle)[] playerTiles;
        private (actors, Rectangle)[] enemyTiles;
        private int tileSize = 64;
        //moving the screen
        private bool isDragging = false;
        private Point dragStart;
        private int viewOffsetX = 0;
        private int viewOffsetY = 0;


        public GEOSform()
        {
            //What does initialize component do anyway?
            InitializeComponent();
            uiManager = new UIManager(this, 64, testMatch);

            //test data
            testmap = new(8, 6);
            player1 = new("Index", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder.png"), 10, (7, 6));
            player2 = new("Sarsio", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder.png"), 10, (1, 1));
            player3 = new("Adhela", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder.png"), 10, (1, 3));
            enemy1 = new("Milo", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder2.png"), 10, (4, 3));
            enemy2 = new("Edmond", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder2.png"), 10, (4, 5));

            testMatch = new(testmap, [player1, player2, player3], [enemy1, enemy2]);

            //this is apparently a constructor
            this.DoubleBuffered = true; // Makes drawing smoother
            this.Paint += new PaintEventHandler(GEOSform_Paint);
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

            //creating the map
            uiManager.CreateGridmap(sender, e, testMatch);
            uiManager.DrawingCharactersOnMap(g, testMatch);

            //playerTiles = new (actors, Rectangle)[testMatch.PlayerTeam.Length];
            //enemyTiles = new (actors, Rectangle)[testMatch.EnemyTeam.Length];
            //int counter = 0;
            //foreach (actors act in testMatch.PlayerTeam)
            //{
            //    playerTiles[counter] = (act, new Rectangle(((this.ClientSize.Width) / 2 - (testmap.Width * 64 / 2) + viewOffsetX) + act.MapPosition.Item1 * tileSize + viewOffsetX / tileSize, ((this.ClientSize.Height) / 2 - (testmap.Height * 64 / 2) + viewOffsetY) + act.MapPosition.Item2 * tileSize + viewOffsetY / tileSize, tileSize, tileSize));
            //    counter++;
            //}


            //counter = 0;
            //foreach (actors act in testMatch.EnemyTeam)
            //{
            //    enemyTiles[counter] = (act, new Rectangle(((this.ClientSize.Width) / 2 - (testmap.Width * 64 / 2) + viewOffsetX) + act.MapPosition.Item1 * tileSize + viewOffsetX / tileSize, ((this.ClientSize.Height) / 2 - (testmap.Height * 64 / 2) + viewOffsetY) + act.MapPosition.Item2 * tileSize + viewOffsetY / tileSize, tileSize, tileSize));
            //    counter++;
            //}
        }

        private void GEOSform_MouseWheel(object sender, MouseEventArgs e)
        {
            uiManager.MouseWheel(sender, e);
        }

        //private actors clickedOnPlayerCharacter(Point mousePosition)
        //{
        //    foreach (var i in playerTiles)
        //    {
        //        if (i.Item2.Contains(mousePosition))
        //            return i.Item1;
        //    }
        //    return null;
        //}


        //private actors clickedOnEnemyCharacter(Point mousePosition)
        //{
        //    foreach (var i in enemyTiles)
        //    {
        //        if (i.Item2.Contains(mousePosition))
        //            return i.Item1;
        //    }
        //    return null;
        //}

        private void GEOSform_MouseDown(object sender, MouseEventArgs e)
        {
            dragStart = e.Location;

            if (uiManager.clickedOnPlayerCharacter(e.Location) == null /*&& clickedOnEnemyCharacter(e.Location) == null*/)
            {
                isDragging = true;
                dragStart = e.Location;
            }

            if (uiManager.clickedOnPlayerCharacter(e.Location) != null)
            {
                clickedOnPlayerLabel.Text = $"You have just clicked on {uiManager.clickedOnPlayerCharacter(e.Location).Name}";
                uiManager.OpenNewPlayerCharacterActionPanel(uiManager.clickedOnPlayerCharacter(e.Location), e.Location);

            }

            //if (clickedOnEnemyCharacter(e.Location) != null)
            //{
            //    clickedOnPlayerLabel.Text = $"You have cilcked on an enemy, {clickedOnEnemyCharacter(e.Location).Name}";
            //    uiManager.OpenNewEvemyCharacterInfoPanel(clickedOnEnemyCharacter(e.Location), e.Location);
            //}
        }

        private void GEOSform_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                //difference between previous and current mouse position
                int deltaX = e.X - dragStart.X;
                int deltaY = e.Y - dragStart.Y;
                //how much the screen will move (when dragging on it)
                uiManager.ViewOffsetX += deltaX;
                uiManager.ViewOffsetY += deltaY;
                int tileOffsetX = viewOffsetX / tileSize;
                int tileOffsetY = viewOffsetY / tileSize;

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
            tileCoordinates.Text = e.Location.ToString();
        }

        private void GEOSform_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void xScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            viewOffsetX = xScrollBar.Value;

            Invalidate();
        }

        private void yScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            viewOffsetY = yScrollBar.Value;

            Invalidate();
        }
    }
}
