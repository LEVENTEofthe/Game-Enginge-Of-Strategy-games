using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRPG_library.actors
{
    public interface ActorAI
    {
        string AIType { get; }
        Actor myself {  get; }

        void MyTurn(TileMap map);

    }

    public class EnemyAI : ActorAI
    {
        public string AIType => "Enemy";
        public Actor myself { get; set; }
        
        public EnemyAI(Actor actor)
        {
            myself = actor;
        }

        public void MyTurn(TileMap map)
        {
            Actor aggro = FindClosestActor(map, myself);

            if (aggro == null)
            {
                MoveAction moveAction = myself.ActionSet.OfType<MoveAction>().FirstOrDefault();
                if (moveAction != null)
                {
                    List<Tile> canStepTo = moveAction.GetSelectableTiles(map, myself);
                    Tile wantToMoveThere = canStepTo
                        .OrderBy(tile => Math.Abs(tile.Column - aggro.Column) + Math.Abs(tile.Row - aggro.Row))
                        .FirstOrDefault();

                    if (wantToMoveThere != null)
                        moveAction.Execute(myself, wantToMoveThere, map);
                    else
                        Debug.Write($"{myself.Name}: HEEELP I CAN'T MOOOVE");
                }
            }
            else
                Debug.Write($"{myself.Name}: Help I don't even know who to attack");
        }
        

        public Actor? FindClosestActor(TileMap map, Actor myself)
        {
            var visited = new bool[map.Columns, map.Rows];
            var queue = new Queue<(int x, int y, int dist)>();

            queue.Enqueue((myself.Column, myself.Row, 0));
            visited[myself.Column, myself.Row] = true;

            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            while (queue.Count > 0)
            {
                var (x, y, dist) = queue.Dequeue();

                if (map.MapObject[x, y].ActorStandsHere != null && dist > 0)
                    return map.MapObject[x, y].ActorStandsHere;

                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];

                    if (nx >= 0 && nx < map.Columns && ny >= 0 && ny < map.Rows && !visited[nx, ny])
                    {
                        visited[nx, ny] = true;
                        queue.Enqueue((nx, ny, dist + 1));
                    }
                }
            }

            return null;
        }
    }
}
