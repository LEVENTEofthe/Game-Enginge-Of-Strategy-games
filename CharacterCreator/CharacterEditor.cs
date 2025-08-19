using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json;
using System.Windows.Forms;
using SRPG_library;

namespace CharacterCreator
{
    public partial class CharacterEditor : Form
    {
        string characterImageSource;
        List<SingleAction?> EventList;
        List<SingleAction> ImplementedActions = new List<SingleAction>();
        public CharacterEditor()
        {
            InitializeComponent();

            Assembly asse = typeof(SingleAction).Assembly;

            EventList = asse.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(SingleAction)) && !t.IsAbstract)
                .Select(t => (SingleAction)Activator.CreateInstance(t))
                .ToList();

            Debug.WriteLine("SingleActions discovered: ");
            foreach (var i in EventList)
            {
                Debug.WriteLine(i.ID);
            }

            foreach (var i in EventList)
            {
                actorActions.Items.Add(i);
            }
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

                    if (tryImage.Size == new Size(8, 8) || tryImage.Size == new Size(16, 16) || tryImage.Size == new Size(32, 32) || tryImage.Size == new Size(64, 64) || tryImage.Size == new Size(128, 128))
                    {
                        characterImagePicbox.Image = tryImage;
                        characterImageSource = $"{ofd.InitialDirectory}/{ofd.SafeFileName}";
                    }
                    else
                    {
                        MessageBox.Show($"Invalid image size: {tryImage.Size}", "Invalid size", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tryImage.Dispose();
                    }

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
            Actors createdChara = new Actors(nameTextbox.Text, characterImageSource, Convert.ToInt32(HPNumupdown.Value), -1, -1, ImplementedActions);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(createdChara, options);
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

        private void actorActions_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }
    }
}
