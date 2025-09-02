using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library.events
{
    public class EventMethod
    {
        public string ID { get; }
        public Type InputType { get; }
        public Type OutputType { get; }
        public Func<Object?, Object?> Executor { get; }

        public EventMethod(string iD, Type inputType, Type outputType, Func<object?, object?> executor)
        {
            ID = iD;
            InputType = inputType;
            OutputType = outputType;
            Executor = executor;
        }
    }
    
    public class EventActionLibrary
    {
        private readonly Dictionary<string, EventMethod> EventMethods = new();

        public void Register(EventMethod eventMethod)
        {
            EventMethods[eventMethod.ID] = eventMethod;
        }

        public EventMethod? Get(string id) => EventMethods.TryGetValue(id, out var desc) ? desc : null;
    }
}
