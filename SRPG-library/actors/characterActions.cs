using SRPG_library.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library
{
    /*      //I think we doesn't even use this anymore
    public interface ICharacterActions  
    {
        public string Name { get; }
        public string Description { get; }

        public abstract void Execute(Actor user, ActionContext context);

        public bool canBeExecuted();

        public virtual bool IsValidTarget(Actor target)
        {
            // Default to allowing any target
            return true;
        }

        public void GetCost();
    }
    */

    /*
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
    */
}
