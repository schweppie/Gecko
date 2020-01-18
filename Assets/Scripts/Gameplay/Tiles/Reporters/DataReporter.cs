using Gameplay.Robots;
using UnityEngine;

namespace Gameplay.Tiles.Reporters
{
    public abstract class DataReporter<T>
    {
        protected Tile tile;

        public DataReporter(Tile tile)
        {
            this.tile = tile;
        }

        public abstract T GetValue(Robot robot);
    }
}
