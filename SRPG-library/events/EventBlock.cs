using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library.events
{
    public class EventBlock
    {
        public string ID { get; }
        public Type InputType { get; }
        public Type OutputType { get; }
        public Func<Object?, Object?> Executor { get; }

        public EventBlock(string ID, Type inputType, Type outputType, Func<object?, object?> executor)
        {
            this.ID = ID;
            InputType = inputType;
            OutputType = outputType;
            Executor = executor;
        }
    }
    
    public class EventBlockPool
    {
        private readonly Dictionary<string, EventBlock> EventBlocks = new();

        public void Register(EventBlock eventBlck)
        {
            EventBlocks[eventBlck.ID] = eventBlck;
        }

        public EventBlock? Get(string id) => EventBlocks.TryGetValue(id, out var desc) ? desc : null;
    }
}
