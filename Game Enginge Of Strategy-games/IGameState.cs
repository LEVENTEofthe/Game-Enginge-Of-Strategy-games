using SRPG_library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Game_Enginge_Of_Strategy_games
{
    public interface IGameState
    {
        Form ParentForm { get; }
        Match match { get; }
        Dictionary<string, Func<Object>> Handlers { get; }  //I'm entirely not sure about this, but for the time being we gonna use the handlers like this

        void OnTurnStart();
        void MouseDown(MouseEventArgs e);

        public Actor clickedOnPlayerCharacter(Point mousePosition, Match match)
        {
            if (CameraManager.ReturnTileUnderCursor(mousePosition, match.Map)?.ActorStandsHere != null)
            {
                return CameraManager.ReturnTileUnderCursor(mousePosition, match.Map)?.ActorStandsHere;
            }
            return null;
        }
    }


    public class MatchSetup : IGameState
    {
        public Form ParentForm => throw new NotImplementedException();
        public Match match => throw new NotImplementedException();

        public Dictionary<string, Func<object>> Handlers => throw new NotImplementedException();

        public void OnTurnStart() { }
        public void MouseDown(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class PlayerTurn_SelectingAction_AllUnitsOneTime : IGameState
    {
        public Form ParentForm { get; set; }
        public Match match { get; set; }
        public Dictionary<string, Func<object>> Handlers { get; set; }

        public PlayerTurn_SelectingAction_AllUnitsOneTime(Form parentForm, Match match, Dictionary<string, Func<object>> handlers)
        {
            Handlers = handlers;
            this.match = match;
            ParentForm = parentForm;
        }

        public void OnTurnStart() { }

        public void MouseDown(MouseEventArgs e)
        {
            IGameState gameState = this;
            Actor clickedActor = gameState.clickedOnPlayerCharacter(e.Location, match);

            if (clickedActor != null)
            {
                List<Button> buttons = new List<Button>();

                foreach (ISingleAction ActorAction in clickedActor.ActionSet)
                {
                    System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();


                    Button button = new Button { Name = ActorAction.ID, Text = ActorAction.ID, Size = new(90, 31), Font = new("Arial", 9) };
                    ToolTip1.SetToolTip(button, ActorAction.Description);

                    button.Click += (s, ev) =>

                    #region clicking a character action button
                    {
                        foreach (string VariableKey in ActorAction.Variables.Keys.ToList())
                        {
                            if (Handlers.TryGetValue(VariableKey, out var method))
                            {
                                ActorAction.Variables[VariableKey] = method();
                            }
                        }

                        match.ExecuteSelectedAction(ActorAction, clickedActor);

                        if (match.CurrentTurn is PlayerTurn_SelectingAction_AllUnitsOneTime)
                            match.TurnEnd();

                        UIManager.ClosePlayerCharacterActionPanel(ParentForm);
                    };
                    #endregion

                    buttons.Add(button);
                }

                UIManager.OpenNewPlayerCharacterActionPanel(ParentForm, clickedActor, e.Location, match.Map, buttons);
            }
        }

        public override string ToString()
        {
            return "Selecting Action";
        }
    }

    public class PlayerTurn_FinalizingAction : IGameState
    {
        public Form ParentForm { get; set; }
        public Match match { get; set; }
        public Dictionary<string, Func<object>> Handlers { get; set; }

        public PlayerTurn_FinalizingAction(Form parentForm, Match match, Dictionary<string, Func<object>> handlers)
        {
            ParentForm = parentForm;
            this.match = match;
            Handlers = handlers;
        }

        public void OnTurnStart() { }

        public void MouseDown(MouseEventArgs e)
        {
            if (match.SelectedAction != null) //Action execute phrase
            {
                Tile clickedTile = CameraManager.ReturnTileUnderCursor(e.Location, match.Map);

                if (match.SelectableTargetTiles.Contains(clickedTile))
                {
                    match.SelectedAction.Execute(match.SelectedActor, CameraManager.ReturnTileUnderCursor(e.Location, match.Map), match.Map);

                    match.SelectedAction = null;
                    match.SelectedActor = null;
                    match.SelectableTargetTiles.Clear();

                    UIManager.ClosePlayerCharacterActionPanel(ParentForm);

                    match.TurnEnd();
                }
            }
        }
        public override string ToString()
        {
            return "Finalizing Action";
        }
    }

    public class EnemyTurn_AllUnitsActOneTime : IGameState
    {
        public Form ParentForm { get; set; }
        public Match match { get; set; }
        public Dictionary<string, Func<object>> Handlers { get; set; }

        public EnemyTurn_AllUnitsActOneTime(Form parentForm, Match match, Dictionary<string, Func<object>> handlers)
        {
            Handlers = handlers;
            this.match = match;
            ParentForm = parentForm;
        }

        public void OnTurnStart() 
        { 
            foreach (Tile tile in match.Map.MapObject)
            {
                if (tile.ActorStandsHere != null)
                    if (tile.ActorStandsHere.Variables.Contains("CPUcontrolled"))
                    {

                    }
            }
        }

        public void MouseDown(MouseEventArgs e)
        {
            
        }
    }

    public class PlayerTurn_SelectingAction_OneUnitActsBySpeed : IGameState
    {
        public Form ParentForm => throw new NotImplementedException();

        public Match match => throw new NotImplementedException();

        public Dictionary<string, Func<object>> Handlers => throw new NotImplementedException();

        public void OnTurnStart() { }

        public void MouseDown(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }

    public class EnemyTurn_OneUnitActsBySpeed : IGameState
    {
        public Form ParentForm => throw new NotImplementedException();

        public Match match => throw new NotImplementedException();

        public Dictionary<string, Func<object>> Handlers => throw new NotImplementedException();

        public void OnTurnStart() { }

        public void MouseDown(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
