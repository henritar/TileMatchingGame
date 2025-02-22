
using Assets.Scripts.Runtime.TileMatchingGame.Controller;
using Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Model
{
    public class Tile 
    {
        public TileFlyweight TileData { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public Tile(TileFlyweight tileData, int row, int column)
        {
            TileData = tileData;
            Row = row;
            Column = column;
        }

        public void UpdatePosition(int newRow, int newColumn)
        {
            Row = newRow;
            Column = newColumn;
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
