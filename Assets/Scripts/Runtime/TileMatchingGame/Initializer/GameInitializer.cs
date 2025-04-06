using Assets.Scripts.Runtime.TileMatchingGame.Controller;
using Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates;
using Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects;
using Assets.Scripts.Runtime.TileMatchingGame.Services;
using Assets.Scripts.Runtime.TileMatchingGame.Services.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.View;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.TileMatchingGame.Initializer
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private TileFlyweight[] tileFlyweights; 
        [SerializeField] private Level[] levelData;
        [SerializeField] private RectTransform _levelButtonParent;
        [SerializeField] private Button _levelButtonPrefab;
        [SerializeField] private GameHUD _gameHudView;
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private Transform _tilesParent;
        [SerializeField] private RectTransform _boardFrameTransform;
        [SerializeField] private RectTransform _startScreenView;
        [SerializeField] private RectTransform _pauseView;
        [SerializeField] private RectTransform _victoryView;
        [SerializeField] private RectTransform _gameOverView;
        [SerializeField] private RectTransform _goalsView;

        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;

        private IBoard _board;
        private IGoalManager _goalManager;
        private GameplayController _gameplayController;
        private TileViewPool _tileViewPool;
        private LevelManager _levelManager;
        private LevelButtonFactory _levelFactory;

        void Awake()
        {
            //Registering interfaces
            _board = new Board();
            ITileFactory tileFactory = new TileFactory(tileFlyweights);
            IMatchFinder matchFinder = new DFSMatchFinder(_board);
            IScoreManager scoreManager = new ScoreManager();
            ISoundManager soundManager = new SoundManager(_musicSource, _sfxSource);

            //Instanciating
            CanvasAdapter canvasAdapter = new CanvasAdapter(_board, Camera.main, _boardFrameTransform);
            _tileViewPool = new TileViewPool(_board, _tilePrefab, _tilesParent, canvasAdapter);
            BoardFiller boardFiller = new BoardFiller(_board, tileFactory, _tileViewPool, canvasAdapter, soundManager);
            BoardModifier boardModifier = new BoardModifier(_board, boardFiller, _tileViewPool);
            GameManager gameManager = new GameManager(_board, matchFinder, boardModifier, scoreManager, soundManager,
                InitializeGameStates(soundManager, scoreManager)
                );

            _levelFactory = new LevelButtonFactory(_levelManager, _levelButtonPrefab, _levelButtonParent);
            _gameplayController = new GameplayController(gameManager);
            _gameHudView.Initialize(gameManager, scoreManager, _goalManager, _levelManager);
        }

        private Func<GameManager, IGameState[]> InitializeGameStates(ISoundManager soundManager, IScoreManager scoreManager)
        {
            return gm =>
            {
                _goalManager = new GoalManager(gm, scoreManager, _board);
                _levelManager = new LevelManager(gm, _goalManager, _board, levelData.ToList());
                return new IGameState[]
                                { new PlayingState(gm, _startScreenView,soundManager), new PauseState(_pauseView),
                    new VictoryState(_levelManager, _victoryView,soundManager), new GameOverState(_gameOverView,
                    soundManager), new ShowGoalsState(_goalsView, _gameHudView) };
            };
        }

        private void Start()
        {
            foreach (var level in levelData)
            {
                _levelFactory.CreateButton(level);
            }

            _tileViewPool.SetBoard(_board);
        }

        private void Update()
        {
            _gameplayController.ObserveClickHandler();
        }
    }
}