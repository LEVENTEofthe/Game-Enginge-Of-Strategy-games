using SRPG_library.events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SRPG_library
{
    public interface ISingleAction      //I am thinking of making it so that actor class object by default would only have the necessary class fields implemented to them, excluding action specific data like attack and move distance. My solution would be that actions that actually NEED those fields would bestow them on the objects that is using them. Like, every attack action would be bestowing the same AttackValue field to the actors, except those that would want to work by different logic, and those actors that has no attack actions, they don't need to implement any attack value field. The values of the bestowen fields would be set in the character creator.
    {
        string ID { get; }
        string Description {  get; }
        Dictionary<string, object> Variables { get; }
        Color SelectableTileColor { get; }

        List<Tile> GetSelectableTiles(TileMap map, Actor user);
        void Execute(Actor User, object target, TileMap map);

    }

    public class MoveAction : ISingleAction
    {
        public string ID => "Move";
        public string Description => "";
        public Dictionary<string, object> Variables => new Dictionary<string, object>();
        public Color SelectableTileColor => Color.FromArgb(110, Color.Purple);

        public List<Tile> GetSelectableTiles(TileMap map, Actor user)
        {
            List<Tile> selectableTiles = new List<Tile>();

            Tile origin = map.MapObject[user.columnIndex, user.rowIndex];

            for (int c = -(int)user.Variables.GetValueOrDefault("Move"); c <= (int)user.Variables.GetValueOrDefault("Move"); c++)
            {
                for (int r = -(int)user.Variables.GetValueOrDefault("Move"); r <= (int)user.Variables.GetValueOrDefault("Move"); r++)
                {
                    if (Math.Abs(c) + Math.Abs(r) <= (int)user.Variables.GetValueOrDefault("Move"))
                    {
                        if (origin.Column + c <= map.Columns && origin.Column + c > 0 && origin.Row + r <= map.Rows && origin.Row + r > 0)
                        {
                            selectableTiles.Add(map.MapObject[origin.columnIndex + c, origin.rowIndex + r]);
                        }
                    }
                }
            }
            return selectableTiles;
        }

        public void Execute(Actor User, object target, TileMap map)
        {
            Tile targetTile = target as Tile;

            if (targetTile != null && targetTile.CanStepHere())
            {
                Tile currentTile = map.MapObject[User.columnIndex, User.rowIndex];
                currentTile.ActorStandsHere = null;
                User.Column = targetTile.Column;
                User.Row = targetTile.Row;
                targetTile.ActorStandsHere = User;
            }
            else
            {
                Debug.WriteLine($"You tried to step on the tile {targetTile} which is already occupied");
            }
        }
    }

    public class AttackAction : ISingleAction
    {
        public string ID => "Attack";
        public string Description => "";
        public Dictionary<string, object> Variables => new Dictionary<string, object>();
        public int Strength { get => (int)Variables["Strength"]; set => Variables["Strength"] = value; }
        public int Range { get => (int)Variables["AttackRange"]; set => Variables["AttackRange"] = value; }
        public Color SelectableTileColor => Color.FromArgb(110, Color.Crimson);

        public List<Tile> GetSelectableTiles(TileMap map, Actor user)
        {
            List<Tile> selectableTiles = new List<Tile>();

            Tile origin = map.MapObject[user.columnIndex, user.rowIndex];

            for (int c = -Range; c <= Range; c++)
            {
                for (int r = -Range; r <= Range; r++)
                {
                    if (Math.Abs(c) + Math.Abs(r) <= Range)
                    {
                        if (origin.Column + c <= map.Columns && origin.Column + c > 0 && origin.Row + r <= map.Rows && origin.Row + r > 0)
                        {
                            if (map.MapObject[origin.columnIndex + c, origin.rowIndex + r].ActorStandsHere != null)
                                selectableTiles.Add(map.MapObject[origin.columnIndex + c, origin.rowIndex + r]);
                        }
                    }
                }
            }
            return selectableTiles;
        }

        public void Execute(Actor User, object target, TileMap map)
        {
            Tile targetTile = target as Tile;

            if (targetTile != null && targetTile.ActorStandsHere != null)
                if (targetTile.ActorStandsHere.Variables.ContainsKey("HP")) 
                {
                    int HP = (int)targetTile.ActorStandsHere.Variables.GetValueOrDefault("HP") - Strength;

                    targetTile.ActorStandsHere.Variables["HP"] = HP;
                } 
        }
    }

    public class HealAction : ISingleAction
    {
        public string ID => "Heal";
        public string Description => "";
        public Dictionary<string, object> Variables => new Dictionary<string, object>();
        public Color SelectableTileColor => Color.FromArgb(60, Color.LightGreen);

        public List<Tile> GetSelectableTiles(TileMap map, Actor user)
        {
            List<Tile> selectableTiles = new List<Tile>();

            Tile origin = map.MapObject[user.columnIndex, user.rowIndex];

            for (int c = -(int)user.Variables.GetValueOrDefault("Heal"); c <= user.AttackRange; c++)
            {
                for (int r = -user.AttackRange; r <= user.AttackRange; r++)
                {
                    if (Math.Abs(c) + Math.Abs(r) <= user.AttackRange)
                    {
                        if (origin.Column + c <= map.Columns && origin.Column + c > 0 && origin.Row + r <= map.Rows && origin.Row + r > 0)
                        {
                            if (map.MapObject[origin.columnIndex + c, origin.rowIndex + r].ActorStandsHere != null)
                                selectableTiles.Add(map.MapObject[origin.columnIndex + c, origin.rowIndex + r]);
                        }
                    }
                }
            }
            return selectableTiles;
        }

        public void Execute(Actor User, object target, TileMap map)
        {
            Tile targetTile = target as Tile;

            if (targetTile != null && targetTile.ActorStandsHere != null)
            {
                targetTile.ActorStandsHere.HP = targetTile.ActorStandsHere.HP + 4;
            }
        }
    }

    public class ThiefAttackAction : ISingleAction
    {
        public string ID => "Thief attack";
        public string Description => "";
        public Dictionary<string, object> Variables { get; } = new();
        public Actor ActorStolenFrom { get => (Actor)Variables["ActorChooser"]; set => Variables["ActorChooser"] = value; }
        public Color SelectableTileColor => Color.FromArgb(100, Color.Crimson);

        public ThiefAttackAction()
        {
            Variables["ActorChooser"] = new Actor();
        }

        public List<Tile> GetSelectableTiles(TileMap map, Actor user)
        {
            List<Tile> selectableTiles = new List<Tile>();

            Tile origin = map.MapObject[user.columnIndex, user.rowIndex];

            for (int c = -user.AttackRange; c <= user.AttackRange; c++)
            {
                for (int r = -user.AttackRange; r <= user.AttackRange; r++)
                {
                    if (Math.Abs(c) + Math.Abs(r) <= user.AttackRange)
                    {
                        if (origin.Column + c <= map.Columns && origin.Column + c > 0 && origin.Row + r <= map.Rows && origin.Row + r > 0)
                        {
                            if (map.MapObject[origin.columnIndex + c, origin.rowIndex + r].ActorStandsHere != null)
                                selectableTiles.Add(map.MapObject[origin.columnIndex + c, origin.rowIndex + r]);
                        }
                    }
                }
            }
            return selectableTiles;
        }

        public void Execute(Actor User, object target, TileMap map)
        {
            Tile targetTile = target as Tile;

            if (targetTile != null && targetTile.ActorStandsHere != null)
            {
                targetTile.ActorStandsHere.HP = targetTile.ActorStandsHere.HP - ActorStolenFrom.HP;
            }
        }
    }

    public class AttackDynamicAction : ISingleAction
    {
        public string ID => "DynamicAttack";
        public string Description => "";
        public int attackValue;
        public Dictionary<string, object> Variables => new Dictionary<string, object> { { "ActorChooser", attackValue } };
        public Color SelectableTileColor => Color.FromArgb(100, Color.Crimson);

        public List<Tile> GetSelectableTiles(TileMap map, Actor user)
        {
            List<Tile> selectableTiles = new List<Tile>();

            Tile origin = map.MapObject[user.columnIndex, user.rowIndex];

            for (int c = -user.AttackRange; c <= user.AttackRange; c++)
            {
                for (int r = -user.AttackRange; r <= user.AttackRange; r++)
                {
                    if (Math.Abs(c) + Math.Abs(r) <= user.AttackRange)
                    {
                        if (origin.Column + c <= map.Columns && origin.Column + c > 0 && origin.Row + r <= map.Rows && origin.Row + r > 0)
                        {
                            if (map.MapObject[origin.columnIndex + c, origin.rowIndex + r].ActorStandsHere != null)
                                selectableTiles.Add(map.MapObject[origin.columnIndex + c, origin.rowIndex + r]);
                        }
                    }
                }
            }
            return selectableTiles;
        }

        public void Execute(Actor User, object target, TileMap map)
        {
            Tile targetTile = target as Tile;

            if (targetTile != null && targetTile.ActorStandsHere != null)
            {
                targetTile.ActorStandsHere.HP = targetTile.ActorStandsHere.HP - attackValue;
            }
        }
    }

    public class AliceAttackAction : ISingleAction
    {
        public string ID => "Alice attack";
        public string Description => "Attack that deals super-effective damage to all Edmond units";
        public Dictionary<string, object> Variables => new Dictionary<string, object>();

        public Color SelectableTileColor => Color.FromArgb(100, Color.Crimson);

        public List<Tile> GetSelectableTiles(TileMap map, Actor user)
        {
            List<Tile> selectableTiles = new List<Tile>();

            Tile origin = map.MapObject[user.columnIndex, user.rowIndex];

            for (int c = -user.AttackRange; c <= user.AttackRange; c++)
            {
                for (int r = -user.AttackRange; r <= user.AttackRange; r++)
                {
                    if (Math.Abs(c) + Math.Abs(r) <= user.AttackRange)
                    {
                        if (origin.Column + c <= map.Columns && origin.Column + c > 0 && origin.Row + r <= map.Rows && origin.Row + r > 0)
                        {
                            if (map.MapObject[origin.columnIndex + c, origin.rowIndex + r].ActorStandsHere != null)
                                selectableTiles.Add(map.MapObject[origin.columnIndex + c, origin.rowIndex + r]);
                        }
                    }
                }
            }
            return selectableTiles;
        }

        public void Execute(Actor User, object target, TileMap map)
        {
            Tile targetTile = target as Tile;

            if (targetTile != null && targetTile.ActorStandsHere != null)
            {
                if(targetTile.ActorStandsHere.Variables.Contains("Edmond unit"))
                    targetTile.ActorStandsHere.HP = targetTile.ActorStandsHere.HP - 9999;
                else
                    targetTile.ActorStandsHere.HP = targetTile.ActorStandsHere.HP - 10;
            }
        }
    }

    public class EdmondDebufferAction : ISingleAction
    {
        public string ID => "Edmond Debuff attack";
        public string Description => "An attack that inflicts 'homeless' on the enemy";
        public Color SelectableTileColor => Color.FromArgb(100, Color.Crimson);

        public Dictionary<string, object> Variables => new Dictionary<string, object>();

        public void Execute(Actor User, object target, TileMap map)
        {
            throw new NotImplementedException();
        }

        public List<Tile> GetSelectableTiles(TileMap map, Actor user)
        {
            throw new NotImplementedException();
        }
    }
}
