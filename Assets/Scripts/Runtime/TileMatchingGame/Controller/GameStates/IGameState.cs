using Assets.Scripts.Runtime.TileMatchingGame.Model;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates
{
    public interface IGameState
    {
        void Enter();
        void Exit();
        void HandleTileClick(Tile tile);
    }
}