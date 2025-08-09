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
        private ToolContext toolContext;

        public Top_EventsUC(ToolContext toolContext)
        {
            InitializeComponent();

            this.toolContext = toolContext;
        }

        private void playerUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolContext.EventId = "playerChooserField";
        }

        private void setDirectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolContext.EventId = "setDirectPlayer";
        }
    }
}
