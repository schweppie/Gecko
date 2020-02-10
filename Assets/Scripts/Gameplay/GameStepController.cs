using Gameplay.Field;
using JP.Framework.Flow;

namespace Gameplay
{
    public class GameStepController : SingletonBehaviour<GameStepController>
    {
        public delegate void StepDelegate(int step);

        public event StepDelegate OnStaticStepStart;
        public event StepDelegate OnStaticStep;
        public event StepDelegate OnStaticStepComplete;
        public event StepDelegate OnDynamicStepStart;
        public event StepDelegate OnDynamicStep;
        public event StepDelegate OnDynamicStepComplete;

        private int step;
        public int Step => step;

        public void DoForwardStep()
        {
            FieldController.Instance.ClearBuffers();

            step++;

            OnStaticStepStart?.Invoke(step);
            OnStaticStep?.Invoke(step);
            OnStaticStepComplete?.Invoke(step);
            
            OnDynamicStepStart?.Invoke(step);
            OnDynamicStep?.Invoke(step);
            FieldController.Instance.WriteOccupiersToTiles();
            OnDynamicStepComplete?.Invoke(step);
        }
    }
}
