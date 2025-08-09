using Game_Enginge_Of_Strategy_games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library.events
{
    public abstract class EventCommandBlocks
    {
        public abstract string ID { get; }
        public abstract void Execute();
    }

    public class actorChooser : EventCommandBlocks
    {
        public override string ID => "ActorChooser";
        public actors.actors actor {  get; private set; }
        public string actorPool { get; private set; }

        public actorChooser(string actorPool)
        {
            this.actorPool = actorPool;
        }

        public override void Execute()
        {
            //We need to implement some way so it works with different pools of actors. My first thought is making it so actors can hold various tags and this method filters by tags
            actor = UIManager.ActorChooser(actorPool, "C://Users/bakos/Documents/GEOS data library/assets/actor textures");
        }
    }
}
