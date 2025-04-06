using Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using System.Linq;
using static Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects.Level;


namespace Assets.Scripts.Runtime.TileMatchingGame.Model
{
    public class CollectTilesPointsGoal : IGoal<int>
    {
        private readonly IScoreManager _scoreManager;

        private int _amountToCollect;
        private int _currentScore;

        public GoalsEnum Goal => GoalsEnum.TotalPointsGoal;

        public CollectTilesPointsGoal(IScoreManager scoreManager, GoalSetup setupData)
        {
            _scoreManager = scoreManager;
            _scoreManager.OnScoreChanged += UpdateProgress;
            _currentScore = 0;

            _amountToCollect = setupData.maxPoints;
            _currentScore = 0;
        }

        public bool IsGoalCompleted()
        {
            return _currentScore >= _amountToCollect;
        }

        public string GetDescription()
        {
            return $"Collect {_amountToCollect} points!";
        }

        public string GetProgress()
        {
            return $"{_currentScore}/{_amountToCollect}";
        }

        public void UpdateProgress(int progressData)
        {
            if (progressData is int score)
            {
                _currentScore = score;
            }
        }

        public bool HasFailedGoal()
        {
            return false;
        }

        public void Dispose()
        {
            _scoreManager.OnScoreChanged -= UpdateProgress;
        }
    }
}