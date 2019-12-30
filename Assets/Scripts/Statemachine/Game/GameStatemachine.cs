using JP.Framework.Statemachine;

namespace Gecko.Gameplay
{
    public class GameStatemachine : StateMachine<GameStates>
    {
        protected override void InstantiateStates()
        {
            AddState(GameStates.Countdown, new CountdownState());
            AddState(GameStates.Gameloop, new GameLoopState());
            AddState(GameStates.GameOver, new GameOverState());
        }

        protected override GameStates GetInitialState()
        {
            return GameStates.Countdown;
        }
    }
}
