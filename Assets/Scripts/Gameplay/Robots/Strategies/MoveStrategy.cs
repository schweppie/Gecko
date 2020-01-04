using Gameplay.Robots.Commands;

namespace Gameplay.Robots.Strategies
{
    public class MoveStrategy : RobotCommandStrategy
    {
        public MoveStrategy(Robot robot) : base(robot)
        {
        }

        public override int GetPriority()
        {
            return 5;
        }

        public override bool IsApplicable()
        {
            return true;
        }

        public override RobotCommand GetCommand()
        {
            return new MoveRobotCommand();
        }
    }
}
