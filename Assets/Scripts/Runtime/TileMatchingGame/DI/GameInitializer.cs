using Assets.Scripts.Runtime.TileMatchingGame.Controller;
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

        void Start()
        {
            DIContainer.Instance.Register<IBoard, Board>(DIContainer.RegistrationType.Singleton, () => new Board(5, 5));
            DIContainer.Instance.Register<ITileFactory, TileFactory>(DIContainer.RegistrationType.Singleton, () => new TileFactory(tileFlyweights));

            CanvasAdapter canvasAdapter = new CanvasAdapter(DIContainer.Instance.Resolve<IBoard>(), Camera.main, _boardFrameTransform);
            TileViewPool tileViewPool = new TileViewPool(_tilePrefab, _tilesParent);
            BoardFiller boardFiller = new BoardFiller(DIContainer.Instance.Resolve<IBoard>(), DIContainer.Instance.Resolve<ITileFactory>(), tileViewPool, canvasAdapter);
            GameManager gameManager = new GameManager(boardFiller);
            _gameplayController = new GameplayController(gameManager);

            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, canvasAdapter);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, tileViewPool);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, boardFiller);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, gameManager);
            DIContainer.Instance.Register(DIContainer.RegistrationType.Singleton, _gameplayController);

            tileViewPool.PrePopulate(10);
            gameManager.StartGame();
        }

        private void Update()
        {
            _gameplayController.ObserveClickHandler();
        }
    }
}