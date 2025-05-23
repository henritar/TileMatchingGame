using Assets.Scripts.Runtime.TileMatchingGame.Controller;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.View
{
    public class TileView : MonoBehaviour
    {
        private Tile _tile;
        public Tile Tile { get => _tile; }
        [SerializeField]
        public SpriteRenderer SpriteRenderer;
        public Vector3 OriginalSpriteBoundsSize { get; private set; }

        private GameplayController _gameplayController;

        private void Awake()
        {
            OriginalSpriteBoundsSize = SpriteRenderer.bounds.size;
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

        public void OnTilePositionUpdated(Vector3 targetPosition)
        {
            StartCoroutine(AnimateFalling(targetPosition));
        }

        private IEnumerator AnimateFalling(Vector3 targetPosition)
        {
            float duration = 0.3f;
            float elapsedTime = 0f;
            Vector3 startPosition = transform.position;

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
        }
    }
}
