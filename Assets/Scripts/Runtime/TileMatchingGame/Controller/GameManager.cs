using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Services;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller
{
    public class GameManager
    {
        private BoardFiller _boardFiller;

        public GameManager()
        {
            _boardFiller = DIContainer.Instance.Resolve<BoardFiller>();
        }

        public GameManager(BoardFiller boardFiller)
        {
            _boardFiller = boardFiller;
        }

        public void StartGame()
        {
            _boardFiller.FillEmptySpaces();
        }

        public void HandleTileClick(Tile tile)
        {
        }
    }
}
