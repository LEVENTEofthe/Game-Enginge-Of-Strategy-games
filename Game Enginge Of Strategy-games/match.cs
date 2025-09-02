using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRPG_library;

namespace Game_Enginge_Of_Strategy_games
{
    public class Match  //one fight is called a match - because your game won't only contain one fight
                        //But again, looking at it, this is useless right now because Player/Enemy teams seems to be outdated concepts and the Match class doesn't contribute anything else to the TileMap class. Another argument: A game contains many matches, but a match only contains one map, hence you could just say your game contains many maps, unless the Match class were to contribute someting.
    {
        public TileMap Map { get; set; }
        public Actor[] PlayerTeam { get; set; }
        //public List<SingleAction> ExecutableActions { get; set; }
        public ActionEvent SelectedAction {  get; set; }
        public Actor SelectedActor { get; set; }
        public List<Tile> SelectableTargetTiles {  get; set; }

        public Match(TileMap map)
        {
            Map = map;

            SelectableTargetTiles = new List<Tile>();

            //For now  I'll just try loading in all possible actions statically. Would be better if we didn't have to come here every time to put them here when we create a new SingleAction
            //ActorMove actorMovement = new ActorMove();
            //ExecutableActions = new List<SingleAction>
            //{
            //    actorMovement
            //};
        }

        public void ExecuteSelectedAction(ActionEvent action, Actor actor)
        {
            SelectedAction = action;
            SelectedActor = actor;
            //SelectableTargetTiles = SelectedAction.GetSelectableTiles(Map, SelectedActor);
        }
    }
}
