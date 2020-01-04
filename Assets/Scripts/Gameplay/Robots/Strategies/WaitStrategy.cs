using Gameplay.Robots.Commands;

namespace Gameplay.Robots.Strategies
{
    public class WaitStrategy : RobotCommandStrategy
    {
        public WaitStrategy(Robot robot) : base(robot)
        {
        }

        public override int GetPriority()
        {
            return 7;
        }

        public override bool IsApplicable()
        {
            return true;
        }

        public override RobotCommand GetCommand()
        {
            return new WaitCommand();
        }
    }
}
