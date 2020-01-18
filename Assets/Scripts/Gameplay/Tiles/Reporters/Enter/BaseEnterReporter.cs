using Gameplay.Tiles.Components;
using UnityEngine;

namespace Gameplay.Tiles.Reporters.Enter
{
    public abstract class BaseEnterReporter : DataReporter<Vector3Int>
    {
        public BaseEnterReporter(Tile tile) : base(tile)
        {
        }
    }
}
