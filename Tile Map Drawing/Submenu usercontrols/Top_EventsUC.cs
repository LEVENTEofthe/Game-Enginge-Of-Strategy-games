using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tile_Map_Drawing
{
    public partial class Top_EventsUC : UserControl
    {
        private string tool;

        public Top_EventsUC()
        {
            InitializeComponent();
        }

        private void playerUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tool = "playerChooserField";
        }

        private void setDirectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tool = "playerDirect";
        }
    }
}
