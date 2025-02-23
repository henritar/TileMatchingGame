using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates
{
    public class ShowGoalsState : IGameState
    {
        private RectTransform _goalsView;

        public GameStateEnum State => GameStateEnum.Goals;

        public ShowGoalsState(RectTransform goalsView)
        {
            _goalsView = goalsView;
        }


        public void Enter()
        {
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