using Gameplay;
using JP.Framework.Statemachine;

namespace Gecko.Gameplay
{
    public class GameStepState : State<GameStates>
    {
        public override void Enter()
        {
            GameStepController.Instance.DoForwardStep();
            
            stateMachine.ChangeTo(GameStates.GameVisualization);
        }
    }
}
