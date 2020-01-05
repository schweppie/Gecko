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
            Tile tile = FieldController.Instance.GetTileAtIntPosition(robot.Position + robot.Direction);
            directionTile = tile.GetComponent<DirectionTileComponent>();

            return directionTile != null;
        }

        public override RobotCommand GetCommand()
        {
            return new ChangeDirectionCommand(directionTile.GetDirection());
        }
    }
}
