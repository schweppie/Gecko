using Gameplay.Robots;
using UnityEngine;

namespace Gameplay.Tiles.Reporters.Enter
{
    public class DefaultEnterReporter : BaseEnterReporter
    {
        public DefaultEnterReporter(Tile tile) : base(tile)
        {
        }

        public override Vector3Int GetValue(Robot robot)
        {
            return tile.IntPosition;
        }
    }
}
