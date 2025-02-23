using Assets.Scripts.Runtime.TileMatchingGame.Controller;
using Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates;
using Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects;
using Assets.Scripts.Runtime.TileMatchingGame.Services;
using Assets.Scripts.Runtime.TileMatchingGame.Services.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.View;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.DI
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private TileFlyweight[] tileFlyweights; 
        [SerializeField] private Level[] levelData; 
        [SerializeField] private GameHUD _gameHudView;
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private Transform _tilesParent;
        [SerializeField] private RectTransform _boardFrameTransform;
        [SerializeField] private RectTransform _pauseView;
        [SerializeField] private RectTransform _victoryView;
        [SerializeField] private RectTransform _gameOverView;
        [SerializeField] private RectTransform _goalsView;

        private GameplayController _gameplayController;

        void Awake()
        {
            //Registering interfaces
            DIContainer.Instance.Register<IBoard, Board>(DIContainer.RegistrationType.Singleton, () => new Board(5, 5));
            DIContainer.Instance.Register<ITileFactory, TileFactory>(DIContainer.RegistrationType.Singleton, () => new TileFactory(tileFlyweights));
            DIContainer.Instance.Register<IMatchFinder, DFSMatchFinder>(DIContainer.RegistrationType.Singleton, () => new DFSMatchFinder());
            DIContainer.Instance.Register<IScoreManager, ScoreManager>(DIContainer.RegistrationType.Singleton, () => new ScoreManager());

            //Instanciating
            CanvasAdapter canvasAdapter = new CanvasAdapter(DIContainer.Instance.Resolve<IBoard>(), Camera.main, _boardFrameTransform);
            LevelManager levelManager = new LevelManager(levelData.ToList());
            TileViewPool tileViewPool = new TileViewPool(_tilePrefab, _tilesParent, canvasAdapter);
            BoardFiller boardFiller = new BoardFiller(DIContainer.Instance.Resolve<IBoard>(), DIContainer.Instance.Resolve<ITileFactory>(), tileViewPool, canvasAdapter);
            BoardModifier boardModifier = new BoardModifier(DIContainer.Instance.Resolve<IBoard>(), boardFiller);
            GameManager gameManager = new GameManager(DIContainer.Instance.Resolve<IBoard>(), DIContainer.Instance.Resolve<IMatchFinder>(), boardModifier, DIContainer.Instance.Resolve<IScoreManager>());
            _gameplayController = new GameplayController(gameManager);

            //GameStates
            IGameState[] gameStates = new IGameState[] { new PlayingState(gameManager), new PauseState(_pauseView), new VictoryState(levelManager, _victoryView), new GameOverState(_gameOverView), new ShowGoalsState(_goalsView) };
            IGoal[] levelGoals = new IGoal[] { new CollectTilesPointsGoal(), new MaxMovesGoal()};


            //Registering
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, canvasAdapter);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, levelManager);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, tileViewPool);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, boardFiller);
            DIContainer.Instance.Register<IBoardModifier, BoardModifier>(DIContainer.RegistrationType.Singleton, () => boardModifier);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, gameManager);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, _gameplayController);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, _gameHudView);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, gameStates);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, levelGoals);
        }

        private void Start()
        {
            var tileViewPool = DIContainer.Instance.Resolve<TileViewPool>();
            var levelManager = DIContainer.Instance.Resolve<LevelManager>();

            tileViewPool.SetBoard();
            tileViewPool.PrePopulate(10);
            levelManager.LoadLevel();
        }

        private void Update()
        {
            _gameplayController.ObserveClickHandler();
        }
    }
}