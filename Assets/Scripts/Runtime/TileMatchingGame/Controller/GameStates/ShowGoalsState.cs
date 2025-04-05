using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.View;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates
{
    public class ShowGoalsState : IGameState
    {
        private RectTransform _goalsView;
        private GameHUD _gameHUD;

        public GameStateEnum State => GameStateEnum.Goals;

        public ShowGoalsState(RectTransform goalsView, GameHUD gameHUD)
        {
            _goalsView = goalsView;
            _gameHUD = gameHUD;
        }

        public void Enter()
        {
            _gameHUD.SetGoalsDescription();
            _goalsView.gameObject.SetActive(true);
        }

        public void Exit()
        {
            _goalsView.gameObject.SetActive(false);
        }

        public void HandleTileClick(Tile tile)
        {

        }
    }
}