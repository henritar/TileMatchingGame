using Assets.Scripts.Runtime.TileMatchingGame.Model;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TileFlyweight", menuName = "Tile/TileData")]
    public class TileFlyweight : ScriptableObject
    {
        public TileColor Color;
        public Sprite Sprite;
    }
}