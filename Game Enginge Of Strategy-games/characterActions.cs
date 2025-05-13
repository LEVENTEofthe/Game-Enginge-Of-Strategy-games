using GridbaseBattleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    internal abstract class characterActions
    {
        public abstract string Name { get; }

        public abstract void Execute(actors user, actionContext context);
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
