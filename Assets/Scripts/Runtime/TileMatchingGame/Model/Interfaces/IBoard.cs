
using System;

namespace Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces
{
    public interface IBoard 
    {
        public int Width { get; }
        public int Height { get; }

        public Tile GetTileAt(int row, int column);
        public void SetTileAt(int row, int column, Tile newTile);

        public event Action OnBoardUpdated;
    }
}