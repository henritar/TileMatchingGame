using Assets.Scripts.Runtime.TileMatchingGame.Controller;
using Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.DI;
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
        private LevelManager _levelManager;

        void Start()
        {
            _gameManager = DIContainer.Instance.Resolve<GameManager>();
            _scoreManager = DIContainer.Instance.Resolve<IScoreManager>();
            _levelManager = DIContainer.Instance.Resolve<LevelManager>();
            _scoreManager.OnScoreChanged += SetGoalsText;
            _restartButton.onClick.AddListener(() => _levelManager.LoadLevel());
            _nextLevelButton.onClick.AddListener(() => _levelManager.LoadLevel());
            _goalsButton.onClick.AddListener(() => _gameManager.ChangeState(GameStateEnum.Goals));
            _closeGoalsButton.onClick.AddListener(() => _gameManager.ChangeState(GameStateEnum.LastState));

            UpdateScoreDisplay(_scoreManager.CurrentScore);
        }

        public void SetGoalsText(int newScore)
        {
            _goalsText.text = string.Join("\n",
                _gameManager.LevelGoals.Select(goal =>
                    $"{goal.GetDescription()}\n{goal.GetProgress()}"));

            UpdateScoreDisplay(newScore);
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