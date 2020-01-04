using System.Collections.Generic;
using JP.Framework.Flow;
using UnityEngine;

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
        
        private HashSet<Vector3Int> stepBlockBuffer = new HashSet<Vector3Int>();

        public bool IsPositionBlocked(Vector3Int position)
        {
            return stepBlockBuffer.Contains(position);
        }

        public void PopulatePositionBuffer(Vector3Int position)
        {
            stepBlockBuffer.Add(position);
        }
        
        public void DoForwardStep()
        {
            stepBlockBuffer.Clear();
            
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
            
            stepBlockBuffer.Clear();
        }
    }
}
