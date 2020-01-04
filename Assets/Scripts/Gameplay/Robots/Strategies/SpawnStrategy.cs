using Gameplay.Robots.Commands;
using Gameplay.Robots.Components;

namespace Gameplay.Robots.Strategies
{
    public class SpawnStrategy : RobotCommandStrategy
    {
        public SpawnStrategy(Robot robot) : base(robot)
        {
        }

        public override int GetPriority()
        {
            return 1;
        }

        public override bool IsApplicable()
        {
            return commandComponent.Commands.Count == 0;
        }

        public override RobotCommand GetCommand()
        {
            return new SpawnRobotCommand();
        }
    }
}
