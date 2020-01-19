using Gameplay.Field;
using UnityEngine;

namespace Gameplay.Robots.Commands
{
    public class FallCommand : RobotCommand
    {
        public override void Execute()
        {
            robot.Move(Vector3Int.down);
            FieldController.Instance.AddOccupier(robot.Position, robot);
        }
    }
}
