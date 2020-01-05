using Gameplay.Robots.Commands;
using UnityEngine;

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
            return true;
        }

        public override RobotCommand GetCommand()
        {
            return new MoveCommand();
        }

        public override Vector3Int GetMoveToPositionIntention()
        {
            return robot.Position + robot.Direction;
        }
    }
}
