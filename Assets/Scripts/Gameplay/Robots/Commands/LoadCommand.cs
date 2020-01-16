namespace Gameplay.Robots.Commands
{
    public class LoadCommand : RobotCommand
    {
        public override void Execute()
        {
            GameStepController.Instance.AddOccupier(robot.Position, robot);
            robot.CarriesResource = true;
        }

        public override void Undo()
        {
            robot.CarriesResource = false;
        }
    }
}
