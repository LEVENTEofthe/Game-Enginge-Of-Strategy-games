using System.Text.Json;

namespace CharacterCreator
{
    public partial class CharacterEditor : Form
    {
        string characterImageSource;
        public CharacterEditor()
        {
            InitializeComponent();
        }

        private void importPicBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image";
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            ofd.InitialDirectory = @"C:/Users/bakos/Documents/GEOS data library/assets/actor textures";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image tryImage = null;
                try
                {
                    tryImage = Image.FromFile(ofd.FileName);

                    if (tryImage.Size == new Size(8, 8) || tryImage.Size == new Size(16, 16) || tryImage.Size == new Size(32, 32) || tryImage.Size == new Size(64, 64) || tryImage.Size == new Size(128, 128))
                    {
                        characterImagePicbox.Image = tryImage;
                        characterImageSource = ofd.FileName;
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
            actors createdChara = new actors(nameTextbox.Text, characterImageSource, Convert.ToInt32(HPNumupdown.Value));

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(createdChara, options);
            File.WriteAllText(filePath, json);
        } 

        private void exportBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JSON files (*.json)|*.json";
            sfd.InitialDirectory = @"C:/Users/bakos/Documents/GEOS data library/database/actors";
            sfd.FileName = nameTextbox.Text;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                exportChar(sfd.FileName);
            }
        }
    }
}
