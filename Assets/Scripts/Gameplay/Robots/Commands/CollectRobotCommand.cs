namespace Gameplay.Robots.Commands
{
    public class CollectRobotCommand : RobotCommand
    {
        public override void Execute()
        {
            robot.RobotVisual.OnDispose();
        }
    }
}
