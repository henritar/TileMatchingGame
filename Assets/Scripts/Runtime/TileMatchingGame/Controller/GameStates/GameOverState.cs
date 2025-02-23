using Assets.Scripts.Runtime.TileMatchingGame.Model;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates
{
    public class GameOverState : IGameState
    {
        private RectTransform _gameOverView;

        public GameStateEnum State => GameStateEnum.GameOver;

        public GameOverState(RectTransform gameOverView)
        {
            _gameOverView = gameOverView;
        }


        public void Enter()
        {
            _gameOverView.gameObject.SetActive(true);
        }

        public void Exit()
        {
        }

        public void HandleTileClick(Tile tile)
        {
        }
    }
}