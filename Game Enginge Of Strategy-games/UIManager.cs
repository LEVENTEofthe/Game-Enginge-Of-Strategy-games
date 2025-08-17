using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SRPG_library;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using System.Text.Json;

namespace Game_Enginge_Of_Strategy_games
{
    public class UIManager  //For the game system
    {
        private static Panel currentActorActionPanel;

        public static void ClosePlayerCharacterActionPanel(Control parentForm)
        {
            if (currentActorActionPanel != null)
            {
                parentForm.Controls.Remove(currentActorActionPanel);
                currentActorActionPanel.Dispose();
                currentActorActionPanel = null;
            }
        }

        public static void OpenNewPlayerCharacterActionPanel(Control parentForm, Actors actor, Point location)
        {
            ClosePlayerCharacterActionPanel(parentForm);

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
            //foreach (var i in actor.ActionSet)
            //{
            //    Button btn = new Button { Text = i.Name, Location = point, Size = new(90, 27)};
            //    point.Y += 25;
            //    currentActorActionPanel.Controls.Add(btn);
            //}

            parentForm.Controls.Add(currentActorActionPanel);
            
        }

        public static void highlightTile(Tile selectedTile, Color color, Graphics g, int TileSize)
        {
            if (selectedTile == null)
                return;

            Rectangle highlight = new Rectangle(selectedTile.Column * TileSize, selectedTile.Row * TileSize, TileSize, TileSize);

            using (Brush brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, highlight);
            }
        }

        #region Actor chooser
        public static Actors ActorChooser(string jsonFolderPath, string imageFolderPath)   //We should make it so it can't only deploy all actors, but can handle different pools of actors
        {
            int index = -1;
            var form = new Form
            {
                Text = "Actor Chooser",
                Width = 800,
                Height = 600
            };

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            var actorObjects = LoadActorChooserData(jsonFolderPath);
            foreach (var Object in actorObjects)
            {
                var panel = CreateActorCard(imageFolderPath, Object);

                EventHandler panelClick = (s, e) =>
                {
                    index = actorObjects.IndexOf(Object);
                    form.DialogResult = DialogResult.OK;
                    form.Close();
                };

                panel.Click += panelClick;                  //adding the click event to the panel itself
                foreach (Control ctrl in panel.Controls)    //adding the click event to all items on the panel
                {
                    ctrl.Click += panelClick;
                }

                flow.Controls.Add(panel);
            }
            form.Controls.Add(flow);

            var result = form.ShowDialog();

            if (result == DialogResult.OK && index >= 0)
            {
                return actorObjects[index];
            }
            return null;
        }

        public static List<Actors> LoadActorChooserData(string jsonFolderPath)
        {
            var actorJsonsList = new List<Actors>();
            var files = Directory.GetFiles(jsonFolderPath, "*.json");

            foreach (var file in files)
            {
                try
                {
                    string json = File.ReadAllText(file);
                    var data = JsonSerializer.Deserialize<Actors>(json);
                    if (data != null)
                        actorJsonsList.Add(data);
                }
                catch (Exception)
                {
                    //Still need to handle expections
                    throw;
                }
            }

            return actorJsonsList;
        }

        public static Panel CreateActorCard(string imageFolderPath, Actors actorObject)
        {
            var panel = new Panel
            {
                Width = 120,
                Height = 150,
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };

            var image = new PictureBox
            {
                Width = 100,
                Height = 100,
                SizeMode = PictureBoxSizeMode.Zoom,
                ImageLocation = Path.Combine(imageFolderPath, actorObject.Image),
                Left = 10,
                Top = 10
            };

            var label = new Label
            {
                Text = actorObject.Name,
                AutoSize = false,
                Width = 100,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Left = 10,
                Top = 115,
            };

            panel.Controls.Add(image);
            panel.Controls.Add(label);
            
            panel.Tag = actorObject;

            return panel;
        }
        #endregion
    }
}
