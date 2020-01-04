using Gameplay.Robots.Commands;
using Gameplay.Tiles.Components;

namespace Gameplay.Robots.Strategies
{
    public class FallStrategy : RobotCommandStrategy
    {
        public FallStrategy(Robot robot) : base(robot)
        {
        }

        public override int GetPriority()
        {
            return 4;
        }

        public override bool IsApplicable()
        {
            return robot.Tile.GetComponent<EmptyTileComponent>() != null;
        }

        public override RobotCommand GetCommand()
        {
            return new FallCommand();
        }
    }
}
