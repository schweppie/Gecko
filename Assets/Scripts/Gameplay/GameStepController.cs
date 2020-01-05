using JP.Framework.Flow;

namespace Gameplay
{
    public class GameStepController : SingletonBehaviour<GameStepController>
    {
        public delegate void StepDelegate(int step);
        
        public event StepDelegate OnDynamicForwardStep;
        public event StepDelegate OnDynamicBackwardStep;

        public event StepDelegate OnStaticForwardStep;
        public event StepDelegate OnStaticBackwardStep;

        private int step;
        public int Step => step;
        
        public void DoForwardStep()
        {
            step++;

            if (OnStaticForwardStep != null)
                OnStaticForwardStep(step);
            
            if (OnDynamicForwardStep != null)
                OnDynamicForwardStep(step);
        }

        public void DoBackwardStep()
        {
            if (step == 0)
                return;
            
            step--;
            
            if (OnDynamicBackwardStep != null)
                OnDynamicBackwardStep(step);
            
            if (OnStaticBackwardStep != null)
                OnStaticBackwardStep(step);
        }
    }
}
