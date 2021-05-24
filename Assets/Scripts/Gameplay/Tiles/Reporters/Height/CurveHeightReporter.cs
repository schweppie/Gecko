using Gameplay.Robots;
using UnityEngine;

namespace Gameplay.Tiles.Reporters.Height
{
    public class CurveHeightReporter : BaseHeightReporter
    {
        private AnimationCurve heightCurve;

        public CurveHeightReporter(Tile tile, AnimationCurve heightCurve) : base(tile)
        {
            this.heightCurve = heightCurve;
        }

        public override float GetValue(Robot robot)
        {
            return tile.IntPosition.y + heightCurve.Evaluate(GetRobotDistanceOnTile(robot));
        }
    }
}
