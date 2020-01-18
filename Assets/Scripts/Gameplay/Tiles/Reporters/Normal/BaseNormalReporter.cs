using UnityEngine;

namespace Gameplay.Tiles.Reporters.Normal
{
    public abstract class BaseNormalReporter : DataReporter<Vector3>
    {
        public BaseNormalReporter(Tile tile) : base(tile)
        {
        }
    }
}
