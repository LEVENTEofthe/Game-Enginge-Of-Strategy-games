using SRPG_library.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library
{
    public class GameEvent
    {
        public string ID { get; set; }
        public List<EventBlock_EditorInstance> Blocks { get; set; } = new();

        //public GameEvent(string ID,  List<EventBlock_EditorInstance> Blocks)
        //{
        //    this.ID = ID;
        //    this.Blocks = Blocks;
        //}

        public object? Execute(EventBlockPool pool, object user)
        {
            var results = new List<object?>();

            for (int i = 0; i < Blocks.Count; i++)
            {
                var block = Blocks[i];
                var method = pool.Get(block.BlockID)
                    ?? throw new InvalidOperationException($"Method {block.BlockID} not found.");

                //resolve arguments
                var paramets = new object?[block.Parameters.Count];
                for (int j = 0; j < block.Parameters.Count; j++)
                {
                    var binding = block.Parameters[j];
                    paramets[j] = binding.BindingType switch
                    {
                        ParameterBindingType.Constant => binding.ConstantValue,
                        ParameterBindingType.BlockResult => results[binding.SourceBlockIndex!.Value],
                        ParameterBindingType.UserContext => ResolveUserValue(user, binding.UserProperty!),
                        _ => throw new InvalidOperationException("Unknown argument source type")
                    };
                }

                var rslt = method.Executor(paramets);
                results.Add(rslt);
            }

            return results.LastOrDefault();
        }

        private object? ResolveUserValue(object user, string propertyName)
        {
            var prop = user.GetType().GetProperty(propertyName);
            if (prop == null)
                throw new InvalidOperationException($"Property {propertyName} not found on {user.GetType().Name}");
            return prop.GetValue(user);
        }
    }
}
