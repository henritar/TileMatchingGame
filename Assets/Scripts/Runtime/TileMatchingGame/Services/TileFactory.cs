using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.ScriptableObjects;
using Assets.Scripts.Runtime.TileMatchingGame.Services.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Services
{
    public class TileFactory : ITileFactory
    {
        private readonly TileFlyweight[] _tileDatas;

        public TileFactory(TileFlyweight[] tileDatas)
        {
            _tileDatas = tileDatas;
        }

        public Tile CreateTile(int row, int col)
        {
            TileFlyweight data = _tileDatas[Random.Range(0, _tileDatas.Length)];
            return new Tile(data, row, col);
        }
    }
}