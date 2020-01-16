using Gameplay.Robots;
using UnityEngine;

namespace Gameplay.Tiles.Reporters.Height
{
    public abstract class BaseHeightReporter : DataReporter<float>
    {
        private Vector3 centerOffset = new Vector3(-0.5f, 0f, -0.5f);

        public BaseHeightReporter(Tile tile) : base(tile)
        {
        }

        protected float GetRobotDistanceOnTile(Robot robot)
        {
            Vector3 position = robot.RobotVisual.transform.position - tile.IntPosition - centerOffset;

            float distance;
            float zDot = Vector3.Dot(Vector3.forward, tile.Visual.transform.forward);

            if (Mathf.Abs(zDot) > 0.5f)
            {
                if (zDot > 0f)
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

            return distance;
        }
    }
}
