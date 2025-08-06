using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library.events
{
    public interface IEvent
    {
        string Id { get; }
        string Description { get; }
        List<EventCommands> Commands { get; }

        void Trigger();
    }
}
