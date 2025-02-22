using Assets.Scripts.Runtime.TileMatchingGame.Controller;
using Assets.Scripts.Runtime.TileMatchingGame.DI;
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

        private GameplayController _gameplayController;

        private void Start()
        {
            _gameplayController = DIContainer.Instance.Resolve<GameplayController>();
        }

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

        public void OnPointerClick()
        {
            _gameplayController.HandleTileClick(_tile);
        }
    }
}
