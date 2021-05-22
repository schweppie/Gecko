using Gameplay.Field;

namespace Gameplay.Robots.Commands
{
    public class DestroySelfCommand : RobotCommand
    {
        private RobotVisual robotVisual;

        public override void Execute()
        {
            robot.RemoveRobot();
            FieldController.Instance.RemoveOccupier(robot.Position);
        }
    }
}
