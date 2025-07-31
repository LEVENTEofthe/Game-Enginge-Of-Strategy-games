using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library.events
{
    public interface IEventUiManager    //Since some events need to use the UI manager, which shouldn't be put in the shared class library, we are using this as a bridge
    {
        public void ActorChooser(string jsonFolderPath, string imageFolderPath);
    }
}
