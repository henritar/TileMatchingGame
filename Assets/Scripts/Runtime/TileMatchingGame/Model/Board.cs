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

        public Board(int width, int height)
        {
            _width = width;
            _height = height;
            _tiles = new Tile[width, height];
        }

        public Tile GetTileAt(int row, int column)
        {
            if (row >= 0 && row < Height && column >= 0 && column < Width)
            {
                return _tiles[column, row]; 
            }
            return null;
        }

        public void SetTileAt(int row, int column, Tile newTile)
        {
            if (row >= 0 && row < Height && column >= 0 && column < Width)
            {
                _tiles[column, row] = newTile;
                OnBoardUpdated?.Invoke(); 
            }
        }
    }
}