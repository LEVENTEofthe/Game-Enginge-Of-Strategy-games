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
        public IGameState CurrentTurn { get; set; }
        private List<IGameState> turnOrder;
        public int TurnNumber { get; set; }
        public ISingleAction SelectedAction {  get; set; }
        public Actor SelectedActor { get; set; }
        public List<Tile> SelectableTargetTiles {  get; set; }

        public Match(TileMap map)
        {
            Map = map;

            SelectableTargetTiles = new List<Tile>();
        }

        public List<IGameState> TurnOrder
        {
            get { return turnOrder; }
            set { turnOrder = value;
                CurrentTurn = turnOrder.FirstOrDefault();
                TurnNumber = 1;
            }
        }

        public void ExecuteSelectedAction(ISingleAction action, Actor actor)
        {
            SelectedAction = action;
            SelectedActor = actor;
            SelectableTargetTiles = SelectedAction.GetSelectableTiles(Map, SelectedActor);

        }

        public void TurnEnd()
        {
            TurnNumber = TurnNumber + 1;
            IGameState newSidesTurn = TurnOrder[TurnNumber % TurnOrder.Count];
            CurrentTurn = newSidesTurn;
        }
    }

    public enum ParticingSides { player, enemy }    //We'd want to make sure the users can initiate more than one player input side and more than one computer controlled side with differing AI
}                                                   //Guess I'd make it work that each ParticingSides type would implement different IGameState childs. So there would be different ParticingSides object for gamestates where you can move all your units once during your turn and for one where each of your units have their own turn where only they can move
                                                    //Another option would be deleting ParticingSides and feeding only Player and Enemy IGameStates to TurnOrder, because from a Player IGameState you can go to other IGameStates which will lead back to the original Player state, which you have the option to end and advance turn