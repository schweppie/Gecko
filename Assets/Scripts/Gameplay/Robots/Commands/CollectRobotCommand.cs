using Gameplay.Field;

namespace Gameplay.Robots.Commands
{
    public class CollectRobotCommand : RobotCommand
    {
        public override void Execute()
        {
            robot.RobotVisual.OnDispose();
            FieldController.Instance.GetTileAtIntPosition(robot.Position).ReleaseOccupier(robot);
        }

        public override void Undo()
        {
            RobotsController.Instance.CreateRobotVisual(robot);
            FieldController.Instance.GetTileAtIntPosition(robot.Position).SetOccupier(robot);
        }
    }
}
