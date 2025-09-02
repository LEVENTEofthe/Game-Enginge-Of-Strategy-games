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
    public interface ISingleAction
    {
        string ID { get; }
        Color SelectableTileColor { get; }
        List<Tile> GetSelectableTiles(TileMap map, Actor user);
        
        void Execute(Actor User, object target, TileMap map);
        
    }

    public class MoveAction : ISingleAction
    {
        public string ID => "Move";
        public Color SelectableTileColor => Color.FromArgb(60, Color.Purple);

        public List<Tile> GetSelectableTiles(TileMap map, Actor user)
        {
            List<Tile> selectableTiles = new List<Tile>();

            Tile origin = map.MapObject[user.columnIndex, user.rowIndex];

            for (int c = -user.Movement; c <= user.Movement; c++)
            {
                for (int r = -user.Movement; r <= user.Movement; r++)
                {
                    if (Math.Abs(c) + Math.Abs(r) <= user.Movement)
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
                targetTile.ActorStandsHere.HP = targetTile.ActorStandsHere.HP - 10;
            }
        }
    }

    public class HealAction : ISingleAction
    {
        public string ID => "Heal";

        public Color SelectableTileColor => Color.FromArgb(60, Color.LightGreen);

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
                targetTile.ActorStandsHere.HP = targetTile.ActorStandsHere.HP + 4;
            }
        }
    }

    public class ThiefAttackAction : ISingleAction
    {
        public string ID => "Thief attack";

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
            

            throw new NotImplementedException();
        }
    }
}
