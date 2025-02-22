
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Model
{
    public class Tile 
    {
        public TileColor Color { get; set; }
        public Sprite Sprite { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }

    public enum TileColor
    {
        Red,
        Blue,
        Green,
        Yellow,
        Purple
    }
}
