using Gameplay;
using JP.Framework.Statemachine;

namespace Gecko.Gameplay
{
    public class GameVisualizeState : State<GameStates>
    {
        public override void Enter()
        {
            GameVisualizationController.Instance.OnVisualizationComplete += OnVisualizationComplete;
            GameVisualizationController.Instance.DoVisualization();
        }

        public override void Exit()
        {
            GameVisualizationController.Instance.OnVisualizationComplete -= OnVisualizationComplete;
        }

        private void OnVisualizationComplete()
        {
            stateMachine.ChangeTo((GameStates.GameStep));
        }
    }
}
