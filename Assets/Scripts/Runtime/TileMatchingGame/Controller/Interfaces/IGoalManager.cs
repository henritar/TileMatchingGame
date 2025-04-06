using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces
{
    public interface IGoalManager : IDisposable
    {
        IReadOnlyList<IGoal> CurrentLevelGoals { get; }
        public void SetupLevelGoals(Level.GoalSetup[] goalsSetup);
        public bool CheckForVictory();
        public bool CheckForGameOver();
    }
}