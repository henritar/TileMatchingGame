using Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces;
using System;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller
{
    public class ScoreManager : IScoreManager
    {
        public event Action<int> OnScoreChanged;
        private int _currentScore;
        private int _tileValueScore = 10;
        public int CurrentScore
        {
            get => _currentScore;
            private set
            {
                _currentScore = value;
                OnScoreChanged?.Invoke(_currentScore); // Dispara o evento quando a pontuação muda
            }
        }

        public void SetTileValueScore(int tileValueScore)
        {
            _tileValueScore = tileValueScore;
        }

        public void AddScore(int points)
        {
            CurrentScore += points * _tileValueScore;
        }

        public void ResetScore()
        {
            CurrentScore = 0;
        }
    }
}