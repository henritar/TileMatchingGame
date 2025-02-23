using Assets.Scripts.Runtime.TileMatchingGame.Model;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates
{
    public interface IGameState
    {
        GameStateEnum State { get; }
        void Enter();
        void Exit();
        void HandleTileClick(Tile tile);
    }
}