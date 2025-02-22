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

        public CanvasAdapter(IBoard board, Camera mainCamera, RectTransform frameRectTransform)
        {
            _board = board;
            _mainCamera = mainCamera;
            _frameRectTransform = frameRectTransform;
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

        public Vector2 GetBoardWorldSize()
        {
            Vector3[] corners = GetBoardWorldCorners();
            return new Vector2(corners[2].x - corners[0].x, corners[2].y - corners[0].y);
        }

        public Vector3 GetBoardWorldOrigin()
        {
            return GetBoardWorldCorners()[0];
        }

        public Vector2 GetCellSize(IBoard board)
        {
            Vector2 boardWorldSize = GetBoardWorldSize();
            return new Vector2(boardWorldSize.x / board.Width, boardWorldSize.y / board.Height);
        }

        public void PositionTileView(TileView tileView, int row, int col)
        {
            Vector2 cellSize = GetCellSize(_board);

            Vector3 spriteSize = tileView.SpriteRenderer.bounds.size;
            float scaleX = cellSize.x / spriteSize.x;
            float scaleY = cellSize.y / spriteSize.y;

            Vector3 worldPosition = BoardLayoutCalculator.CalculateWorldPosition(
                row,
                col,
                GetBoardWorldOrigin(),
                cellSize
            );

            tileView.transform.position = worldPosition;
            tileView.transform.localScale = new Vector3(scaleX, scaleY, 1);
        }
    }
}