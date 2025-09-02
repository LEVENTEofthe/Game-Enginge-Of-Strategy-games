using SRPG_library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    public class EventGraphicsManager
    {
        public ISingleAction Action { get; set; }
        public List<Tile> HighlightTiles { get; set; }
        public string Tooltiptext { get; set; }
        public object PanelData { get; set; }
        public bool ShowPanel { get; set; }
    }

    public static class EventGraphicsInvoker
    {
        public static event Action<EventGraphicsManager> OnActionGraphicsRequested;
        public static void RequestActionGraphics(EventGraphicsManager data)
        {
            OnActionGraphicsRequested?.Invoke(data);
        }
    }
}
