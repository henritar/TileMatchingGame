using System;

namespace Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces
{
    public interface IGoal : IDisposable
    {
        public GoalsEnum Goal { get; }
        public bool IsGoalCompleted();
        public bool HasFailedGoal();
        public string GetDescription();
        public string GetProgress();
    }

    public interface IGoal<T> : IGoal
    {
        public void UpdateProgress(T progressData);
    }

    public enum GoalsEnum
    {
        TotalPointsGoal,
        MaxMovesGoal,
        ColorTilesGoal
    }
}