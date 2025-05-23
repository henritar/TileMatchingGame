﻿using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using System;

namespace Assets.Scripts.Runtime.TileMatchingGame.Model
{
    public class Board : IBoard
    {
        private Tile[,] _tiles;
        public int Width { get; set; } 
        public int Height { get; set; }

        public event Action OnBoardUpdate;
        public event Action<Tile> OnTileRemoved;
        public event Action<Tile> OnTileFalling;

        public Tile GetTileAt(int row, int column)
        {
            if (_tiles == null || _tiles.Length <= 0 || row >= _tiles.Length / Height || column >= _tiles.Length / Width)
            {
                return null;
            }

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
            }
        }
        public void RemoveTileAt(int row, int column, bool isFalling = false)
        {
            if (row >= 0 && row < Height && column >= 0 && column < Width)
            {
                var removedTile = _tiles[row, column];
                if (!isFalling)
                {
                   OnTileRemoved?.Invoke(removedTile);
                }
                _tiles[row, column] = null;
            }

        }

        public void ResetBoard()
        {
            _tiles = new Tile[Height, Width];
            OnBoardUpdate?.Invoke();
        }

        public void FallTile(int row, int col, int rowAbove, Tile tileAbove)
        {
            SetTileAt(row, col, tileAbove);
            tileAbove.UpdatePosition(row, col);
            RemoveTileAt(rowAbove, col, true);
            OnTileFalling?.Invoke(tileAbove);
        }
    }
}