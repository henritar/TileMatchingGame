using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates
{
    public class VictoryState : IGameState
    {

        private LevelManager _levelManager;

        public GameStateEnum State => GameStateEnum.Victory;

        public VictoryState()
        {
            _levelManager = DIContainer.Instance.Resolve<LevelManager>();
        }

        public VictoryState(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }


        public void Enter()
        {
            //Show Victory Interface
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