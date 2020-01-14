using Gameplay.Robots;
using UnityEngine;

namespace Gameplay.Tiles.Reporters
{
    public class DefaultHeightReporter : VisualDataReporter<float>
    {
        public DefaultHeightReporter(Tile tile) : base(tile)
        {
        }

        public override float GetValue(Robot robot, float t)
        {
            return tile.IntPosition.y;

            int robotY = robot.Position.y;
            int tileY = tile.IntPosition.y;
            int steps = robotY - tileY;

            if (steps == 0)
                return tile.IntPosition.y;

            float height = Mathf.Lerp(robotY, tileY, t * (1 / steps));

            return height;
        }
    }
}
