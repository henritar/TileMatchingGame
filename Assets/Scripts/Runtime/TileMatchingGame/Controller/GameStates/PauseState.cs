using Assets.Scripts.Runtime.TileMatchingGame.Model;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates
{
    public class PauseState : IGameState
    {
        public GameStateEnum State => GameStateEnum.Paused;

        public void Enter()
        {
            Time.timeScale = 0f;
        }

        public void Exit()
        {
            Time.timeScale = 1f;
        }

        public void HandleTileClick(Tile tile)
        {
        }
    }
}