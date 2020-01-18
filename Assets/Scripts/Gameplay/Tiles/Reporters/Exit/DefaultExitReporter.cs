using Gameplay.Robots;
using UnityEngine;

namespace Gameplay.Tiles.Reporters.Exit
{
    public class DefaultExitReporter : BaseExitReporter
    {
        public DefaultExitReporter(Tile tile) : base(tile)
        {
        }

        public override Vector3Int GetValue(Robot robot)
        {
            return tile.IntPosition + robot.Direction;
        }
    }
}
