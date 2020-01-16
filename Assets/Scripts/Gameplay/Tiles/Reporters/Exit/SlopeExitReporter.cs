using Gameplay.Robots;
using UnityEngine;

namespace Gameplay.Tiles.Reporters.Exit
{
    public class SlopeExitReporter : BaseExitReporter
    {
        public SlopeExitReporter(Tile tile) : base(tile)
        {
        }

        public override Vector3Int GetValue(Robot robot)
        {
            Vector3Int heightOffset = Vector3Int.zero;
            Vector3Int exitPosition;

            if (robot.Direction == -tile.Visual.transform.forward)
                heightOffset.y = 1;

            exitPosition = CorrectForSlopesBelowExit(tile.IntPosition + robot.Direction + heightOffset);

            Debug.DrawLine(tile.IntPosition, exitPosition, Color.green, 10f);

            return exitPosition;
        }
    }
}
