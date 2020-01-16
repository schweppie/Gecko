using Gameplay.Robots;
using Gameplay.Tiles.Components;
using UnityEngine;

namespace Gameplay.Tiles.Reporters.Exit
{
    public abstract class BaseExitReporter : DataReporter<Vector3Int>
    {
        public BaseExitReporter(Tile tile) : base(tile)
        {
        }

        private Vector3Int CorrectForSlopesBelowExit(Vector3Int exitPosition)
        {
            // If our exit position is 'in air' check if belows tile is a slope
            Tile exitTile = FieldController.Instance.GetTileAtIntPosition(exitPosition);
            if (exitTile.GetComponent<EmptyTileComponent>() != null)
            {
                Tile slopeTile = FieldController.Instance.GetTileAtIntPosition(exitPosition + Vector3Int.down);

                if (slopeTile.GetComponent<SlopeTileComponent>() != null)
                {
                    Debug.DrawLine(exitPosition, exitPosition + Vector3Int.down, Color.magenta, 10f);
                    return slopeTile.IntPosition;
                }
            }

            Debug.DrawLine(tile.IntPosition, exitPosition, Color.green, 10f);

            return exitPosition;
        }

        /// <summary>
        /// Returns the exit position corrected if slopes are below it
        /// This means that if the non-corrected exit is on a empty tile, check if there
        /// is a slope below. If this is the case, return the position of the slope tile
        /// </summary>
        /// <param name="robot"></param>
        /// <returns></returns>
        public Vector3Int GetCorrectedExit(Robot robot)
        {
            return CorrectForSlopesBelowExit(GetValue(robot));
        }
    }
}
