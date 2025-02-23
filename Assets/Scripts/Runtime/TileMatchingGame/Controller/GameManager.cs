using Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates;
using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.Services.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.View;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller
{
    public class GameManager
    {
        private IBoard _board;
        private IGameState _currentState;
        private IMatchFinder _matchFinder;
        private IBoardModifier _boardModifier;

        public IMatchFinder MatchFinder { get => _matchFinder; }
        public GameManager()
        {
            _matchFinder = DIContainer.Instance.Resolve<IMatchFinder>();
            _boardModifier = DIContainer.Instance.Resolve<IBoardModifier>();
        }

        public GameManager(IMatchFinder matchFinder, IBoardModifier boardModifier)
        {
            _matchFinder = matchFinder;
            _boardModifier = boardModifier;
        }

        public void ChangeState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void StartGame()
        {
            ChangeState(new PlayingState(this));
        }

        public void RefillBoard()
        {
            _boardModifier.FillEmptySpaces();
        }

        public void HandleTileClick(TileView tileView)
        {
            _currentState?.HandleTileClick(tileView.Tile);
        }

        public void OnMatchedTiles(List<Tile> matchedTiles)
        {
            _boardModifier.RemoveTiles(matchedTiles);

            //_boardModifier.UpdateTilesPosition();

            //RefillBoard();
        }
    }

    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        GameOver,
        Victory,
    }

}
