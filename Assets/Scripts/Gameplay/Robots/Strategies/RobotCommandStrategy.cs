using Gameplay.Robots.Commands;
using Gameplay.Robots.Components;
using JP.Framework.Strategy;
using UnityEngine;

namespace Gameplay.Robots.Strategies
{
    public abstract class RobotCommandStrategy : IStrategy
    {
        public abstract int GetPriority();
        public abstract bool IsApplicable();

        public abstract RobotCommand GetCommand();
        
        protected Robot robot;
        protected RobotCommandComponent commandComponent;

        public RobotCommandStrategy(Robot robot)
        {
            this.robot = robot;
            commandComponent = robot.GetComponent<RobotCommandComponent>();
        }

        public virtual Vector3Int GetMoveToPositionIntention()
        {
            return robot.Position;
        }
    }
}
