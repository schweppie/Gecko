using Gameplay.Robots;
using UnityEngine;

namespace Gameplay.Tiles.Reporters.Height
{
    public class SlopeHeightReporter : BaseHeightReporter
    {
        public SlopeHeightReporter(Tile tile) : base(tile)
        {
        }

        public override float GetValue(Robot robot)
        {
            return tile.IntPosition.y + 1f - GetRobotDistanceOnTile(robot);
        }
    }
}
