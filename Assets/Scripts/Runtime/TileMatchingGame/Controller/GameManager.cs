using Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates;
using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.Services.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.View;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller
{
    public class GameManager
    {
        private IBoard _board;
        private IGameState _currentState;
        private IGameState _lastState;
        private IMatchFinder _matchFinder;
        private IBoardModifier _boardModifier;

        private readonly Dictionary<GameStateEnum, IGameState> _gameStatesDict = new Dictionary<GameStateEnum, IGameState>();

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

        private void CreateGameStates()
        {
            var gameStates = DIContainer.Instance.Resolve<IGameState[]>();
            foreach (var gameState in gameStates) 
            {
                _gameStatesDict[gameState.State] = gameState;
            }
        }

        public void ChangeState(GameStateEnum newStateEnum)
        {
            if (newStateEnum == _currentState?.State)
            {
                return;
            }

            var newState = _gameStatesDict[newStateEnum];

            _lastState = _currentState;
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void StartGame()
        {
            CreateGameStates();
            ChangeState(GameStateEnum.Playing);
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

            _boardModifier.UpdateTilesPosition();

            RefillBoard();
        }

        public void OnPausePressed()
        {
            if (_currentState.State != GameStateEnum.Paused)
            {
                ChangeState(GameStateEnum.Paused);
            }
            else
            {
                ChangeState(_lastState.State);
            }
        }
    }

    public enum GameStateEnum
    {
        Menu,
        Playing,
        Paused,
        GameOver,
        Victory,
    }

}
