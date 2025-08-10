using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tile_Map_Drawing.MenuRibbons
{
    public partial class Top_PropertiesUC : UserControl
    {
        public int mapColumns;
        public int mapRows;
        public EventHandler clickedInvalidateMapBt;

        public Top_PropertiesUC()
        {
            InitializeComponent();

            mapColumns = Convert.ToInt32(mapColumnsNumupdown.Value);
            mapRows = Convert.ToInt32(mapRowsNumupdown.Value);
        }

        public Top_PropertiesUC(int mapColumns, int mapRows)
        {
            InitializeComponent();
            this.mapColumns = mapColumns;
            this.mapRows = mapRows;
            mapColumnsNumupdown.Value = mapColumns;
            mapRowsNumupdown.Value = mapRows;
        }

        private void mapColumnsNumupdown_ValueChanged(object sender, EventArgs e)
        {
            mapColumns = Convert.ToInt32(mapColumnsNumupdown.Value);
        }

        private void mapRowsNumupdown_ValueChanged(object sender, EventArgs e)
        {
            mapRows = Convert.ToInt32(mapRowsNumupdown.Value);
        }

        private void invalidateMapBtn_Click(object sender, EventArgs e)
        {
            clickedInvalidateMapBt?.Invoke(this, EventArgs.Empty);
        }
    }
}