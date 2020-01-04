using Gameplay.Robots.Commands;
using Gameplay.Tiles.Components;

namespace Gameplay.Robots.Strategies
{
    public class CollectStrategy : RobotCommandStrategy
    {
        public CollectStrategy(Robot robot) : base(robot)
        {
        }

        public override int GetPriority()
        {
            return 3;
        }

        public override bool IsApplicable()
        {
            return robot.Tile.GetComponent<CollectorTileComponent>() != null;
        }

        public override RobotCommand GetCommand()
        {
            return new CollectRobotCommand();
        }
    }
}