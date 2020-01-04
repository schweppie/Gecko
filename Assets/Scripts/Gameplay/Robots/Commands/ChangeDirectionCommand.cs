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
            robot.SetDirection(direction);
            GameStepController.Instance.PopulatePositionBuffer(robot.Position);
        }

        public override void Undo()
        {
            robot.SetDirection(oldDirection);
        }
    }
}
