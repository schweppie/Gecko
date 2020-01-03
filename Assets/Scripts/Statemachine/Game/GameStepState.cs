using Gameplay;
using JP.Framework.Statemachine;
using UnityEngine;

namespace Gecko.Gameplay
{
    public class GameStepState : State<GameStates>
    {
        public override void Update()
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                GameStepController.Instance.DoForwardStep();
                stateMachine.ChangeTo(GameStates.GameVisualization);
            }
            
            if (Input.GetKeyUp(KeyCode.S))
            {
                GameStepController.Instance.DoBackwardStep();
                stateMachine.ChangeTo(GameStates.GameVisualization);                
            }
        }
    }
}
