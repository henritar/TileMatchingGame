using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
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

        public void HandleTileClick(Tile tile)
        {
            _gameManager.HandleTileClick(tile);
        }  

    }
}