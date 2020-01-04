namespace Gameplay.Robots.Commands
{
    public class WaitCommand : RobotCommand
    {
        public override void Execute()
        {
            GameStepController.Instance.PopulatePositionBuffer(robot.Position);
        }

        public override void Undo()
        {
        }
    }
}
