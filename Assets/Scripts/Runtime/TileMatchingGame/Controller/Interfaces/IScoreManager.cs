using Assets.Scripts.Runtime.TileMatchingGame.Model.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.TileMatchingGame.Controller.Interfaces
{
    public interface IScoreManager
    {
        public event Action<int> OnScoreChanged;
        void AddScore(int points);
        void ResetScore();
        int CurrentScore { get; }
    }
}