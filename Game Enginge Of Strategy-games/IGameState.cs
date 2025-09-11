using SRPG_library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    public interface IGameState
    {
        Form ParentForm { get; }
        Match match { get; }
        Dictionary<string, Func<Object>> Handlers { get; }  //I'm entirely not sure about this, but for the time being we gonna use the handlers like this

        void MouseDown(MouseEventArgs e);

        public Actor clickedOnPlayerCharacter(Point mousePosition, Match match)
        {
            if (CameraManager.ReturnTileUnderCursor(mousePosition, match.Map)?.ActorStandsHere != null)
            {
                return CameraManager.ReturnTileUnderCursor(mousePosition, match.Map)?.ActorStandsHere;
            }
            return null;
        }
    }
}
