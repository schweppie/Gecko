namespace Gameplay.Robots.Commands
{
    public class MoveRobotCommand : RobotCommand
    {
        public override void Execute()
        {
            robot.Move(robot.Direction);
        }

        public override void Undo()
        {
            robot.Move(robot.Direction * -1);
        }
    }
}
