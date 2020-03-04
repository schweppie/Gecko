using Gameplay.Field;
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
            return 9;
        }

        public override bool IsApplicable()
        {
            IOccupier otherOccupier = FieldController.Instance.GetOccupierAt(robot.Position);

            if (otherOccupier != null)
                commandComponent.AddInvalidOccupier(otherOccupier);

            return true;
        }

        public override RobotCommand GetCommand()
        {
            return new WaitCommand();
        }
    }
}
