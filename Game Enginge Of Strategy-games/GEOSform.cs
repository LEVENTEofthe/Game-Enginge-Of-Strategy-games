using GridbaseBattleSystem;

namespace Game_Enginge_Of_Strategy_games
{
    public partial class GEOSform : Form
    {
        private gridMap testmap;
        private actors player;
        private const int tileSize = 32;

        public GEOSform()
        {
            //What does initialize component do anyway?
            InitializeComponent();

            testmap = new(6, 6);
            player = new("Index", Image.FromFile("C:/Users/bakos/Documents/GEOS assets/actors/palaceholder.png"), 10, (5, 5));


            this.DoubleBuffered = true; // Makes drawing smoother
            this.Paint += new PaintEventHandler(GEOSform_Paint); // Hook into the Paint event(??)
        }

        private void GEOSform_Load(object sender, EventArgs e)
        {

        }

        private void GEOSform_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //Drawing the grid
            int centerX = (this.ClientSize.Width) / 2 - (testmap.Width * tileSize / 2);
            int centerY = (this.ClientSize.Height) / 2 - (testmap.Height * tileSize / 2);

            for (int x = 0; x < testmap.Width; x++) 
            { 
                for (int y = 0; y < testmap.Height; y++)
                {
                    Rectangle tileRect = new Rectangle(centerX + x * tileSize, centerY + y * tileSize, tileSize, tileSize);
                    g.DrawRectangle(Pens.White, tileRect);
                }
            }

            //Putting the player on the grid
            (int px, int py) = player.MapPosition;
            g.DrawImage(player.Image, centerX + px * tileSize, centerY + py * tileSize, tileSize, tileSize);
        }
    }
}
