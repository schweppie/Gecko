using Gameplay.Field;
using Gameplay.Robots.Commands;
using Gameplay.Tiles;
using Gameplay.Tiles.Components;

namespace Gameplay.Robots.Strategies
{
    public class ChangeDirectionStrategy : RobotCommandStrategy
    {
        private DirectionTileComponent directionTile;
        
        public ChangeDirectionStrategy(Robot robot) : base(robot)
        {
        }

        public override int GetPriority()
        {
            return 5;
        }

        public override bool IsApplicable()
        {
            Tile tile = FieldController.Instance.GetTileAtIntPosition(robot.Position);
            directionTile = tile.GetComponent<DirectionTileComponent>();

            if (directionTile == null)
                return false;

            if (robot.Direction == directionTile.GetDirection())
                return false;

            IOccupier otherOccupier = FieldController.Instance.GetOccupierAt(robot.Position);

            if (otherOccupier != null)
                commandComponent.AddInvalidOccupier(otherOccupier);

            return true;
        }

        public override RobotCommand GetCommand()
        {
            return new ChangeDirectionCommand(directionTile.GetDirection());
        }
    }
}
