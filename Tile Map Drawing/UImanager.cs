using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SRPG_library;
using Label = System.Windows.Forms.Label;
using System.Xml.Linq;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using System.Diagnostics;

namespace Tile_Map_Drawing
{
    public static class UImanager   //For the map drawing
    {
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

            List<string> errorList = new List<string>();

            foreach (var file in files)
            {
                try
                {
                    string json = File.ReadAllText(file);

                    if (string.IsNullOrWhiteSpace(file))
                    {
                        errorList.Add(json);
                        continue;
                    }

                    var data = JsonSerializer.Deserialize<Actors>(json);
                    if (data != null)
                        actorJsonsList.Add(data);
                }
                catch (JsonException Exception)
                {
                    errorList.Add($"{file}: {Exception.Message}");
                    Debug.WriteLine($"JSON exception with the file '{file}': {Exception}");
                }
                catch (Exception Exception)
                {
                    errorList.Add($"{file}: {Exception.Message}");
                    Debug.WriteLine($"Exception with the file '{file}': {Exception}");
                }
            }

            if (errorList.Any())
            {
                string errors = string.Join("\n", errorList);
                MessageBox.Show(
                            $"There were errors with the following files:\n\n{errors}",
                            "File Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
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
