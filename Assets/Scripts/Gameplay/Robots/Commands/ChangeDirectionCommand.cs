using Gameplay.Field;
using UnityEngine;

namespace Gameplay.Robots.Commands
{
    public class ChangeDirectionCommand : RobotCommand
    {
        private Vector3Int direction;
        private Vector3Int oldDirection;
        
        public ChangeDirectionCommand(Vector3Int direction)
        {
            this.direction = direction;
        }
        
        public override void Execute()
        {
            oldDirection = robot.Direction;
            robot.SetDirection(direction);
            FieldController.Instance.AddOccupier(robot.Position, robot);
        }
    }
}
