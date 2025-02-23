using Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Model
{
    public class Tile 
    {
        public readonly int Id;
        public TileFlyweight TileData { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        private static int _nextId = 0;

        public Tile(TileFlyweight tileData, int row, int column)
        {
            Id = _nextId++;
            TileData = tileData;
            Row = row;
            Column = column;
        }

        public void UpdatePosition(int newRow, int newColumn)
        {
            Row = newRow;
            Column = newColumn;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }

    [SerializeField]
    public enum TileColor
    {
        Red,
        Blue,
        Green,
        Yellow,
        Purple
    }
}
