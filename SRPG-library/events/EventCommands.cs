using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library.events
{
    public abstract class EventCommands
    {
        public abstract string Type { get; }
        public abstract void Execute();
    }

    public class deployActor : EventCommands
    {
        public override string Type => "DeployActor";
        public readonly IEventUiManager uiManager;
        public override void Execute()
        {
            uiManager.ActorChooser("C://Users/bakos/Documents/GEOS data library/database/actors", "C://Users/bakos/Documents/GEOS data library/assets/actor textures");
        }

        public deployActor(IEventUiManager uiManager)
        {
            this.uiManager = uiManager ?? throw new ArgumentNullException(nameof(uiManager));
        }
    }
}
