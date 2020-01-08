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
        
        private Dictionary<Vector3Int, IOccupier> occupationBuffer = new Dictionary<Vector3Int, IOccupier>();
        public Dictionary<Vector3Int, IOccupier> OccupationBuffer => occupationBuffer;

        private Dictionary<Vector3Int, IOccupier> oldOccupationBuffer = new Dictionary<Vector3Int, IOccupier>();
        public Dictionary<Vector3Int, IOccupier> OldOccupationBuffer => oldOccupationBuffer;


        public void AddOccupier(Vector3Int position, IOccupier occupier)
        {
            occupationBuffer[position] = occupier;
        }
        
        private void ClearBuffers()
        {
            oldOccupationBuffer.Clear();
            foreach (var pair in occupationBuffer)
                oldOccupationBuffer.Add(pair.Key, pair.Value);
            occupationBuffer.Clear();
        }

        public void DoForwardStep()
        {
            ClearBuffers();

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

            ClearBuffers();
        }
    }
}
