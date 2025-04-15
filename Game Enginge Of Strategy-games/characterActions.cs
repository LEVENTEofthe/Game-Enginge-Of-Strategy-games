using GridbaseBattleSystem;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    public abstract class characterActions  //for defining character actions
    {
        public abstract string Name { get; }
        public abstract void Execute(ActionContext context);
    }

    public class characterMovement : characterActions
    {
        public override string Name => "Move";
        public override void Execute(ActionContext context)
        {
            var user = context.User;
            user.MapPosition = (user.MapPosition.Item1 + 1, user.MapPosition.Item2);
        }
    }

    public class telekinesis : characterActions
    {
        public override string Name => "Telekinesis";
        public override void Execute(ActionContext context)
        {
            //var targetType = ActionContext
            var target = context.Target;
            target.MapPosition = (target.MapPosition.Item1 + 1, target.MapPosition.Item2);
        }
    }
}
