using UnityEngine;

namespace Gecko.Gameplay
{
    public class GameStateMachineHolder : MonoBehaviour
    {
        private GameStatemachine gameStatemachine;
        
        private void Start()
        {
            gameStatemachine = new GameStatemachine();
            gameStatemachine.Start();
        }
    }
}