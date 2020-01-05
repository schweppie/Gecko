using Gameplay.Robots.Commands;
using UnityEngine;

namespace Gameplay.Robots.Strategies
{
    public class MoveStrategy : RobotCommandStrategy
    {
        private bool used = false;
        
        public MoveStrategy(Robot robot) : base(robot)
        {
        }

        public override int GetPriority()
        {
            return 6;
        }

        public override bool IsApplicable()
        {
            // TODO find better solution
            if (used)
                return false;
            
            used = true;
            return true;
        }

        public override RobotCommand GetCommand()
        {
            return new MoveCommand();
        }

        public override Vector3Int GetIntentTarget()
        {
            return robot.Position + robot.Direction;
        }

        public void ResetUsed()
        {
            used = false;
        }
    }
}
