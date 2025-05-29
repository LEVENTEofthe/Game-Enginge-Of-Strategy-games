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
        private CameraManager cameraManager;
        private Panel currentActorActionPanel;

        public UIManager(Form Form)
        {
            parentForm = Form;
        }

        public void ClosePlayerCharacterActionPanel()
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
            ClosePlayerCharacterActionPanel();

            Panel panel = new Panel //I wonder if we could make a built in customizable panel creator. Let's not linger on it yet, but it sure would be nice in the future
            {
                Size = new Size(200, 100),
                BackColor = Color.FromArgb(205, 127, 50),
                Location = location,
                Visible = true
            };

            currentActorActionPanel = panel;

            TextBox nameText = new TextBox { Text = $"{actor.Name}", Location = new Point(5, 5), Size = new Size(90, 30) };
            currentActorActionPanel.Controls.Add(nameText);

            Point point = new(5, 30);
            foreach (var i in actor.ActionSet)
            {
                Button btn = new Button { Text = i.Name, Location = point, Size = new(90, 27)};
                point.Y += 25;
                currentActorActionPanel.Controls.Add(btn);
            }

            parentForm.Controls.Add(currentActorActionPanel);
        }

        public void highlightTile(tile selectedTile, Color color, Graphics g)
        {
            if (selectedTile == null)
                return;

            Rectangle highlight = new Rectangle(selectedTile.Column * cameraManager.TileSize, selectedTile.Row * cameraManager.TileSize, cameraManager.TileSize, cameraManager.TileSize);

            using (Brush brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, highlight);
            }
        }
    }
}
