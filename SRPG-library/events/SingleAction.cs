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
    public interface ISingleAction
    {
        string ID { get; }
        List<Tile> GetSelectableTiles(TileMap map, Actor user);
        void Execute(Actor User, object target, TileMap map);
    }

    public class MoveAction : ISingleAction
    {
        public string ID => "Move";
        public List<Tile> GetSelectableTiles(TileMap map, Actor user)
        {
            List<Tile> selectableTiles = new List<Tile>();

            Tile origin = map.MapObject[user.columnIndex, user.rowIndex];

            for (int c = -user.Movement; c <= user.Movement; c++)
            {
                for (int r = -user.Movement; r <= user.Movement; r++)
                {
                    if (Math.Abs(c) + Math.Abs(r) <= user.Movement)
                    {
                        if (origin.Column + c <= map.Columns && origin.Column + c > 0 && origin.Row + r <= map.Rows && origin.Row + r > 0)
                        {
                            selectableTiles.Add(map.MapObject[origin.columnIndex + c, origin.rowIndex + r]);
                        }
                    }
                }
            }
            return selectableTiles;
        }
        public void Execute(Actor User, object target, TileMap map)
        {
            Tile targetTile = target as Tile;
            
            if (targetTile != null && targetTile.CanStepHere())
            {
                Tile currentTile = map.MapObject[User.columnIndex, User.rowIndex];
                currentTile.ActorStandsHere = null;
                User.Column = targetTile.Column;
                User.Row = targetTile.Row;
                targetTile.ActorStandsHere = User;
            }
            else
            {
                Debug.WriteLine($"You tried to step on the tile {targetTile} which is already occupied");
            }
        }
    }


    /*
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
                actionContext.User.Column = actionContext.TargetTile.Column;
                actionContext.User.Row = actionContext.TargetTile.Row;
                actionContext.TargetTile.ActorStandsHere = actionContext.User;
            }
            else
            {
                Debug.WriteLine($"You tried to step on the tile {actionContext.TargetTile} which is already occupied");
            }
    
        }
    }
    */
}
