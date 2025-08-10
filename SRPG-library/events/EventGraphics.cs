using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library
{
    public static class EventGraphics
    {
        private static Dictionary<string, Image> eventImages = new();

        public static void LoadImages(string folder)
        {
            eventImages.Clear();
            foreach (var file in Directory.GetFiles(folder, "*.png"))
            {
                string id = Path.GetFileNameWithoutExtension(file);
                eventImages[id] = Image.FromFile(file);
            }
        }

        public static Image GetImage(string EventID)
        {
            if (EventID != null && eventImages.TryGetValue(EventID, out var image))
                return image;

            return null;
        }
    }
}
