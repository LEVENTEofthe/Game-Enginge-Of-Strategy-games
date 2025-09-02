using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SRPG_library
{
    public class Events    //The plan is that we'll make this thing inside the class library that would know what each command ID need to do, but doesn't know how to do it. We'd give this thing to the GEOS form and it would fill the HOWs with the methods we'd like to use in the events from inside itself.
    {
        public List<ICommand> CommandSequence = new();
        public string Id { get; }
        public string Description { get; }
        public object Trigger(object initialInput = null)
        {
            object current = initialInput;
            foreach (var step in CommandSequence)
            {
                current = step.Execute(current);
            }
            return current;
        }
    }   

    public class ThiefAttackEvent : IEvents
    {
        public string Id => "thiefAttack";
        public string Description => "after choosing an attack target, a character selector will pop up. The damage dealt will be equal to the attack of the chosen character";
        public List<string> Commands => new List<string> { "Attack", "ActorChooser"};

        public void Trigger()
        {
            
        }
    }
}
