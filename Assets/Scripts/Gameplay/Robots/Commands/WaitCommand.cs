using Gameplay.Field;

namespace Gameplay.Robots.Commands
{
    public class WaitCommand : RobotCommand
    {
        public override void Execute()
        {
            FieldController.Instance.AddOccupier(robot.Position, robot);
            robot.RobotVisual.AnimateIdle();
        }
    }
}
