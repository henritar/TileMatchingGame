using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates
{
    public class VictoryState : IGameState
    {

        private LevelManager _levelManager;
        private RectTransform _victoryView;

        public GameStateEnum State => GameStateEnum.Victory;


        public VictoryState(LevelManager levelManager, RectTransform victoryView)
        {
            _levelManager = levelManager;
            _victoryView = victoryView;
        }


        public void Enter()
        {
            _victoryView.gameObject.SetActive(true);
            _levelManager.SetNextLevel();
        }

        public void Exit()
        {
        }

        public void HandleTileClick(Tile tile)
        {

        }
    }
}