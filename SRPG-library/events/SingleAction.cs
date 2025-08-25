using SRPG_library.events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SRPG_library
{
    public abstract class SingleAction  //The single blocks that when put together will form complex events
    {
        public abstract string ID { get; }
        [JsonIgnore]
        public ActionContext ActionContext { get; }
        public abstract void Execute(ActionContext actionContext);

        public override string ToString()
        {
            return ID;
        }

        [JsonConstructorAttribute]
        public SingleAction() { }
    }

    public class ActorMove : SingleAction
    {
        public override string ID => "ActorMove";

        public ActionContext ActionContext { get; set;}


        public override void Execute(ActionContext actionContext)
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

        //public override void Execute(ActionContext actionContext)      //I wonder if I can make this ActionContext stuff more readable somehow
        //{ 
        //    if (actionContext.Map.MapObject[actionContext.User.Column + MoveInCol - 1, actionContext.User.Row + MoveInRow - 1] != null && actionContext.Map.MapObject[actionContext.User.Column + MoveInCol - 1, actionContext.User.Row + MoveInRow - 1].CanStepHere())
        //    {
        //        actionContext.Map.MapObject[actionContext.User.Column - 1, actionContext.User.Row - 1].ActorStandsHere = null;
        //        Debug.WriteLine($"Az actor a {actionContext.Map.MapObject[actionContext.User.Column - 1, actionContext.User.Row - 1]} csempén megszűntette magát");
        //        actionContext.User.Column += MoveInCol;
        //        actionContext.User.Row += MoveInRow;
        //        actionContext.Map.MapObject[actionContext.User.Column - 1, actionContext.User.Row - 1].ActorStandsHere = actionContext.User;
        //        Debug.WriteLine($"Az actor létrehozta magát a {actionContext.Map.MapObject[actionContext.User.Column - 1, actionContext.User.Row - 1]} csempén");
        //    }
        //    else
        //    {
        //        Debug.WriteLine($"You tried to step with {actionContext.User.Name} to the tile ({actionContext.User.Column + MoveInCol}, {actionContext.User.Row + MoveInRow}), even though the map's dimension is ({actionContext.Map.Columns}, {actionContext.Map.Rows}) and {actionContext.Map.MapObject[actionContext.User.Column + MoveInCol, actionContext.User.Row + MoveInRow].ActorStandsHere?.Name} is already standing there");
        //        return;
        //    }

        //    if (actionContext.User.Column > actionContext.Map.Columns)  //These are probably already outdated
        //        actionContext.User.Column = actionContext.Map.Columns;
        //    if (actionContext.User.Row > actionContext.Map.Rows)
        //        actionContext.User.Row = actionContext.Map.Rows;
        //}


    }

    public class actorChooser : SingleAction
    {
        public override string ID => "ActorChooser";
        public Actors Actor {  get; private set; }
        public string ActorPool { get; private set; }


        public override void Execute(ActionContext actionContext)
        {
            //We need to implement some way so it works with different pools of actors. My first thought is making it so actors can hold various tags and this method filters by tags
            return;
        }
    }
}
