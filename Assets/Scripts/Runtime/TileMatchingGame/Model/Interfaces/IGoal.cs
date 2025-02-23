using static Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects.Level;

namespace Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces
{
    public interface IGoal
    {
        public GoalsEnum Goal { get; }
        public bool IsGoalCompleted();
        public bool HasFailedGoal();
        public string GetDescription();
        public void UpdateProgress(object progressData);
        public string GetProgress();
        public void SetupGoal(GoalSetup[] setupData);
    }

    public enum GoalsEnum
    {
        TotalPointsGoal,
        MaxMovesGoal,
        ColorTilesGoal
    }
}