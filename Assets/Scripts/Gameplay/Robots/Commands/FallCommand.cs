using Gameplay.Field;
using UnityEngine;

namespace Gameplay.Robots.Commands
{
    public class FallCommand : RobotCommand
    {
        public override void Execute()
        {
            robot.Move(Vector3Int.down);
        }

        public override void Undo()
        {
            robot.Move(Vector3Int.up);
        }

        public override void Prewarm()
        {
            FieldController.Instance.GetTileAtIntPosition(robot.Position).ReleaseOccupier(robot);
        }
    }
}
