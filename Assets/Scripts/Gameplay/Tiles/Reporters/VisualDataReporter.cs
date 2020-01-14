using Gameplay.Robots;
using UnityEngine;

namespace Gameplay.Tiles.Reporters
{
    public abstract class VisualDataReporter<T>
    {
        protected Tile tile;

        public VisualDataReporter(Tile tile)
        {
            this.tile = tile;
        }

        public abstract T GetValue(Robot robot, float t);
    }
}
