
namespace Gameplay.Robots.Commands
{
    public class MoveCommand : RobotCommand
    {
        private void OnVisualizationComplete()
        {
            GameVisualizationController.Instance.OnVisualizationComplete -= OnVisualizationComplete;
            robot.RobotVisual.AnimateIdle();
        }

        public override void Execute()
        {
            GameVisualizationController.Instance.OnVisualizationComplete += OnVisualizationComplete;
            robot.Move(robot.Direction);
            GameStepController.Instance.AddOccupier(robot.Position, robot);

            robot.RobotVisual.AnimateMove();
        }

        public override void Undo()
        {
            robot.Move(robot.Direction * -1);
        }
    }
}
