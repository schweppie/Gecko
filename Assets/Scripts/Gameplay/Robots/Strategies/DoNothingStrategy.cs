using System.Linq;
using Gameplay.Robots.Commands;

namespace Gameplay.Robots.Strategies
{
    public class DoNothingStrategy : RobotCommandStrategy
    {
        public DoNothingStrategy(Robot robot) : base(robot)
        {
        }

        public override int GetPriority()
        {
            return 2;
        }

        public override bool IsApplicable()
        {
            return commandComponent.Commands.Last().GetType() == typeof(CollectRobotCommand)
                    || commandComponent.Commands.Last().GetType() == typeof(DoNothingCommand);
        }

        public override RobotCommand GetCommand()
        {
            return new DoNothingCommand();
        }
    }
}
