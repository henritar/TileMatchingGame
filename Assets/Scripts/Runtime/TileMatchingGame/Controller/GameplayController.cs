using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.View;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller
{
    public class GameplayController
    {
        private readonly GameManager _gameManager;

        public GameplayController()
        {
            _gameManager = DIContainer.Instance.Resolve<GameManager>();
        }

        public GameplayController(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void ObserveClickHandler()
        {
            if (Input.GetMouseButtonDown(0)) // Clique esquerdo
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject.TryGetComponent(out TileView tileView))
                {
                    tileView.OnPointerClick();
                }
            }
        }

        public void HandleTileClick(TileView tileView)
        {
            _gameManager.HandleTileClick(tileView);
        }  

    }
}