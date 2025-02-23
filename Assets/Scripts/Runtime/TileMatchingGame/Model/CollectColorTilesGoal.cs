using Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces
{
    public class CollectColorTilesGoal : IGoal
    {
        private readonly Dictionary<TileColor, int> _amountByColor = new Dictionary<TileColor, int>();
        private readonly Dictionary<TileColor, int> _currentByColor = new Dictionary<TileColor, int>();
        public GoalsEnum Goal => GoalsEnum.ColorTilesGoal;

        public string GetDescription()
        {
            
            return $"You must get:\n {string.Join("\n", _amountByColor.Select(par => $"{par.Key} {par.Value} tiles"))}";
        }

        public string GetProgress()
        {
            return string.Join("\n", _amountByColor.Select(par => $"{_currentByColor[par.Key]}/{par.Value} {par.Key}\n"));
        }

        public bool HasFailedGoal()
        {
            return false;
        }

        public bool IsGoalCompleted()
        {
            return _currentByColor.All(par => par.Value > _amountByColor[par.Key]);
        }

        public void SetupGoal(Level.GoalSetup[] setupData)
        {
            _currentByColor.Clear();

            foreach (var colorGoal in setupData.Where(data => data.goalEnum == GoalsEnum.ColorTilesGoal))
            {
                _amountByColor[colorGoal.tileColor] = colorGoal.tileQuantity;
                _currentByColor[colorGoal.tileColor] = 0;
            };
        }

        public void UpdateProgress(object progressData)
        {
            if (progressData is Tile tile && _currentByColor.Keys.Contains(tile.TileData.Color))
            {
                _currentByColor[tile.TileData.Color]++;
            }
        }

    }
}