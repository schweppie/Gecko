using Utility;

namespace Gameplay
{
    public class GameStepController : SingletonBehaviour<GameStepController>
    {
        public delegate void StepDelegate(int step);
        public event StepDelegate OnGameStep;

        private int step;
        public int Step => step;
        
        public void DoGameStep()
        {
            step++;

            if (OnGameStep != null)
                OnGameStep(step);
        }
    }
}
