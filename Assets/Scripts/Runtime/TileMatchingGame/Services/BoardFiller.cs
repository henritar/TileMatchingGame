using Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.Services.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.View;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Services
{
    public class BoardFiller
    {
        private readonly IBoard _board;
        private readonly ITileFactory _tileFactory;
        private readonly TileViewPool _tileViewPool;
        private readonly CanvasAdapter _canvasAdapter;
        private readonly ISoundManager _soundManager;

        private WaitForSeconds _waitFor2Secs = new WaitForSeconds(0.3f);

        public BoardFiller()
        {
            _board = DIContainer.Instance.Resolve<IBoard>();
            _tileFactory = DIContainer.Instance.Resolve<ITileFactory>();
            _tileViewPool = DIContainer.Instance.Resolve<TileViewPool>();
            _canvasAdapter = DIContainer.Instance.Resolve<CanvasAdapter>();
            _soundManager = DIContainer.Instance.Resolve<ISoundManager>();
        }

        public BoardFiller(IBoard board, ITileFactory tileFactory, TileViewPool tileViewPool, CanvasAdapter canvasAdapter, ISoundManager soundManager)
        {
            _board = board;
            _tileFactory = tileFactory;
            _tileViewPool = tileViewPool;
            _canvasAdapter = canvasAdapter;
            _soundManager = soundManager;
        }

        public void StartFillEmptySpaces()
        {
            CoroutineRunner.Instance.StartCoroutine(FillEmptySpacesCoroutine());
        }
        public void FillEmptySpaces()
        {
            CoroutineRunner.Instance.StartCoroutine(FillEmptySpacesCoroutine(true));
        }

        public IEnumerator FillEmptySpacesCoroutine(bool shouldWait = false)
        {
            for (int col = 0; col < _board.Width; col++)
            {
                for (int row = 0; row < _board.Height; row++)
                {
                    if (_board.GetTileAt(row, col) == null)
                    {
                        Tile newTile = _tileFactory.CreateTile(row, col);

                        _board.SetTileAt(row, col, newTile);

                        TileView tileView = _tileViewPool.GetTileView(newTile);

                        tileView.Initialize(newTile);

                        tileView.OnTilePositionUpdated(_canvasAdapter.GetTileViewPosition(tileView));
                        _soundManager.PlaySound(AppConstants.TileFallingSound);
                    }
                }
                yield return shouldWait ? null : _waitFor2Secs;
            }
        }
    }
}