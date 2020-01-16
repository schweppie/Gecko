
using UnityEngine;

namespace Gameplay.Robots.Commands
{
    public class MoveCommand : RobotCommand
    {
        // Vertical offset is used when moving on or off slopes.
        // Is calculated in the move strategy.
        // 1 = up, 0 = default, -1 going down
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
            // TODO undo is broken becuase of slopes and new visualization
            robot.Move(robot.Direction * -1);
        }
    }
}
