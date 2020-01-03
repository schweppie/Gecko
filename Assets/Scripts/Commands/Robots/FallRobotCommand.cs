using UnityEngine;

namespace Commands.Robots
{
    public class FallRobotCommand : RobotCommand
    {
        public override void Execute()
        {
            robot.Move(Vector3Int.down);
        }

        public override void Undo()
        {
            robot.Move(Vector3Int.up);
        }
    }
}
