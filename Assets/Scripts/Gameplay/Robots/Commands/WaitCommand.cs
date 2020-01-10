namespace Gameplay.Robots.Commands
{
    public class WaitCommand : RobotCommand
    {
        public override void Execute()
        {
            GameStepController.Instance.AddOccupier(robot.Position, robot);
            robot.RobotVisual.AnimateIdle();
        }

        public override void Undo()
        {
        }
    }
}
