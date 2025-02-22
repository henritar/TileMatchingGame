using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Services
{
    public static class BoardLayoutCalculator
    {
        public static Vector3 CalculateWorldPosition(int row, int col, Vector3 boardWorldOrigin, Vector2 cellSize)
        {
            return boardWorldOrigin + new Vector3(col * cellSize.x + cellSize.x * 0.5f, row * cellSize.y + cellSize.y * 0.5f, 0);
        }

        public static Vector2 GetCellSize(int boardWidth, int boardHeight, float boardWorldWidth, float boardWorldHeight)
        {
            return new Vector2(boardWorldWidth / boardWidth, boardWorldHeight / boardHeight);
        }
    }
}