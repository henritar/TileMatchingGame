using Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates
{
    public class GameOverState : IGameState
    {
        private RectTransform _gameOverView;
        private ISoundManager _soundManager;

        public GameStateEnum State => GameStateEnum.GameOver;

        public GameOverState(RectTransform gameOverView, ISoundManager soundManager)
        {
            _gameOverView = gameOverView;
            _soundManager = soundManager;
        }


        public void Enter()
        {
            _gameOverView.gameObject.SetActive(true);
            _soundManager.PlaySound(AppConstants.GameOverSound);
        }

        public void Exit()
        {
            _gameOverView.gameObject.SetActive(false);
        }

        public void HandleTileClick(Tile tile)
        {
        }
    }
}