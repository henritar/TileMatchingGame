using Assets.Scripts.Runtime.TileMatchingGame.DI;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.View;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Services
{
    public class TileViewPool : IDisposable
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private Transform _parent;
        private Stack<TileView> _pool = new Stack<TileView>();
        private Dictionary<Tile, TileView> _tileViewMap = new Dictionary<Tile, TileView>();

        private CanvasAdapter _canvasAdapter;
        private IBoard _board;

        private int _initialSize;

        public TileViewPool(GameObject tilePrefab, Transform parent)
        {
            _tilePrefab = tilePrefab;
            _parent = parent;
        }
        
        public void SetBoard(IBoard board = null)
        {
            _board = board ?? DIContainer.Instance.Resolve<IBoard>();
            _board.OnTileRemoved += HandleTileRemoved;
            _board.OnTileFalling += HandleTileRemoved;
        }

        public void PrePopulate(int initialSize)
        {
            _initialSize = initialSize;
            for (int i = 0; i < initialSize; i++)
            {
                TileView tileView = InstantiateTile(i);
                tileView.gameObject.SetActive(false);
                _pool.Push(tileView);
            }
        }
        
        public TileView GetTileView(Tile tile)
        {
            if (_pool.Count > 0)
            {
                TileView tileView = _pool.Pop();
                tileView.gameObject.SetActive(true);
                _tileViewMap[tile] = tileView;
                return tileView;
            }
            else
            {
                TileView tileView = InstantiateTile(tile.Id);
                _tileViewMap[tile] = tileView;
                return tileView;
            }
        }

        public void ReturnTileView(TileView tileView)
        {
            tileView.gameObject.SetActive(false);
            _pool.Push(tileView);
        }

        private void HandleTileRemoved(Tile tile)
        {
            if (_tileViewMap.TryGetValue(tile, out TileView tileView))
            {
                _tileViewMap.Remove(tile);
                ReturnTileView(tileView);
            }
        }

        private void HandleTileFalling(Tile tile)
        {
            if (_tileViewMap.TryGetValue(tile, out TileView tileView))
            {
                //tileView.OnTilePositionUpdated();
            }
        }

        private TileView InstantiateTile(int i)
        {
            GameObject instance = UnityEngine.Object.Instantiate(_tilePrefab, _parent);
            instance.name = $"Tile-{i}";
            TileView tileView = instance.GetComponent<TileView>();
            return tileView;
        }


        void IDisposable.Dispose()
        {
            if (_board != null)
            {
                _board.OnTileRemoved -= HandleTileRemoved;
            }
        }
    }
}