using GridbaseBattleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Game_Enginge_Of_Strategy_games
{
    internal class UIManager    //this class will make the main form's work easier by handling all UI releated processes. That's good because now the main form will only need to be responsible for the app and "high level events" logic while the UI logics will be separated here, making it way more clean and easier to customise.
    {
        private Form parentForm;
        public int TileSize {  get; set; }
        public int ViewOffsetX {  get; set; }
        public int ViewOffsetY { get; set; }
        public List<(actors, Rectangle)> PlayerTiles;
        public List<(actors, Rectangle)> EnemyTiles;
        public match Match { get; set; }
        private Panel currentActorActionPanel;


        public UIManager(Form Form, int TileSize, match Match)
        {
            parentForm = Form;
            this.TileSize = TileSize;
            this.Match = Match;
            PlayerTiles = new List<(actors, Rectangle)>();
        }

        public void updatePlayerTiles()
        {
            PlayerTiles = new List<(actors, Rectangle)>();
            foreach (actors act in Match.PlayerTeam)
            {
                var rect = new Rectangle(((parentForm.ClientSize.Width) / 2 - (parentForm.Width * 64 / 2) + ViewOffsetX) + act.MapPosition.Item1 * TileSize + ViewOffsetX / TileSize, ((parentForm.ClientSize.Height) / 2 - (Match.Map.Height * 64 / 2) + ViewOffsetY) + act.MapPosition.Item2 * TileSize + ViewOffsetY / TileSize, TileSize, TileSize);

                PlayerTiles.Add((act, rect));
            }
        }



        //public List<(actors, Rectangle)> EnemyTiles
        //{
        //    get { return enemyTiles; }
        //    set
        //    {
        //        int counter = 0;
        //        foreach (actors act in Match.EnemyTeam)
        //        {
        //            enemyTiles[counter] = (act, new Rectangle(((parentForm.ClientSize.Width) / 2 - (Match.Map.Width * 64 / 2) + ViewOffsetX) + act.MapPosition.Item1 * TileSize + ViewOffsetX / TileSize, ((parentForm.ClientSize.Height) / 2 - (Match.Map.Height * 64 / 2) + ViewOffsetY) + act.MapPosition.Item2 * TileSize + ViewOffsetY / TileSize, TileSize, TileSize));
        //            counter++;
        //        }
        //    }
        //}


        //Drawing object graphics
        public void CreateGridmap(object sender, PaintEventArgs e, match match)
        {
            Graphics g = e.Graphics;

            //creating, positioning the grid map
            int centerX = (parentForm.ClientSize.Width) / 2 - (match.Map.Width * TileSize / 2) + ViewOffsetX;
            int centerY = (parentForm.ClientSize.Height) / 2 - (match.Map.Height * TileSize / 2) + ViewOffsetY;

            for (int x = 0; x < match.Map.Width; x++)
            {
                for (int y = 0; y < match.Map.Height; y++)
                {
                    Rectangle tileRect = new Rectangle(centerX + x * TileSize, centerY + y * TileSize, TileSize, TileSize);
                    g.DrawRectangle(Pens.White, tileRect);
                }
            }
        }

        public void DrawingCharactersOnMap(Graphics g, match match)
        {
            foreach (actors i in match.PlayerTeam)
            {
                g.DrawImage(i.Image, ((parentForm.ClientSize.Width) / 2 - (match.Map.Width * 64 / 2) + ViewOffsetX) + i.MapPosition.Item1 * TileSize, ((parentForm.ClientSize.Height) / 2 - (match.Map.Height * 64 / 2) + ViewOffsetY) + i.MapPosition.Item2 * TileSize, TileSize, TileSize);
            }
        }


        //Drawing object bodies
        public actors clickedOnPlayerCharacter(Point mousePosition)
        {
            foreach (var i in PlayerTiles)
            {
                if (i.Item2.Contains(mousePosition))
                    return i.Item1;
            }
            return null;
        }


        //Screen movements
        //this seems to be not implemented yet
        public void Zoom(int size)
        {
            if (size > 19 && size < 101)
            {
                TileSize = size;
                parentForm.Invalidate();
            }
        }

        public void MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = e.Delta;

            if (delta > 0)
            {
                //the + {x} means that it will zoom by {x} much after every mousewheel rub. Math.Min is used for setting a limit
                TileSize = Math.Min(TileSize + 4, 128);
            }
            else
                TileSize = Math.Max(TileSize - 4, 20);

            parentForm.Invalidate();
        }


        //action panels
        public void CloseActionPanels()
        {
            if (currentActorActionPanel != null)
            {
                parentForm.Controls.Remove(currentActorActionPanel);
                currentActorActionPanel.Dispose();
                currentActorActionPanel = null;
            }
        }

        public void OpenNewPlayerCharacterActionPanel(actors actor, Point location)
        {
            CloseActionPanels();

            Panel panel = new Panel
            {
                Size = new Size(200, 100),
                BackColor = Color.FromArgb(205, 127, 50),
                Location = location,
                Visible = true
            };

            currentActorActionPanel = panel;

            TextBox nameText = new TextBox { Text = $"{actor.Name}", Location = new Point(5, 5), Size = new Size(90, 30) };
            Point btnStartpoint = new Point(5, 20);
            foreach (var action in actor.Actions)
            {
                Button actBtn = new Button
                {
                    Text = action.Name,
                    Size = new Size(90, 30),
                    Location = btnStartpoint,
                };
                //actBtn.Click += (s, e) => action.Execute();
                btnStartpoint = new Point(btnStartpoint.X, btnStartpoint.Y + 10);
            }

            Button moveBtn = new Button { Text = "Move", Location = new Point(5, 20), Size = new Size(90, 30) };
            Button attackBtn = new Button { Text = "Attack", Location = new Point(5, 40), Size = new Size(90, 30) };

            currentActorActionPanel.Controls.Add(nameText);
            currentActorActionPanel.Controls.Add(moveBtn);
            currentActorActionPanel.Controls.Add(attackBtn);

            parentForm.Controls.Add(currentActorActionPanel);
        }

        public void OpenNewEvemyCharacterInfoPanel(actors actor, Point location)
        {
            CloseActionPanels();

            Panel panel = new Panel
            {
                Size = new Size(200, 100),
                BackColor = Color.FromArgb(205, 127, 50),
                Location = location,
                Visible = true
            };

            currentActorActionPanel = panel;

            TextBox nameText = new TextBox { Text = $"{actor.Name}", Location = new Point(5, 5), Size = new Size(90, 30) };

            currentActorActionPanel.Controls.Add(nameText);

            parentForm.Controls.Add(currentActorActionPanel);
        }
    }
}
