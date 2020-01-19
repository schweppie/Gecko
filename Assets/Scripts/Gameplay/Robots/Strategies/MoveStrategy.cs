using Gameplay.Field;
using Gameplay.Robots.Commands;
using Gameplay.Tiles;
using Gameplay.Tiles.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Robots.Strategies
{
    public class MoveStrategy : RobotCommandStrategy
    {
        private Dictionary<Vector3Int, IOccupier> occupationBuffer;
        private Dictionary<Vector3Int, IOccupier> oldOccupationBuffer;

        private Vector3Int targetPosition;

        public MoveStrategy(Robot robot) : base(robot)
        {
            occupationBuffer = FieldController.Instance.OccupationBuffer;
            oldOccupationBuffer = FieldController.Instance.OldOccupationBuffer;
        }

        public override int GetPriority()
        {
            return 7;
        }

        public override bool IsApplicable()
        {
            // Don't move while on empty tile (falling)
            if (robot.Tile.GetComponent<EmptyTileComponent>() != null)
                return false;

            var occupationBuffer = FieldController.Instance.OccupationBuffer;
            var oldOccupationBuffer = FieldController.Instance.OldOccupationBuffer;

            Vector3Int defaultExitPosition = robot.Tile.ExitReporter.GetValue(robot);
            Vector3Int correctedExitPosition = robot.Tile.ExitReporter.GetCorrectedExit(robot);

            targetPosition = correctedExitPosition;

            Tile targetTile = FieldController.Instance.GetTileAtIntPosition(targetPosition);

            // Check if we can enter the target tile's enter position
            // If the enter position is higher than our current exit, we can't enter
            Vector3Int enterPosition = targetTile.EnterReporter.GetValue(robot);
            if (enterPosition.y > targetPosition.y && defaultExitPosition.y == correctedExitPosition.y)
                return false;

            // If occupied, no move
            if (occupationBuffer.ContainsKey(targetPosition))
                return false;

            // Can only enter blocking direction tiles in same direction
            BlockingDirectionTileComponent blockingDirectionTile = targetTile.GetComponent<BlockingDirectionTileComponent>();
            if (blockingDirectionTile != null && !blockingDirectionTile.IsDirectionPerpendicular(robot.Direction))
                return false;

            // Check if target tile's vertical path is free
            if (!IsVerticalPathFree(targetPosition))
                return false;

            // Check old occupier of the tile this robot wants to move to, to avoid robots moving through each other
            if (oldOccupationBuffer.ContainsKey(targetPosition))
            {
                IOccupier oldOccupier = FieldController.Instance.GetOldOccupierAt(targetPosition);
                IOccupier currentOccupier = FieldController.Instance.GetOccupierAt(robot.Position);

                if (oldOccupier == currentOccupier)
                {
                    commandComponent.AddInvalidOccupier(oldOccupier);
                    return false;
                }
            }

            return true;
        }

        private bool IsVerticalPathFree(Vector3Int position)
        {
            Tile targetTile = FieldController.Instance.GetTileAtIntPosition(position);
            Tile tileAboveTarget = FieldController.Instance.GetTileAboveIntPosition(position);
            Tile tileBelowTarget = targetTile;

            // If targetTile is in mid air, find closest ground tile
            if (targetTile.GetComponent<EmptyTileComponent>() != null)
                tileBelowTarget = FieldController.Instance.GetTileBelowIntPosition(position);

            // No floor below? This means no more world.. we may always fall
            if (tileBelowTarget == null)
                return true;

            // Can't land on blocking tile
            if (tileBelowTarget.GetComponent<BlockingTileComponent>() != null)
                return false;

            // Can't land on incorrect direction blocking tile
            BlockingDirectionTileComponent blockingDirectionTile = tileBelowTarget.GetComponent<BlockingDirectionTileComponent>();
            if (blockingDirectionTile != null && !blockingDirectionTile.IsDirectionPerpendicular(robot.Direction))
                return false;

            // If no tile below or above, use bounds of field
            Vector3Int abovePosition = position;
            Vector3Int belowPosition = position;
            BoundsInt worldBounds = FieldController.Instance.Bounds;

            // Find hightest and lowest positions
            abovePosition.y = tileAboveTarget == null ? worldBounds.yMax + 1 : tileAboveTarget.IntPosition.y;
            belowPosition.y = tileBelowTarget == null ? worldBounds.yMin - 1 : tileBelowTarget.IntPosition.y;

            // Find occupiers between abovePosition and belowPosition
            //      Need to check if it is a Robot as well (BlockingTileComponents are IOccupiers)
            //      Maybe we should consider breaking up into static/dynamic occupation buffers?
            //
            //      Oh.. and it should probably be optimized..
            var newVerticalOccupiers = occupationBuffer.Where(o => o.Value.GetType() == typeof(Robot) && o.Key.x == position.x && o.Key.z == position.z);
            newVerticalOccupiers = newVerticalOccupiers.Where(o => o.Key.y >= belowPosition.y && o.Key.y < abovePosition.y);

            var oldVerticalOccupiers = oldOccupationBuffer.Where(o => o.Value.GetType() == typeof(Robot) && o.Key.x == position.x && o.Key.z == position.z);
            oldVerticalOccupiers = oldVerticalOccupiers.Where(o => o.Key.y >= belowPosition.y && o.Key.y < abovePosition.y);

            // No other occupiers, we can fall
            if (newVerticalOccupiers.Count() == 0 && oldVerticalOccupiers.Count() == 0)
                //|| oldVerticalOccupiers.Count() > 0 && newVerticalOccupiers.Count() == 0)
                return true;

            return false;
        }

        public override RobotCommand GetCommand()
        {
            // Provide the vertical delta, for when moving up/down slopes
            return new MoveCommand(targetPosition.y - robot.Position.y);
        }
    }
}
