using Gameplay.Robots;
using UnityEngine;

namespace Gameplay.Tiles.Reporters
{
    public class CurveHeightReporter : BaseHeightReporter
    {
        private AnimationCurve heightCurve;

        private Vector3 centerOffset = new Vector3(-0.5f, 0f, -0.5f);

        public CurveHeightReporter(AnimationCurve heightCurve, Tile tile) : base(tile)
        {
            this.heightCurve = heightCurve;
        }

        public override float GetValue(Robot robot, float t)
        {
            Vector3 position = robot.RobotVisual.transform.position - tile.IntPosition - centerOffset;

            float distance;
            float zDot = Vector3.Dot(Vector3.forward, tile.Visual.transform.forward);

            if (Mathf.Abs(zDot) > 0.5f)
            {
                if (zDot < 0f)
                    distance = position.z;
                else
                    distance = 1f - position.z;
            }
            else
            {
                float xDot = Vector3.Dot(Vector3.right, tile.Visual.transform.right);
                if (xDot < 0f)
                    distance = position.x;
                else
                    distance = 1f - position.x;
            }

            return tile.IntPosition.y + heightCurve.Evaluate(distance);
        }
    }
}
