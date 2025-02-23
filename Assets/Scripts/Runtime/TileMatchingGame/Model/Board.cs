using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using System;

namespace Assets.Scripts.Runtime.TileMatchingGame.Model
{
    public class Board : IBoard
    {
        private Tile[,] _tiles;
        private int _width;
        private int _height;

        public int Width { get => _width; } 
        public int Height { get => _height; }

        public event Action OnBoardUpdated;
        public event Action<Tile> OnTileRemoved;
        public event Action<Tile> OnTileFalling;

        public Board(int width, int height)
        {
            _width = width;
            _height = height;
            _tiles = new Tile[height, width];
        }

        public Tile GetTileAt(int row, int column)
        {
            if (row >= 0 && row < Height && column >= 0 && column < Width)
            {
                return _tiles[row, column]; 
            }
            return null;
        }

        public void SetTileAt(int row, int column, Tile newTile)
        {
            if (row >= 0 && row < Height && column >= 0 && column < Width)
            {
                _tiles[row, column] = newTile;
                OnBoardUpdated?.Invoke(); 
            }
        }
        public void RemoveTileAt(int row, int column, bool isFalling = false)
        {
            if (row >= 0 && row < Height && column >= 0 && column < Width)
            {
                var removedTile = _tiles[row, column];
                if (isFalling)
                {
                    OnTileFalling?.Invoke(removedTile);
                }
                else
                {
                    OnTileRemoved?.Invoke(removedTile);
                }
                _tiles[row, column] = null;
            }
        }

        public void FallTile(int row, int col, int rowAbove, Tile tileAbove)
        {
            SetTileAt(row, col, tileAbove);
            RemoveTileAt(rowAbove, col, true);
            tileAbove.UpdatePosition(row, col);
        }
    }
}