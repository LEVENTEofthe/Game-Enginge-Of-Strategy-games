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

            Panel panel = new Panel
            {
                Size = new Size(200, 100),
                BackColor = Color.FromArgb(205, 127, 50),
                Location = location,
                Visible = true
            };

            currentActorActionPanel = panel;

            TextBox nameText = new TextBox { Text = $"{actor.Name}", Location = new Point(5, 5), Size = new Size(90, 30) };
            Button moveBtn = new Button { Text = "Move", Location = new Point(5, 20), Size = new Size(90, 30) };
            Button attackBtn = new Button { Text = "Attack", Location = new Point(5, 40), Size = new Size(90, 30) };

            currentActorActionPanel.Controls.Add(nameText);
            currentActorActionPanel.Controls.Add(moveBtn);
            currentActorActionPanel.Controls.Add(attackBtn);

            parentForm.Controls.Add(currentActorActionPanel);
        }
    }
}
