using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRPG_library;

namespace Game_Enginge_Of_Strategy_games
{
    public class match //one fight is called a match - because your game won't only contain one fight
    {
        public tileMap Map { get; set; }
        public actors[] PlayerTeam { get; set; }
        public actors[] EnemyTeam { get; set; }
        public match(tileMap map, actors[] playerTeam, actors[] enemyTeam)
        {
            Map = map;
            PlayerTeam = playerTeam;
            EnemyTeam = enemyTeam;
        }
        public match(tileMap map)
        {
            Map = map;
        }
    }
}
