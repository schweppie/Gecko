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
            var occupationBuffer = GameStepController.Instance.OccupationBuffer;
            var oldOccupationBuffer = GameStepController.Instance.OldOccupationBuffer;

            if (!occupationBuffer.ContainsKey(robot.Position + robot.Direction))
            {
                if(oldOccupationBuffer.ContainsKey(robot.Position + robot.Direction))
                {
                    IOccupier oldOccupier = oldOccupationBuffer[robot.Position + robot.Direction];
                    IOccupier currentOccupier = occupationBuffer[robot.Position];

                    if (oldOccupier == currentOccupier)
                    {
                        oldOccupier.PickNewStrategy();
                        return false;
                    }
                }
            }

            return true;
        }

        public override RobotCommand GetCommand()
        {
            return new MoveCommand();
        }
    }
}
