using Gameplay.Robots;
using UnityEngine;

namespace Gameplay.Tiles.Reporters.Enter
{
    public class SlopeEnterReporter : BaseEnterReporter
    {
        public SlopeEnterReporter(Tile tile) : base(tile)
        {
        }

        public override Vector3Int GetValue(Robot robot)
        {
            Vector3Int heightOffset = Vector3Int.zero;

            if (robot.Direction == tile.Visual.transform.forward)
                heightOffset.y = 1;

            return tile.IntPosition + heightOffset;
        }
    }
}
