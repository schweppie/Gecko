using UnityEngine;

namespace Gameplay.Tiles.Reporters.Exit
{
    public abstract class BaseExitReporter : DataReporter<Vector3Int>
    {
        public BaseExitReporter(Tile tile) : base(tile)
        {
        }
    }
}
