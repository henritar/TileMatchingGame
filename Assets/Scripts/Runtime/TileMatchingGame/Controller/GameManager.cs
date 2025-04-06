using Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates;
using Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces;
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
        private IScoreManager _scoreManager;
        private ISoundManager _soundManager;
        private IGameState[] _gameStates;

        private readonly Dictionary<GameStateEnum, IGameState> _gameStatesDict = new Dictionary<GameStateEnum, IGameState>();

        public IMatchFinder MatchFinder { get => _matchFinder; }
        public event Action OnNextMove;
        public event Action OnEndTurn;

        public GameManager(IBoard board, IMatchFinder matchFinder, IBoardModifier boardModifier, IScoreManager scoreManager, ISoundManager soundManager, Func<GameManager, IGameState[]> gameStateFactory)
        {
            _board = board;
            _matchFinder = matchFinder;
            _boardModifier = boardModifier;
            _scoreManager = scoreManager;
            _soundManager = soundManager;
            _gameStates = gameStateFactory(this);
        }

        private void CreateGameStates()
        {
            foreach (var gameState in _gameStates) 
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

            var newState = newStateEnum == GameStateEnum.LastState ? _lastState : _gameStatesDict[newStateEnum];

            _lastState = _currentState;
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void StartGame()
        {
            CreateGameStates();
            _boardModifier.RestartBoard();
            ChangeState(GameStateEnum.Playing);
        }

        public void ResetGame()
        {
            _scoreManager.ResetScore();
            _boardModifier.RestartBoard();
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
            _soundManager.PlaySound(AppConstants.TilePopSound);
            _scoreManager.AddScore(matchedTiles.Count);
            _boardModifier.RemoveTiles(matchedTiles);

            _boardModifier.UpdateTilesPosition();

            OnNextMove?.Invoke();

            RefillBoard();

            OnEndTurn?.Invoke();
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
        Goals,
        LastState
    }

}