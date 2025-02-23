using Assets.Scripts.Runtime.TileMatchingGame.Model;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates
{
    public class PauseState : IGameState
    {
        private RectTransform _pauseView;
        public GameStateEnum State => GameStateEnum.Paused;

        public PauseState(RectTransform pauseView)
        {
            _pauseView = pauseView;
        }

        public void Enter()
        {
            Time.timeScale = 0f;
            _pauseView.gameObject.SetActive(true);
        }

        public void Exit()
        {
            _pauseView.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        public void HandleTileClick(Tile tile)
        {
        }
    }
}