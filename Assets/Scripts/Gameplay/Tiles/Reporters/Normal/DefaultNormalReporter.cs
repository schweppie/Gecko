using Gameplay.Robots;
using UnityEngine;

namespace Gameplay.Tiles.Reporters.Normal
{
    public class DefaultNormalReporter : BaseNormalReporter
    {
        public DefaultNormalReporter(Tile tile) : base(tile)
        {
        }

        public override Vector3 GetValue(Robot robot)
        {
            return Vector3.up;
        }
    }
}
