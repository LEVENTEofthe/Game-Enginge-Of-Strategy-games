using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library.events
{
    public abstract class Events
    {
        public abstract string Id { get; }
        public abstract string Description { get; }
        public abstract List<EventCommandBlocks> Commands { get; }

        public abstract void Trigger();
    }

    public class actorChooserField : Events
    {
        public override string Id => "actorChooserField";
        public override string Description => "an event that lets the player choose an actor from a pool and deploy it to the field";
        public actorChooser actorChooser { get; private set; }
        public override List<EventCommandBlocks> Commands => new List<EventCommandBlocks>();

        public actorChooserField(string actorPool)
        {
            actorChooser = new(actorPool);
            Commands.Add(actorChooser);
        }

        public override void Trigger()
        {
            throw new NotImplementedException();
        }
    }
}
