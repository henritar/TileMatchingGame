using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using System.Linq;
using static Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects.Level;

namespace Assets.Scripts.Runtime.TileMatchingGame.Model
{
    public class MaxMovesGoal : IGoal
    {
        private int _maxMoves;
        private int _totalMoves;
        public GoalsEnum Goal => GoalsEnum.MaxMovesGoal;

        public MaxMovesGoal()
        {
            _totalMoves = 0;
            _maxMoves = int.MaxValue;
        }

        public string GetDescription()
        {
            return $"Beat this level with less then {_maxMoves} moves!";
        }

        public string GetProgress()
        {
            return $"{_maxMoves - _totalMoves} left!";
        }

        public bool HasFailedGoal()
        {
            return _totalMoves >= _maxMoves;
        }

        public bool IsGoalCompleted()
        {
            return _maxMoves >= _totalMoves;
        }

        public void SetupGoal(GoalSetup[] setupData)
        {
            var maxMovesGoal = setupData.First(data => data.goalEnum == GoalsEnum.MaxMovesGoal);
            _maxMoves = maxMovesGoal.maxPoints;
            _totalMoves = 0;
        }

        public void UpdateProgress(object progressData)
        {
            _totalMoves++;
        }

    }
}