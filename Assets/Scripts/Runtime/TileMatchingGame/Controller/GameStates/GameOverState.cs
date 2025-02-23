using Assets.Scripts.Runtime.TileMatchingGame.Model;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates
{
    public class GameOverState : IGameState
    {
        public GameStateEnum State => GameStateEnum.Victory;

        public void Enter()
        {
            //Show GameOver Interface
        }

        public void Exit()
        {
        }

        public void HandleTileClick(Tile tile)
        {
        }
    }
}