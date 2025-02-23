using Assets.Scripts.Runtime.TileMatchingGame.Controller;
using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Runtime.TileMatchingGame.View
{
    public class TileView : MonoBehaviour
    {
        private Tile _tile;
        public Tile Tile { get => _tile; }
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
            _gameplayController.HandleTileClick(this);
        }

        private void OnTilePositionUpdated(Vector3 targetPosition)
        {
            StartCoroutine(AnimateFalling(targetPosition));
        }

        // Corrotina para animar a queda da Tile
        private IEnumerator AnimateFalling(Vector3 targetPosition)
        {
            float duration = 0.25f; 
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
        }
    }
}
