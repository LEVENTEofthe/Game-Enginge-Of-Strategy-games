using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library
{
    public abstract class Events
    {
        public abstract string Id { get; }
        public abstract string Description { get; }
        public abstract List<SingleAction> Commands { get; }
        //Maybe we might want to differentiate tile and other types of events?

        public abstract void Trigger();
    }



    public class playerChooserField : Events
    {
        public override string Id => "playerChooserField";
        public override string Description => "a tile event that lets the player choose an actor from a pool and deploy it to the field";
        public actorChooser actorChooser { get; private set; }
        public override List<SingleAction> Commands => new List<SingleAction>();

        public playerChooserField(string actorPool)
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
