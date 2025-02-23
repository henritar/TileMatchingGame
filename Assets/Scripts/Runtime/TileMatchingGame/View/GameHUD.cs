using Assets.Scripts.Runtime.TileMatchingGame.Controller;
using Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.DI;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.View
{
    public class GameHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private GameManager _gameManager;
        private IScoreManager _scoreManager;

        void Start()
        {
            _gameManager = DIContainer.Instance.Resolve<GameManager>();
            _scoreManager = DIContainer.Instance.Resolve<IScoreManager>();

            _scoreManager.OnScoreChanged += UpdateScoreDisplay;

            UpdateScoreDisplay(_scoreManager.CurrentScore);
        }

        private void UpdateScoreDisplay(int newScore)
        {
            _scoreText.text = $"Score: {newScore}";
        }
    }
}