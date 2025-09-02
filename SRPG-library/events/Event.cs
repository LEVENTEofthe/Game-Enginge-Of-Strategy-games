using SRPG_library.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library
{
    public class ActionEvent    //The plan is that we'll make this thing inside the class library that would know what each command ID need to do, but doesn't know how to do it. We'd give this thing to the GEOS form and it would fill the HOWs with the methods we'd like to use in the events from inside itself.
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public List<string> Commands { get; set; }  //We are only storing the IDs of each command, because I would like to include UI function as well, and you can't put them into the class library.

        public object? Execute(EventActionLibrary actions, object? initialInput = null)
        {
            object? current = initialInput;

            foreach (var commandID in Commands)
            {
                var method = actions.Get(commandID) ??
                    throw new InvalidOperationException($"The action {commandID} is not found");

                if (current != null && !method.InputType.IsAssignableFrom(current.GetType()))
                    throw new InvalidOperationException($"Type mismatch: {commandID} expected {method.InputType} but got {current.GetType()}");

                current = method.Executor(current);
            }

            return current;
        }
    }   
}
