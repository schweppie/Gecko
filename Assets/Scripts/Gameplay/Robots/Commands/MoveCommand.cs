
using UnityEngine;

namespace Gameplay.Robots.Commands
{
    public class MoveCommand : RobotCommand
    {
        int verticalOffset;

        public MoveCommand(int verticalOffset)
        {
            this.verticalOffset = verticalOffset;   
        }

        public override void Execute()
        {
            GameVisualizationController.Instance.OnVisualizationComplete += OnVisualizationComplete;

            robot.Move(robot.Direction + Vector3Int.up * verticalOffset);

            GameStepController.Instance.AddOccupier(robot.Position, robot);

            robot.RobotVisual.AnimateMove();
        }

        private void OnVisualizationComplete()
        {
            GameVisualizationController.Instance.OnVisualizationComplete -= OnVisualizationComplete;
            robot.RobotVisual.AnimateIdle();
        }

        public override void Undo()
        {
            robot.Move(robot.Direction * -1);
        }
    }
}
