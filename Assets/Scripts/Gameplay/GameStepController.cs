using Gameplay.Field;
using JP.Framework.Flow;

namespace Gameplay
{
    public class GameStepController : SingletonBehaviour<GameStepController>
    {
        public delegate void StepDelegate(int step);

        public event StepDelegate OnStaticStep;
        public event StepDelegate OnDynamicStep;

        private int step;
        public int Step => step;

        public void DoForwardStep()
        {
            FieldController.Instance.ClearBuffers();

            step++;

            if (OnStaticStep != null)
                OnStaticStep(step);
            
            if (OnDynamicStep != null)
                OnDynamicStep(step);

            FieldController.Instance.WriteOccupiersToTiles();
        }
    }
}
