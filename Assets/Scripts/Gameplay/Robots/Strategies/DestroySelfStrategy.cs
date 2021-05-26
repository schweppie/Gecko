using Gameplay.Robots.Commands;

namespace Gameplay.Robots.Strategies
{
    public class DestroySelfStrategy : RobotCommandStrategy
    {
        public DestroySelfStrategy(Robot robot) : base(robot)
        {
        }

        public override int GetPriority()
        {
            return 4;
        }

        public override bool IsApplicable()
        {
            return robot.Position.y < 0;
        }

        public override RobotCommand GetCommand()
        {
            return new DestroySelfCommand();
        }
    }
}
