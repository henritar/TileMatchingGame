using Assets.Scripts.Runtime.TileMatchingGame.Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Runtime.TileMatchingGame.View
{
    public class TileView : MonoBehaviour
    {
        private Tile _tile;
        [SerializeField]
        public SpriteRenderer SpriteRenderer;

        public void Initialize(Tile tile)
        {
            _tile = tile;
            UpdateVisuals();
        }

        public void UpdateVisuals()
        {
            if (_tile != null && SpriteRenderer != null)
            {
                SpriteRenderer.sprite = _tile.TileData.Sprite; 
            }
        }
    }
}
