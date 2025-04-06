using Assets.Scripts.Runtime.TileMatchingGame.Controller;
using Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.TileMatchingGame.View
{
    public class GameHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _goalsText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _goalsButton;
        [SerializeField] private Button _closeGoalsButton;

        private GameManager _gameManager;
        private IScoreManager _scoreManager;
        private IGoalManager _goalManager;
        private LevelManager _levelManager;

        public void Initialize(GameManager gameManager, IScoreManager scoreManager, IGoalManager goalManager, LevelManager levelManager)
        {
            _gameManager = gameManager;
            _scoreManager = scoreManager;
            _goalManager = goalManager;
            _levelManager = levelManager;
            _scoreManager.OnScoreChanged += SetGoalsText;
            _restartButton.onClick.AddListener(LoadLevelButton);
            _nextLevelButton.onClick.AddListener(LoadLevelButton);
            _goalsButton.onClick.AddListener(() => _gameManager.ChangeState(GameStateEnum.Goals));
            _closeGoalsButton.onClick.AddListener(() => _gameManager.ChangeState(GameStateEnum.LastState));

            UpdateScoreDisplay(_scoreManager.CurrentScore);
        }

        public void SetGoalsText(int newScore)
        {
            UpdateScoreDisplay(newScore);
        }

        public void SetGoalsDescription()
        {
            _goalsText.text = string.Join("\n\n",
                _goalManager.CurrentLevelGoals.Select(goal =>
                    $"{goal.GetDescription()}\n{goal.GetProgress()}"));

        }

        private void LoadLevelButton()
        {
            _levelManager.LoadLevel();
        }

        private void UpdateScoreDisplay(int newScore)
        {
            _scoreText.text = $"Score: {newScore}";
        }

        private void ShowGoalsView()
        {
            _gameManager.ChangeState(GameStateEnum.Goals);
        }
    }
}