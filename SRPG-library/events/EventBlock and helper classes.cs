using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library.events
{
    public class EventBlock     //Its instances are the useable types of Event blocks
    {
        public string ID { get; }
        public IReadOnlyList<EventBlockParameters> Parameters { get; }
        public Type OutputType { get; }
        public Func<Object?[], Object?> Executor { get; }

        public EventBlock(string ID, IReadOnlyList<EventBlockParameters> parameters, Type outputType, Func<object?[], object?> executor)
        {
            this.ID = ID;
            Parameters = parameters;
            OutputType = outputType;
            Executor = executor;
        }
    }
    
    public class EventBlock_EditorInstance     //Its instances are the building blocks of a single particular Event
    {
        public string BlockID { get; set; } = "";
        public List<ParameterBinding> Parameters { get; set; } = new();

        //public EventBlock_EditorInstance(string BlockID, List<ParameterBinding> Parameters)
        //{
        //    this.BlockID = BlockID;
        //    this.Parameters = Parameters;
        //}
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


    public class EventBlockParameters   //The event blocks need to have parameters with ranging number of parameters. This class can initiate them.
    {
        public string Name { get; }
        public Type InputType { get; }

        public EventBlockParameters(string name, Type inputType)
        {
            Name = name;
            InputType = inputType;
        }
    }

    public enum ParameterBindingType { Constant, BlockResult, UserContext }
    public class ParameterBinding    //The parameters of EventBlocks can either come from other event blocks or from static values. This class helps with managing that
    {
        public ParameterBindingType BindingType { get; set; }
        public object? ConstantValue { get; set; }
        public int? SourceBlockIndex { get; set; }  //When the parameter is not a constant value, the parameter will be the return value of the block with the index that equals this value
        public string? UserProperty {  get; set; }
    }
}
