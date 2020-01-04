namespace Gameplay.Robots.Commands
{
    public class SpawnRobotCommand : RobotCommand
    {
        private RobotVisual robotVisual;
        
        public override void Execute()
        {
            robotVisual = RobotsController.Instance.CreateRobotVisual(robot);
        }

        public override void Undo()
        {
            robot.Dispose();
        }
    }
}
