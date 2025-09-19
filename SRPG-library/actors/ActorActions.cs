using SRPG_library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SRPG_library.actors
{
    public interface IActorAction      //I am thinking of making it so that actor class object by default would only have the necessary class fields implemented to them, excluding action specific data like attack and move distance. My solution would be that actions that actually NEED those fields would bestow them on the objects that is using them. Like, every attack action would be bestowing the same AttackValue field to the actors, except those that would want to work by different logic, and those actors that has no attack actions, they don't need to implement any attack value field. The values of the bestowen fields would be set in the character creator.
    {                                   //Right now, I have that ISingleAction children don't hold the variables they use and alter in their logic, since those variables belong to the Actor object. The dictionary the ISingleAction children hold only implement what variables the Actor holding them needs to implement along with the exact type. I currently have variables like Attack, which needs both an attack value and a range value to work, are an (int, int) touple type.
        string ID { get; }
        string Description {  get; }
        Dictionary<string, Type> VariablesToImplement { get; }
        Color SelectableTileColor { get; }

        List<Tile> GetSelectableTiles(TileMap map, Actor user);
        void Execute(Actor user, object target, TileMap map);

    }

    public class MoveAction : IActorAction
    {
        public string ID => "Move";
        public string Description => "";
        public Dictionary<string, Type> VariablesToImplement => new Dictionary<string, Type> { { "Move", typeof(int) } };
        public Color SelectableTileColor => Color.FromArgb(110, Color.Purple);


        public List<Tile> GetSelectableTiles(TileMap map, Actor user)
        {
            List<Tile> selectableTiles = new List<Tile>();

            Tile origin = map.MapObject[user.columnIndex, user.rowIndex];
            int MoveRange = (int)user.Variables.GetValueOrDefault("Move");

            for (int c = -MoveRange; c <= MoveRange; c++)
            {
                for (int r = -MoveRange; r <= MoveRange; r++)
                {
                    if (Math.Abs(c) + Math.Abs(r) <= MoveRange)
                    {
                        if (origin.Column + c <= map.Columns && origin.Column + c > 0 && 
                            origin.Row + r <= map.Rows && origin.Row + r > 0 && 
                            map.MapObject[origin.columnIndex + c, origin.rowIndex + r].CanStepHere())
                        {
                            selectableTiles.Add(map.MapObject[origin.columnIndex + c, origin.rowIndex + r]);
                        }
                    }
                }
            }
            return selectableTiles;
        }

        public void Execute(Actor user, object target, TileMap map)
        {
            Tile targetTile = target as Tile;

            if (targetTile != null && targetTile.CanStepHere())
            {
                Tile currentTile = map.MapObject[user.columnIndex, user.rowIndex];
                currentTile.ActorStandsHere = null;
                user.Column = targetTile.Column;
                user.Row = targetTile.Row;
                targetTile.ActorStandsHere = user;
            }
            else
            {
                Debug.WriteLine($"You tried to step on the tile {targetTile} which is already occupied");
            }
        }
    }

    public class AttackAction : IActorAction
    {
        public string ID => "Attack";
        public string Description => "";
        public Dictionary<string, Type> VariablesToImplement => new Dictionary<string, Type> { { "Strength", typeof(int) }, { "AttackRange", typeof(int) } };
        public Color SelectableTileColor => Color.FromArgb(110, Color.Crimson);


        public List<Tile> GetSelectableTiles(TileMap map, Actor user)
        {
            List<Tile> selectableTiles = new List<Tile>();

            Tile origin = map.MapObject[user.columnIndex, user.rowIndex];
            int AttackRange = (int)user.Variables.GetValueOrDefault("AttackRange");

            for (int c = -AttackRange; c <= AttackRange; c++)
            {
                for (int r = -AttackRange; r <= AttackRange; r++)
                {
                    if (Math.Abs(c) + Math.Abs(r) <= AttackRange)
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

        public void Execute(Actor user, object target, TileMap map)
        {
            Tile targetTile = target as Tile;
            int Strength = (int)user.Variables.GetValueOrDefault("Strength");

            if (targetTile != null && targetTile.ActorStandsHere != null)
                targetTile.ActorStandsHere.HP -= Strength;
        }
    }

    public class HealAction : IActorAction
    {
        public string ID => "Heal";
        public string Description => "";
        public Dictionary<string, Type> VariablesToImplement => new Dictionary<string, Type> { { "Heal", typeof(int) }, { "HealRange", typeof(int)} };
        public Color SelectableTileColor => Color.FromArgb(60, Color.LightGreen);


        public List<Tile> GetSelectableTiles(TileMap map, Actor user)
        {
            List<Tile> selectableTiles = new List<Tile>();

            Tile origin = map.MapObject[user.columnIndex, user.rowIndex];
            int HealRange = (int)user.Variables.GetValueOrDefault("HealRange");

            for (int c = -HealRange; c <= HealRange; c++)
            {
                for (int r = -HealRange; r <= HealRange; r++)
                {
                    if (Math.Abs(c) + Math.Abs(r) <= HealRange)
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

        public void Execute(Actor user, object target, TileMap map)
        {
            Tile targetTile = target as Tile;
            int Heal = (int)user.Variables.GetValueOrDefault("Heal");

            if (targetTile != null && targetTile.ActorStandsHere != null)
            {
                targetTile.ActorStandsHere.HP += Heal;
            }
        }
    }
    public class ThiefAttackAction : IActorAction
    {
        public string ID => "Thief attack";
        public string Description => "Steals an other character's strength to attack";
        public Dictionary<string, Type> VariablesToImplement => new Dictionary<string, Type> { { "AttackRange", typeof(int) }, { "ActorChooser", typeof(Actor) } };
        public Color SelectableTileColor => Color.FromArgb(100, Color.Crimson);


        public List<Tile> GetSelectableTiles(TileMap map, Actor user)
        {
            List<Tile> selectableTiles = new List<Tile>();

            Tile origin = map.MapObject[user.columnIndex, user.rowIndex];
            int AttackRange = (int)user.Variables.GetValueOrDefault("AttackRange");

            for (int c = -AttackRange; c <= AttackRange; c++)
            {
                for (int r = -AttackRange; r <= AttackRange; r++)
                {
                    if (Math.Abs(c) + Math.Abs(r) <= AttackRange)
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

        public void Execute(Actor user, object target, TileMap map)
        {
            Tile targetTile = target as Tile;
            Actor actorStolenFrom = (Actor)user.Variables.GetValueOrDefault("ActorChooser");

            if (targetTile != null && targetTile.ActorStandsHere != null)
            {
                targetTile.ActorStandsHere.HP -= actorStolenFrom.HP;    //Once I implement filtering to ActorChooser, I'll make it filter to Actors who implemented the Strength variable and it will steal that value
            }
        }
    }

    /*
    public class AttackDynamicAction : ISingleAction
    {
        public string ID => "DynamicAttack";
        public string Description => "";
        public int attackValue;
        public Dictionary<string, object> VariablesToImplement => new Dictionary<string, object> { { "ActorChooser", attackValue } };
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
    */

    public class AliceAttackAction : IActorAction
    {
        public string ID => "Alice attack";
        public string Description => "Attack that deals super-effective damage to all Edmond units";
        public Dictionary<string, Type> VariablesToImplement => new Dictionary<string, Type> { { "Strength", typeof(int) }, { "AttackRange", typeof(int) }, { "SuperEffectiveMultiplier", typeof(float) } };
        public Color SelectableTileColor => Color.FromArgb(100, Color.Crimson);


        public List<Tile> GetSelectableTiles(TileMap map, Actor user)
        {
            List<Tile> selectableTiles = new List<Tile>();

            Tile origin = map.MapObject[user.columnIndex, user.rowIndex];
            int AttackRange = (int)user.Variables.GetValueOrDefault("AttackRange");

            for (int c = -AttackRange; c <= AttackRange; c++)
            {
                for (int r = -AttackRange; r <= AttackRange; r++)
                {
                    if (Math.Abs(c) + Math.Abs(r) <= AttackRange)
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

        public void Execute(Actor user, object target, TileMap map)
        {
            Tile targetTile = target as Tile;
            int Strength = (int)user.Variables.GetValueOrDefault("Strength");
            float SuperEffectiveMultiplier = (float)user.Variables.GetValueOrDefault("SuperEffectiveMultiplier");

            if (targetTile != null && targetTile.ActorStandsHere != null)
            {
                if (targetTile.ActorStandsHere.Variables.ContainsKey("EdmondUnit"))
                    targetTile.ActorStandsHere.HP -= Convert.ToInt32(Strength * SuperEffectiveMultiplier);
                else
                    targetTile.ActorStandsHere.HP -= Strength;
            }
        }
    }

    /*
    public class EdmondDebufferAction : ISingleAction
    {
        public string ID => "Edmond Debuff attack";
        public string Description => "An attack that inflicts 'homeless' on the enemy";
        public Color SelectableTileColor => Color.FromArgb(100, Color.Crimson);
        public Dictionary<string, Type> VariablesToImplement => new Dictionary<string, Type>() { };


        public void Execute(Actor User, object target, TileMap map)
        {
            throw new NotImplementedException();
        }

        public List<Tile> GetSelectableTiles(TileMap map, Actor user)
        {
            throw new NotImplementedException();
        }
    }
    */
}