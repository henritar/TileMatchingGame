using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller
{
    public class LevelManager
    {
        private GameManager _gameManager;
        private IBoard _board;
        private List<Level> _levels;
        private int _currentLevelIndex;

        public Level CurrentLevel => _levels[_currentLevelIndex]; 

        public LevelManager(GameManager gameManager, IBoard board, List<Level> levels)
        {
            _gameManager = gameManager;
            _board = board;
            _levels = levels;
            _currentLevelIndex = 0;
        }

        public void LoadLevel()
        {
            Level level = CurrentLevel;

            _gameManager.ResetGame();

            _board.Width = level.BoardWidth;
            _board.Height = level.BoardHeight;

            _gameManager.StartGame();
            _gameManager.SetGoals(level.LevelGoals);
        }

        public void SetLevel(int index)
        {
            _currentLevelIndex = Mathf.Clamp(index, 0, _levels.Count);
        }

        public void SetNextLevel()
        {
            _currentLevelIndex++;
            if (_currentLevelIndex >= _levels.Count)
            {
                _currentLevelIndex = 0;
            }
        }

        public void RestartLevel()
        {
            LoadLevel();
        }
    }
}