using Gameplay.Field;
using Gameplay.Tiles;
using UnityEngine;

namespace Gameplay.Robots.Commands
{
    public class MoveCommand : RobotCommand
    {
        public override void Execute()
        {
            robot.Move(robot.Direction);
            Vector3Int nextPosition = robot.Position + robot.Direction;
            Tile nextTile = FieldController.Instance.GetTileAtIntPosition(nextPosition);
            //TODO check what needs to be done here
        }

        public override void Undo()
        {
            robot.Move(robot.Direction * -1);
        }

        public override void Prewarm()
        {
            FieldController.Instance.GetTileAtIntPosition(robot.Position).ReleaseOccupier(robot);
        }
    }
}
