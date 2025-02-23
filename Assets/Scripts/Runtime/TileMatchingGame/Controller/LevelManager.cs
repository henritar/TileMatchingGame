using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller
{
    public class LevelManager
    {
        private List<Level> _levels;
        private int _currentLevelIndex;

        public Level CurrentLevel => _levels[_currentLevelIndex]; 

        public LevelManager(List<Level> levels)
        {
            _levels = levels;
            _currentLevelIndex = 0;
        }

        // Carrega o nível atual
        public void LoadLevel()
        {
            Level level = CurrentLevel;

            IBoard board = DIContainer.Instance.Resolve<IBoard>();
            board.Width = level.BoardWidth;
            board.Height = level.BoardHeight;

            GameManager gameManager = DIContainer.Instance.Resolve<GameManager>();
            gameManager.StartGame();
        }

        public void NextLevel()
        {
            _currentLevelIndex++;
            if (_currentLevelIndex >= _levels.Count)
            {
                _currentLevelIndex = 0;
            }
            LoadLevel();
        }

        public void RestartLevel()
        {
            LoadLevel();
        }
    }
}