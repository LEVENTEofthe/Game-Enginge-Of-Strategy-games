using GridbaseBattleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    public class ActionContext    //Here are all the things that a character action would need as parameters
    {
        public actors User { get; set; }
        public targetType TargetType { get; set; }
        private actors target;
        public actors Target
        {
            get { return target; }
            set
            {
                if (TargetType == targetType.none)
                {
                    Target = null;
                }
                if (TargetType == targetType.self)
                {
                    target = User;
                }
                if (TargetType == targetType.clickEnemy)
                {
                    target = value;
                }
            }
        }

        public Dictionary<string, object> Extras { get; set; } = new(); //this is for letting the user create custom stuff that I didn't think of or something
    }
}
