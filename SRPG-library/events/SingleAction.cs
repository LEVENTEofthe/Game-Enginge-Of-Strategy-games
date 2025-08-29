using SRPG_library.events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SRPG_library
{
    public static class SingleAction  //The single blocks that when put together will form complex events
    {
        public static void Execute(string ActionID, ActionContext actionContext)
        {
            switch (ActionID)
            {
                case "Move":
                    Move(actionContext);
                    break;
            }
        }

        public static void Move(ActionContext actionContext)
        {
            Tile currentTile = actionContext.Map.MapObject[actionContext.User.columnIndex, actionContext.User.rowIndex];
            if (actionContext.TargetTile != null && actionContext.TargetTile.CanStepHere())
            {
                currentTile.ActorStandsHere = null;
                Debug.WriteLine($"The actor on {currentTile} has already vanished");
                actionContext.User.Column = actionContext.TargetTile.Column;
                actionContext.User.Row = actionContext.TargetTile.Row;
                actionContext.TargetTile.ActorStandsHere = actionContext.User;
                Debug.WriteLine($"{actionContext.User} has appeared on the tile {actionContext.TargetTile}");
            }
            else
            {
                Debug.WriteLine($"You tried to step on the tile {actionContext.TargetTile} which is already occupied");
            }
        }
    }
}
