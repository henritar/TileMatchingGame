using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;

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
            return $"You have {_maxMoves - _totalMoves} moves to go!";
        }

        public bool HasFailedGoal()
        {
            return _totalMoves >= _maxMoves;
        }

        public bool IsGoalCompleted()
        {
            return _maxMoves >= _totalMoves;
        }

        public void SetupGoal(object setupData)
        {
            if (setupData is int maxMoves)
            {
                _maxMoves = maxMoves;
                _totalMoves = 0;
            }
        }

        public void UpdateProgress(object progressData)
        {
            _totalMoves++;
        }

    }
}