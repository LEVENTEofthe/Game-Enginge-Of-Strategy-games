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
        public (ParticingSides, int) CurrentTurn { get; set; }
        public List<ParticingSides> TurnOrder { get; set; }
        public ISingleAction SelectedAction {  get; set; }
        public Actor SelectedActor { get; set; }
        public List<Tile> SelectableTargetTiles {  get; set; }

        public Match(TileMap map, List<ParticingSides> turnOrder)
        {
            Map = map;
            TurnOrder = turnOrder;

            SelectableTargetTiles = new List<Tile>();
        }

        public void ExecuteSelectedAction(ISingleAction action, Actor actor)
        {
            SelectedAction = action;
            SelectedActor = actor;
            SelectableTargetTiles = SelectedAction.GetSelectableTiles(Map, SelectedActor);

        }

        public void TurnEnd()
        {
            int newTurnNumber = CurrentTurn.Item2 + 1;
            ParticingSides newSidesTurn = TurnOrder[newTurnNumber % TurnOrder.Count];
            CurrentTurn = (newSidesTurn, newTurnNumber);
        }
    }

    public enum ParticingSides { player, enemy }    //We'd want to make sure the users can initiate more than one player input side and more than one computer controlled side with differing AI
}
