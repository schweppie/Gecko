using JP.Framework.Flow;

namespace Gecko.Gameplay
{
    public class GameStateMachineHolder : SingletonBehaviour<GameStateMachineHolder>
    {
        private GameStatemachine gameStatemachine;
        public GameStatemachine GameStateMachine => gameStatemachine;
        
        private void Start()
        {
            gameStatemachine = new GameStatemachine();
            gameStatemachine.Start();
        }

        private void Update()
        {
            gameStatemachine.Update();
        }
    }
}
