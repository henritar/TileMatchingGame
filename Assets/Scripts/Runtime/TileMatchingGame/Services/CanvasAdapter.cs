using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.View;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Services
{
    public class CanvasAdapter
    {
        private readonly IBoard _board;
        private readonly Camera _mainCamera;
        private readonly RectTransform _frameRectTransform;
        private Vector2 _cellSize;

        public CanvasAdapter(IBoard board, Camera mainCamera, RectTransform frameRectTransform)
        {
            _board = board;
            _mainCamera = mainCamera;
            _frameRectTransform = frameRectTransform;
            OnBoardUpdateHandler();
            _board.OnBoardUpdate += OnBoardUpdateHandler;
        }

        public Vector3[] GetBoardWorldCorners()
        {
            Vector3[] frameCorners = new Vector3[4];
            _frameRectTransform.GetWorldCorners(frameCorners);

            Vector2[] screenCorners = new Vector2[4];
            for (int i = 0; i < 4; i++)
            {
                screenCorners[i] = RectTransformUtility.WorldToScreenPoint(_mainCamera, frameCorners[i]);
            }

            Vector3[] worldCorners = new Vector3[4];
            for (int i = 0; i < 4; i++)
            {
                worldCorners[i] = _mainCamera.ScreenToWorldPoint(new Vector3(screenCorners[i].x, screenCorners[i].y, _mainCamera.nearClipPlane));
            }
            return worldCorners;

        }

        public void OnBoardUpdateHandler()
        {
            GetCellSize();
        }

        public Vector2 GetBoardWorldSize()
        {
            Vector3[] corners = GetBoardWorldCorners();
            return new Vector2(corners[2].x - corners[0].x, corners[2].y - corners[0].y);
        }

        public Vector3 GetBoardWorldOrigin()
        {
            return GetBoardWorldCorners()[0];
        }

        public Vector2 GetCellSize()
        {
            Vector2 boardWorldSize = GetBoardWorldSize();
            _cellSize = new Vector2(boardWorldSize.x / _board.Width, boardWorldSize.y / _board.Height);
            return _cellSize;
        }

        public void PositionTileView(TileView tileView)
        {
            tileView.transform.position = GetTileViewPosition(tileView);
        }

        public Vector3 GetTileViewScale(TileView tileView)
        {
            Vector3 spriteSize = tileView.OriginalSpriteBoundsSize;
            float scaleX = _cellSize.x / spriteSize.x;
            float scaleY = _cellSize.y / spriteSize.y;

            return new Vector3(scaleX, scaleY, 1);
        }

        public Vector3 GetTileViewPosition(TileView tileView)
        {
            return GetTileViewPosition(tileView.Tile.Row, tileView.Tile.Column);
        }

        public Vector3 GetTileViewPosition(int row, int col)
        {
            Vector3 worldPosition = BoardLayoutCalculator.CalculateWorldPosition(
                row,
                col,
                GetBoardWorldOrigin(),
                _cellSize
            );

            return worldPosition;
        }
    }
}