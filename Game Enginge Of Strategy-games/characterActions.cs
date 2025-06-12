using GridbaseBattleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    internal interface IcharacterActions
    {
        public string Name { get; }
        public string Description { get; }
        
        public abstract void Execute(actors user, actionContext context);

        public bool canBeExecuted();

        public virtual bool IsValidTarget(actors target)
        {
            // Default to allowing any target
            return true;
        }

        public void GetCost();
    }

    internal class characterMovement : characterActions
    {
        public override string Name => "Move";
        public override void Execute(actors user, actionContext context)
        {
            throw new NotImplementedException();
        }
    }

    internal class attack : characterActions
    {
        public override string Name => "atk";
        public override void Execute(actors user, actionContext context)
        {
            throw new NotImplementedException();
        }
    }
    internal class magic : characterActions
    {
        public override string Name => "Magic";
        public override void Execute(actors user, actionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
