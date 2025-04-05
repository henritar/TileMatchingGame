using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.Services.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.TileMatchingGame.Services
{
    public class DFSMatchFinder : IMatchFinder
    {

        private readonly IBoard _board;

        public DFSMatchFinder(IBoard board)
        {
            _board = board;
        }

        public List<Tile> FindMatches(Tile startTile)
        {
            var matchedTiles = new List<Tile>();
            var visitedTiles = new HashSet<Tile>();

            DFS(startTile, visitedTiles, matchedTiles);

            return matchedTiles;
        }

        private void DFS(Tile currentTile, HashSet<Tile> visitedTiles, List<Tile> matchedTiles)
        {
            visitedTiles.Add(currentTile);
            matchedTiles.Add(currentTile);

            foreach (Tile neighbor in GetNeighbors(currentTile))
            {
                if (!visitedTiles.Contains(neighbor) && neighbor.TileData.Color == currentTile.TileData.Color)
                {
                    DFS(neighbor, visitedTiles, matchedTiles);
                }
            }
        }

        private IEnumerable<Tile> GetNeighbors(Tile tile)
        {
            var neighbors = new List<Tile>();
            int[,] offsets = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

            for (int i = 0; i < offsets.GetLength(0); i++)
            {
                int newRow = tile.Row + offsets[i, 0];
                int newColumn = tile.Column + offsets[i, 1];

                if (newRow >= 0 && newRow < _board.Height &&
                    newColumn >= 0 && newColumn < _board.Width)
                {
                    Tile neighbor = _board.GetTileAt(newRow, newColumn);
                    if (neighbor != null)
                    {
                        neighbors.Add(neighbor);
                    }
                }
            }

            return neighbors;
        }

    }
}