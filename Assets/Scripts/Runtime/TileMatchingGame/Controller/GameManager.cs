using Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates;
using Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.Services.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.View;
using System;
using System.Collections.Generic;
using System.Linq;
using static Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects.Level;

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
        private IGoal[] _levelGoals;
        private List<IGoal> _completedGoals;

        private readonly Dictionary<GameStateEnum, IGameState> _gameStatesDict = new Dictionary<GameStateEnum, IGameState>();
        private readonly Dictionary<GoalsEnum, IGoal> _levelGoalsDict = new Dictionary<GoalsEnum, IGoal>();

        public IMatchFinder MatchFinder { get => _matchFinder; }
        public IReadOnlyList<IGoal> LevelGoals { get => _levelGoalsDict.Values.ToList().AsReadOnly(); }
        public event Action OnNextMove;

        public GameManager(IBoard board, IMatchFinder matchFinder, IBoardModifier boardModifier, IScoreManager scoreManager, ISoundManager soundManager, IGoal[] goals, Func<GameManager, IGameState[]> gameStateFactory)
        {
            _board = board;
            _matchFinder = matchFinder;
            _boardModifier = boardModifier;
            _scoreManager = scoreManager;
            _soundManager = soundManager;
            _levelGoals = goals;
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

        public void SetGoals(GoalSetup[] goalsSetup)
        {
            UnregisterGoals();

            foreach (var goalSetup in goalsSetup)
            {

                _levelGoalsDict[goalSetup.goalEnum] = _levelGoals.First(g => g.Goal == goalSetup.goalEnum);
                _levelGoalsDict[goalSetup.goalEnum].SetupGoal(goalsSetup);

                switch (goalSetup.goalEnum)
                {
                    case GoalsEnum.TotalPointsGoal:
                        _scoreManager.OnScoreChanged += OnScoreChangedHandler;
                        break;
                    case GoalsEnum.MaxMovesGoal:
                        OnNextMove += OnNextMoveHandler;
                        break;
                    case GoalsEnum.ColorTilesGoal:
                        _board.OnTileRemoved -= OnTileRemovedHandler;
                        _board.OnTileRemoved += OnTileRemovedHandler;
                        break;
                }
            }
        }

        private void UnregisterGoals()
        {
            foreach (var goal in _levelGoalsDict.Values)
            {
                switch (goal.Goal)
                {
                    case GoalsEnum.TotalPointsGoal:
                        _scoreManager.OnScoreChanged -= OnScoreChangedHandler;
                        break;
                    case GoalsEnum.MaxMovesGoal:
                        OnNextMove -= OnNextMoveHandler;
                        break;
                    case GoalsEnum.ColorTilesGoal:
                        _board.OnTileRemoved -= OnTileRemovedHandler;
                        break;
                }
            }

            _levelGoalsDict.Clear();
        }

        private void OnScoreChangedHandler(int points)
        {
            if (_levelGoalsDict.ContainsKey(GoalsEnum.TotalPointsGoal))
                _levelGoalsDict[GoalsEnum.TotalPointsGoal].UpdateProgress(points);
        }

        private void OnNextMoveHandler()
        {
            if (_levelGoalsDict.ContainsKey(GoalsEnum.MaxMovesGoal))
                _levelGoalsDict[GoalsEnum.MaxMovesGoal].UpdateProgress(null);
        }

        private void OnTileRemovedHandler(Tile tile)
        {
            if (_levelGoalsDict.ContainsKey(GoalsEnum.ColorTilesGoal))
                _levelGoalsDict[GoalsEnum.ColorTilesGoal].UpdateProgress(tile);
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

            UpdateGoals();
        }

        private void UpdateGoals()
        {
            bool allGoalsCompleted = _levelGoalsDict.Values.All(goal => goal.IsGoalCompleted());
            bool hasfailedAnyGoal = _levelGoalsDict.Values.Any(goal => goal.HasFailedGoal());
            if (allGoalsCompleted)
            {
                ChangeState(GameStateEnum.Victory);
            }
            else if (hasfailedAnyGoal)
            {
                ChangeState(GameStateEnum.GameOver);
            }
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