using GridbaseBattleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    class match //one fight is called a match - because your game won't only contain one fight
    {

        public match(gridMap map, actors[] playerTeam, actors[] enemyTeam)
        {
            this.Map = map;
            this.PlayerTeam = playerTeam;
            this.EnemyTeam = enemyTeam;
        }

        public gridMap Map { get; set; }

        public actors[] PlayerTeam { get; set; }

        public actors[] EnemyTeam { get; set; }
    }
}
