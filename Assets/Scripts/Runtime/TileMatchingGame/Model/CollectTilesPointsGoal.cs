﻿using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using System.Linq;
using static Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects.Level;


namespace Assets.Scripts.Runtime.TileMatchingGame.Model
{
    public class CollectTilesPointsGoal : IGoal
    {
        private int _amountToCollect;
        private int _currentScore;

        public GoalsEnum Goal => GoalsEnum.TotalPointsGoal;

        public CollectTilesPointsGoal()
        {
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

        public void UpdateProgress(object progressData)
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

        public void SetupGoal(GoalSetup[] setupData)
        {
            var pointsGoal = setupData.First(data => data.goalEnum == GoalsEnum.TotalPointsGoal);
            _amountToCollect = pointsGoal.maxPoints;
            _currentScore = 0;
        }
    }
}