using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces
{
    public interface IScoreManager
    {
        public event Action<int> OnScoreChanged;
        void AddScore(int points);
        void ResetScore();
        int CurrentScore { get; } // Propriedade
    }
}