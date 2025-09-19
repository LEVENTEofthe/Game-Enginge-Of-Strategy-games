using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Windows.Forms;
using SRPG_library;
using SRPG_library.actors;

namespace CharacterCreator
{
    public partial class CharacterEditor : Form
    {
        string characterImageSource;
        List<IActorAction?> ActionList;
        Dictionary<string, object> Variables = new Dictionary<string, object>();
        IActorAI actorAI = null;
        public CharacterEditor()
        {
            InitializeComponent();

            LoadActionsToCheckbox();
        }

        private void importPicBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.Title = "Select an image";
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            ofd.InitialDirectory = @"C:/Users/bakos/Documents/GEOS data library/assets/actor textures";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image tryImage = null;
                try
                {
                    tryImage = Image.FromFile(ofd.FileName);

                    //if (tryImage.Size == new Size(8, 8) || tryImage.Size == new Size(16, 16) || tryImage.Size == new Size(32, 32) || tryImage.Size == new Size(64, 64) || tryImage.Size == new Size(128, 128))
                    {
                        characterImagePicbox.Image = tryImage;
                        characterImageSource = $"{ofd.InitialDirectory}/{ofd.SafeFileName}";
                    }
                    //else
                    //{
                    //    MessageBox.Show($"Invalid image size: {tryImage.Size}", "Invalid size", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    tryImage.Dispose();
                    //}

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tryImage?.Dispose();
                }
            }
        }

        private void exportChar(string filePath)
        {
            List<IActorAction> ImplementedActions = actorActions.CheckedItems.OfType<IActorAction>().ToList();

            Actor createdChara = new Actor(nameTextbox.Text, characterImageSource, Convert.ToInt32(TurnSpeed.Value), Convert.ToInt32(MoveNumupdown.Value), ImplementedActions, Variables, actorAI, -1, -1);

            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string json = JsonConvert.SerializeObject(createdChara, Formatting.Indented, settings);
            
            File.WriteAllText(filePath, json);
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "";
            sfd.Filter = "JSON files (*.json)|*.json";
            sfd.InitialDirectory = @"C:/Users/bakos/Documents/GEOS data library/database/actors";
            sfd.FileName = $"{nameTextbox.Text}.actor";
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                exportChar(sfd.FileName);
            }
        }

        private void LoadActionsToCheckbox()
        {
            Assembly assem = typeof(IActorAction).Assembly;

            ActionList = assem.GetTypes()
                .Where(t => typeof(IActorAction).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(t => (IActorAction)Activator.CreateInstance(t))
                .ToList();

            Debug.WriteLine("starting");
            foreach (var instance in ActionList)
            {
                Debug.WriteLine("First");
                Debug.WriteLine($"{instance.ID}");
                actorActions.Items.Add(instance, false);
            }

            actorActions.DisplayMember = "ID";
        }
    }
}
