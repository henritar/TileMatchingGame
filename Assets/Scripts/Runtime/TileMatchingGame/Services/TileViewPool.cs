using Assets.Scripts.Runtime.TileMatchingGame.Model;
using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Services
{
    public class TileViewPool
    {
        [SerializeField] private GameObject _tilePrefab;
        [SerializeField] private Transform _parent;
        private Stack<TileView> _pool = new Stack<TileView>();

        private CanvasAdapter _canvasAdapter;

        private int _initialSize;

        public TileViewPool(GameObject tilePrefab, Transform parent)
        {
            _tilePrefab = tilePrefab;
            _parent = parent;
        }

        public void PrePopulate(int initialSize)
        {
            _initialSize = initialSize;
            for (int i = 0; i < initialSize; i++)
            {
                GameObject instance = Object.Instantiate(_tilePrefab, _parent);
                TileView tileView = instance.GetComponent<TileView>();
                tileView.gameObject.SetActive(false);
                _pool.Push(tileView);
            }
        }

        public TileView GetTileView()
        {
            if (_pool.Count > 0)
            {
                TileView tileView = _pool.Pop();
                tileView.gameObject.SetActive(true);
                return tileView;
            }
            else
            {
                GameObject instance = Object.Instantiate(_tilePrefab, _parent);
                TileView tileView = instance.GetComponent<TileView>();
                return tileView;
            }
        }

        public void ReturnTileView(TileView tileView)
        {
            tileView.gameObject.SetActive(false);
            _pool.Push(tileView);
        }
    }
}