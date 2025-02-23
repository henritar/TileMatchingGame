using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates
{
    public class PlayingState : IGameState
    {
        private GameManager _gameManager;

        public GameStateEnum State => GameStateEnum.Playing;

        public PlayingState()
        {
            _gameManager = DIContainer.Instance.Resolve<GameManager>();
        }

        public PlayingState(GameManager gameManager)
        {
            _gameManager = gameManager;
        }


        public void Enter()
        {
            _gameManager.RefillBoard();
        }

        public void Exit()
        {
        }

        public void HandleTileClick(Tile tile)
        {
            
            List<Tile> matchedTiles = _gameManager.MatchFinder.FindMatches(tile);
            if (matchedTiles.Count >= 2)
            {
                _gameManager.OnMatchedTiles(matchedTiles);
            }
        }
    }
}