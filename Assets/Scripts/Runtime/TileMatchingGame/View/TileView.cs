using Assets.Scripts.Runtime.TileMatchingGame.Model;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.View
{
    public class TileView : MonoBehaviour
    {
        public Tile Tile { get; private set; }
        private SpriteRenderer _spriteRenderer;
    }
}
