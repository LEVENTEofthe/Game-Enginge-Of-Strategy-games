using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRPG_library;

namespace Event_builder
{
    public abstract class EventCommandBlocks
    {
        public abstract string ID { get; }
        public abstract void Execute();
    }

    public class actorChooser : EventCommandBlocks
    {
        public override string ID => "ActorChooser";
        public Actors actor {  get; private set; }
        public string actorPool { get; private set; }

        public actorChooser(string actorPool)
        {
            this.actorPool = actorPool;

        }

        public override void Execute()
        {
            //We need to implement some way so it works with different pools of actors. My first thought is making it so actors can hold various tags and this method filters by tags

        }
    }
}
