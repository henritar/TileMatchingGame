
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces
{
    public interface IBoard 
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public IReadOnlyList<Tile> BoardTiles { get; }
        public Tile GetTileAt(int row, int column);
        public void SetTileAt(int row, int column, Tile newTile);
        public void RemoveTileAt(int row, int column, bool isFalling = false);
        void FallTile(int row, int col, int rowAbove, Tile tileAbove);

        //public event Action OnBoardUpdated;
        public event Action<Tile> OnTileRemoved;
        public event Action<Tile> OnTileFalling;
    }
}