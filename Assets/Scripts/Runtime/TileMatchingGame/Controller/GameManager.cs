using Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates;
using Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.Services.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.View;
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
        private List<IGoal> _completedGoals;

        private readonly Dictionary<GameStateEnum, IGameState> _gameStatesDict = new Dictionary<GameStateEnum, IGameState>();
        private readonly Dictionary<GoalsEnum, IGoal> _levelGoalsDict = new Dictionary<GoalsEnum, IGoal>();

        public IMatchFinder MatchFinder { get => _matchFinder; }
        public IReadOnlyList<IGoal> LevelGoals { get => _levelGoalsDict.Values.ToList().AsReadOnly(); }
        public GameManager()
        {
            _matchFinder = DIContainer.Instance.Resolve<IMatchFinder>();
            _boardModifier = DIContainer.Instance.Resolve<IBoardModifier>();
            _scoreManager = DIContainer.Instance.Resolve<IScoreManager>();
        }

        public GameManager(IMatchFinder matchFinder, IBoardModifier boardModifier, IScoreManager scoreManager)
        {
            _matchFinder = matchFinder;
            _boardModifier = boardModifier;
            _scoreManager = scoreManager;
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

            var newState = newStateEnum == GameStateEnum.LastState ? _lastState : _gameStatesDict[newStateEnum];

            _lastState = _currentState;
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        public void SetGoals(List<GoalSetup> goalsSetup)
        {
            var levelGoals = DIContainer.Instance.Resolve<IGoal[]>();
            _levelGoalsDict.Clear();

            foreach (var goalSetup in goalsSetup)
            {
                _levelGoalsDict[goalSetup.Goal] = levelGoals.First(g => g.Goal == goalSetup.Goal);

                switch (goalSetup.GoalValueType)
                {
                    case GoalValueType.Int:
                        int intValue = int.Parse(goalSetup.GoalValue);
                        _levelGoalsDict[goalSetup.Goal].SetupGoal(intValue);
                        break;
                    case GoalValueType.Float:
                        float floatValue = float.Parse(goalSetup.GoalValue);
                        _levelGoalsDict[goalSetup.Goal].SetupGoal(floatValue);
                        break;
                    case GoalValueType.String:
                        _levelGoalsDict[goalSetup.Goal].SetupGoal(goalSetup.GoalValue);
                        break;
                }

                switch (goalSetup.Goal)
                {
                    case GoalsEnum.TotalPointsGoal:
                    case GoalsEnum.MaxMovesGoal:
                        _scoreManager.OnScoreChanged += (points) => _levelGoalsDict[goalSetup.Goal].UpdateProgress(points);
                        break;
                    case GoalsEnum.ColorTilesGoal:
                        _board.OnTileRemoved += (tile) => _levelGoalsDict[goalSetup.Goal].UpdateProgress(tile);
                        break;
                }
            }
        }

        public void StartGame()
        {
            CreateGameStates();
            _boardModifier.RestartBoard();
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
            _scoreManager.AddScore(matchedTiles.Count);
            _boardModifier.RemoveTiles(matchedTiles);

            _boardModifier.UpdateTilesPosition();

            UpdateGoals();

            RefillBoard();
        }

        private void UpdateGoals()
        {
            bool allGoalsCompleted = _levelGoalsDict.Values.All(goal => goal.IsGoalCompleted());
            if (allGoalsCompleted)
            {
                ChangeState(GameStateEnum.Victory);
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