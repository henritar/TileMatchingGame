using Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces;
using Assets.Scripts.Runtime.TileMatchingGame.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.GameStates
{
    public class PlayingState : IGameState
    {
        private GameManager _gameManager;
        private RectTransform _startScreenView;
        private ISoundManager _soundManager;

        public GameStateEnum State => GameStateEnum.Playing;

        public PlayingState(GameManager gameManager, RectTransform startScreenView, ISoundManager soundManager)
        {
            _gameManager = gameManager;
            _startScreenView = startScreenView;
            _soundManager = soundManager;
        }


        public void Enter()
        {
            _startScreenView.gameObject.SetActive(false);
            _gameManager.RefillBoard();
            _soundManager.PlayMusic(AppConstants.RetroArcadeMusic);
        }

        public void Exit()
        {
            _soundManager.StopMusic();
        }

        public void HandleTileClick(Tile tile)
        {
            
            List<Tile> matchedTiles = _gameManager.MatchFinder.FindMatches(tile);
            if (matchedTiles.Count >= 2)
            {
                _gameManager.OnMatchedTiles(matchedTiles);
            }
        }
    }
}