using Utility;

namespace Gameplay
{
    public class GameStepController : SingletonBehaviour<GameStepController>
    {
        public delegate void StepDelegate(int step);
        public event StepDelegate OnForwardStep;
        public event StepDelegate OnBackwardStep;

        private int step;
        public int Step => step;
        
        public void DoForwardStep()
        {
            step++;

            if (OnForwardStep != null)
                OnForwardStep(step);
        }

        public void DoBackwardStep()
        {
            if (step == 0)
                return;
            
            step--;

            if (OnBackwardStep != null)
                OnBackwardStep(step);            
        }
    }
}
