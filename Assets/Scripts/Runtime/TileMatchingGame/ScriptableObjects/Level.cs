using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level", menuName = "Level/LevelData")]
    public class Level : ScriptableObject
    {
        public int BoardWidth; 
        public int BoardHeight;
        public int TileMatchPoints;
        public GoalSetup[] LevelGoals;

        [System.Serializable]
        public struct GoalSetup
        {
            public GoalsEnum goalEnum;
            public int maxPoints;
            public TileColor tileColor;
            public int tileQuantity;
        }

        public enum GoalValueType
        {
            Int,
            Float,
            String
        }
    }
}