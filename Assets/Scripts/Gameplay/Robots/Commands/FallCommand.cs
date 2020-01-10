using UnityEngine;

namespace Gameplay.Robots.Commands
{
    public class FallCommand : RobotCommand
    {
        public override void Execute()
        {
            robot.Move(Vector3Int.down);
            GameStepController.Instance.AddOccupier(robot.Position, robot);
        }

        public override void Undo()
        {
            robot.Move(Vector3Int.up);
        }
    }
}
