using Gameplay.Robots;
using UnityEngine;

namespace Gameplay.Tiles.Reporters.Normal
{
    public class SlopeNormalReporter : BaseNormalReporter
    {
        public SlopeNormalReporter(Tile tile) : base(tile)
        {
        }

        public override Vector3 GetValue(Robot robot)
        {
            return (tile.Visual.transform.forward + Vector3.up).normalized;
        }
    }
}
