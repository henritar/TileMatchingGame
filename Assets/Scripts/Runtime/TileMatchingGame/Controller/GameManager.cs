using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects;
using Assets.Scripts.Runtime.TileMatchingGame.Services;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TileFlyweight[] tileFlyweights;
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Transform TilesParent;
        [SerializeField] private RectTransform BoardFrameTransform;
        private Board _board;
        private BoardFiller _boardFiller;

        private void Start()
        {
            _board = new Board(9, 9);

            TileFactory tileFactory = new TileFactory(tileFlyweights);
            CanvasAdapter canvasAdapter = new CanvasAdapter(_board, Camera.main, BoardFrameTransform);
            TileViewPool tileViewPool = new TileViewPool(tilePrefab, TilesParent);

            _boardFiller = new BoardFiller(_board, tileFactory, tileViewPool, canvasAdapter);
            _boardFiller.FillEmptySpaces();
        }
    }
}
