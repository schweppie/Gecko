using Gameplay.Field;

namespace Gameplay.Robots.Commands
{
    public class LoadCommand : RobotCommand
    {
        public override void Execute()
        {
            FieldController.Instance.AddOccupier(robot.Position, robot);
            robot.CarriesResource = true;
        }
    }
}
