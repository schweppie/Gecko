namespace Gameplay.Robots.Commands
{
    public class SpawnCommand : RobotCommand
    {
        private RobotVisual robotVisual;
        
        public override void Execute()
        {
            robotVisual = RobotsController.Instance.CreateRobotVisual(robot);
            GameStepController.Instance.PopulatePositionBuffer(robot.Position);
        }

        public override void Undo()
        {
            robot.Dispose();
        }
    }
}
