using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.Services.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.View;

namespace Assets.Scripts.Runtime.TileMatchingGame.Services
{
    public class BoardFiller
    {
        private readonly IBoard _board;
        private readonly ITileFactory _tileFactory;
        private readonly TileViewPool _tileViewPool;
        private readonly CanvasAdapter _canvasAdapter;

        public BoardFiller()
        {
            _board = DIContainer.Instance.Resolve<IBoard>();
            _tileFactory = DIContainer.Instance.Resolve<ITileFactory>();
            _tileViewPool = DIContainer.Instance.Resolve<TileViewPool>();
            _canvasAdapter = DIContainer.Instance.Resolve<CanvasAdapter>();
        }

        public BoardFiller(IBoard board, ITileFactory tileFactory, TileViewPool tileViewPool, CanvasAdapter canvasAdapter)
        {
            _board = board;
            _tileFactory = tileFactory;
            _tileViewPool = tileViewPool;
            _canvasAdapter = canvasAdapter;
        }
        public void FillEmptySpaces()
        {
            for (int col = 0; col < _board.Width; col++)
            {
                for (int row = 0; row < _board.Height; row++)
                {
                    if (_board.GetTileAt(row, col) == null)
                    {
                        Tile newTile = _tileFactory.CreateTile(row, col);

                        _board.SetTileAt(row, col, newTile);

                        TileView tileView = _tileViewPool.GetTileView();

                        tileView.Initialize(newTile);

                        _canvasAdapter.PositionTileView(tileView, row, col);
                    }
                }
            }
        }
    }
}