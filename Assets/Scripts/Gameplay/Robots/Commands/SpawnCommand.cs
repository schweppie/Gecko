using Gameplay.Field;

namespace Gameplay.Robots.Commands
{
    public class SpawnCommand : RobotCommand
    {
        private RobotVisual robotVisual;
        
        public override void Execute()
        {
            robotVisual = RobotsController.Instance.CreateRobotVisual(robot);
            FieldController.Instance.AddOccupier(robot.Position, robot);
        }
    }
}
