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
            return 6;
        }

        public override bool IsApplicable()
        {
            return !GameStepController.Instance.IsPositionBlocked(robot.Position + robot.Direction);
        }

        public override RobotCommand GetCommand()
        {
            return new MoveCommand();
        }
    }
}
