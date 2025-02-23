using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Level", menuName = "Level/LevelData")]
    public class Level : ScriptableObject
    {
        public int BoardWidth; 
        public int BoardHeight;
        public int TileMatchPoints;
    }
}