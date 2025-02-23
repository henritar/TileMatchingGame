using Assets.Scripts.Runtime.TileMatchingGame.Model;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Runtime.TileMatchingGame.Services.Interfaces
{
    public interface IMatchFinder
    {
        public List<Tile> FindMatches(Tile startTile);
    }
}