using Gameplay.Field;

namespace Gameplay.Robots.Commands
{
    public class DestroySelfCommand : RobotCommand
    {
        public override void Execute()
        {
            robot.RemoveRobot();
            FieldController.Instance.RemoveOccupier(robot.Position);
        }
    }
}
