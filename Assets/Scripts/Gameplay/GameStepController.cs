using JP.Framework.Flow;

namespace Gameplay
{
    public class GameStepController : SingletonBehaviour<GameStepController>
    {
        public delegate void StepDelegate(int step);

        public event StepDelegate OnPickCommands;
        public event StepDelegate OnPrewarmCommands;
        public event StepDelegate OnExecuteCommands;
        public event StepDelegate OnRewindCommands;

        private int step;
        public int Step => step;
        
        public void DoForwardStep()
        {
            step++;

            OnPickCommands?.Invoke(step);
            OnPrewarmCommands?.Invoke(step);
            OnExecuteCommands?.Invoke(step);
        }

        public void DoBackwardStep()
        {
            if (step == 0)
                return;
            
            step--;

            OnRewindCommands?.Invoke(step);
        }
    }
}
