using Assets.Scripts.Runtime.TileMatchingGame.Model;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Services.Interfaces
{
    public interface ITileFactory
    {
        public Tile CreateTile(int row, int column);
    }
}