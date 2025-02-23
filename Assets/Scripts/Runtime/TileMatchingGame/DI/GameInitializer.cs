using Assets.Scripts.Runtime.TileMatchingGame.Controller;
using Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates;
using Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects;
using Assets.Scripts.Runtime.TileMatchingGame.Services;
using Assets.Scripts.Runtime.TileMatchingGame.Services.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.DI
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private TileFlyweight[] tileFlyweights; 
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private Transform _tilesParent;
        [SerializeField] private RectTransform _boardFrameTransform;

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
            TileViewPool tileViewPool = new TileViewPool(_tilePrefab, _tilesParent, canvasAdapter);
            BoardFiller boardFiller = new BoardFiller(DIContainer.Instance.Resolve<IBoard>(), DIContainer.Instance.Resolve<ITileFactory>(), tileViewPool, canvasAdapter);
            BoardModifier boardModifier = new BoardModifier(DIContainer.Instance.Resolve<IBoard>(), boardFiller);
            GameManager gameManager = new GameManager(DIContainer.Instance.Resolve<IMatchFinder>(), boardModifier, DIContainer.Instance.Resolve<IScoreManager>());
            _gameplayController = new GameplayController(gameManager);

            //GameStates
            IGameState[] gameStates = new IGameState[] { new PlayingState(gameManager), new PauseState()};


            //Registering
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, canvasAdapter);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, tileViewPool);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, boardFiller);
            DIContainer.Instance.Register<IBoardModifier, BoardModifier>(DIContainer.RegistrationType.Singleton, () => boardModifier);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, gameManager);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, _gameplayController);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, gameStates);
        }

        private void Start()
        {
            var tileViewPool = DIContainer.Instance.Resolve<TileViewPool>();
            var gameManager = DIContainer.Instance.Resolve<GameManager>();

            tileViewPool.SetBoard();
            tileViewPool.PrePopulate(10);
            gameManager.StartGame();
        }

        private void Update()
        {
            _gameplayController.ObserveClickHandler();
        }
    }
}