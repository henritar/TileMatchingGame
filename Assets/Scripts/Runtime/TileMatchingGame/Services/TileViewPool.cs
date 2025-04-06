using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.View;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Services
{
    public class TileViewPool : IDisposable
    {
        private readonly GameObject _tilePrefab;
        private readonly Transform _parent;
        private readonly CanvasAdapter _canvasAdapter;

        private Stack<TileView> _pool = new Stack<TileView>();
        private Dictionary<Tile, TileView> _tileViewMap = new Dictionary<Tile, TileView>();

        private IBoard _board;

        private int _initialSize;

        public TileViewPool(IBoard board, GameObject tilePrefab, Transform parent, CanvasAdapter canvasAdapter)
        {
            _board = board;
            _tilePrefab = tilePrefab;
            _parent = parent;
            _canvasAdapter = canvasAdapter;
        }
        
        public void SetBoard(IBoard board)
        {
            _board.OnTileRemoved += HandleTileRemoved;
            _board.OnTileFalling += HandleTileFalling;
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
            TileView tileView;
            if (_pool.Count > 0)
            {
                tileView = _pool.Pop();
                tileView.gameObject.SetActive(true);
                _tileViewMap[tile] = tileView;
            }
            else
            {
                tileView = InstantiateTile(tile.Id);
                _tileViewMap[tile] = tileView;
            }
            tileView.transform.position = _canvasAdapter.GetTileViewPosition(_board.Height + _board.Height, tile.Column);
            tileView.transform.localScale = _canvasAdapter.GetTileViewScale(tileView);
            return tileView;
        }

        public void ReturnTileView(TileView tileView)
        {
            tileView.gameObject.SetActive(false);
            _pool.Push(tileView);
        }

        public void ReturnAllTilesView()
        {
            _tileViewMap.Values.ToList().ForEach(view => ReturnTileView(view));
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
                var newPosition = _canvasAdapter.GetTileViewPosition(tileView);
                tileView.OnTilePositionUpdated(newPosition);
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
                _board.OnTileFalling -= HandleTileFalling;
            }
        }
    }
}