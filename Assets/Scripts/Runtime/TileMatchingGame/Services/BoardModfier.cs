using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.Services.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.TileMatchingGame.Services
{
    public class BoardModifier : IBoardModifier
    {
        private readonly IBoard _board;
        private readonly BoardFiller _boardFiller;
        private readonly TileViewPool _poolViewPool;

        public BoardModifier(IBoard board, BoardFiller boardFiller)
        {
            _board = board;
            _boardFiller = boardFiller;
        }

        public void RemoveTiles(List<Tile> tilesToRemove)
        {
            foreach (Tile tile in tilesToRemove)
            {
                if (tile != null)
                {
                    _board.RemoveTileAt(tile.Row, tile.Column, false);
                }
            }
        }

        public void UpdateTilesPosition()
        {
            for (int col = 0; col < _board.Width; col++)
            {
                for (int row = 0; row < _board.Height - 1; row++)
                {
                    if (_board.GetTileAt(row, col) == null)
                    {
                        for (int aboveRow = row + 1; aboveRow < _board.Height; aboveRow++)
                        {
                            Tile tileAbove = _board.GetTileAt(aboveRow, col);
                            if (tileAbove != null)
                            {
                                _board.SetTileAt(row, col, tileAbove);
                                _board.RemoveTileAt(aboveRow, col, true);
                                tileAbove.UpdatePosition(row, col);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void FillEmptySpaces()
        {
            _boardFiller.FillEmptySpaces();
        }
    }
}