using Assets.Scripts.Runtime.TileMatchingGame.Controller;
using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.TileMatchingGame.Services
{
    public class LevelButtonFactory : MonoBehaviour
    {
        public RectTransform _levelButtonParent;
        public Button _levelButtonPrefab;
        public LevelButtonFactory(Button buttonPrefab, RectTransform levelButtonParent)
        {
            _levelButtonPrefab = buttonPrefab;
            _levelButtonParent = levelButtonParent;
        }

        public void CreateButton(Level level)
        {
            Button newButton = Instantiate(_levelButtonPrefab, _levelButtonParent);
            TMP_Text text = newButton.GetComponentInChildren<TMP_Text>();
            var levelNumber = Regex.Match(level.name, @"\d+").Value;
            text.text = levelNumber;
            newButton.onClick.AddListener(() => LevelButtonClickHandler(int.Parse(levelNumber)));
        }

        public void LevelButtonClickHandler(int levelIndex)
        {
            var levelManager = DIContainer.Instance.Resolve<LevelManager>();
            levelManager.SetLevel(levelIndex - 1);
            levelManager.LoadLevel();
        }
    }
}