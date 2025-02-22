using Assets.Scripts.Runtime.TileMatchingGame.Controller;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Runtime.TileMatchingGame.View
{
    public class TileView : MonoBehaviour, IPointerClickHandler
    {
        private Tile _tile;
        [SerializeField]
        public SpriteRenderer SpriteRenderer;

        private GameplayController _gameplayController;

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

        public void OnPointerClick(PointerEventData eventData)
        {
            _gameplayController.HandleTileClick(_tile);
        }
    }
}
