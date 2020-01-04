namespace Gameplay.Robots.Commands
{
    public class MoveCommand : RobotCommand
    {
        public override void Execute()
        {
            robot.Move(robot.Direction);
            GameStepController.Instance.PopulatePositionBuffer(robot.Position);
        }

        public override void Undo()
        {
            robot.Move(robot.Direction * -1);
        }
    }
}
