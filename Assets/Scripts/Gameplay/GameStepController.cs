using System.Collections.Generic;
using Gameplay.Tiles;
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

        public IOccupier GetOccupierAt(Vector3Int position)
        {
            if (occupationBuffer.ContainsKey(position))
                return occupationBuffer[position];

            return null;
        }

        public IOccupier GetOldOccupierAt(Vector3Int position)
        {
            if (oldOccupationBuffer.ContainsKey(position))
                return oldOccupationBuffer[position];

            return null;
        }

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

        private void WriteOccupiersToTiles()
        {
            FieldController.Instance.ClearTileOccupations();

            foreach (var pair in occupationBuffer)
            {
                Tile tile = FieldController.Instance.GetTileAtIntPosition(pair.Key);
                tile.SetOccupier(pair.Value);
            }
        }

        public void DoForwardStep()
        {
            ClearBuffers();

            step++;

            if (OnStaticForwardStep != null)
                OnStaticForwardStep(step);
            
            if (OnDynamicForwardStep != null)
                OnDynamicForwardStep(step);

            WriteOccupiersToTiles();
        }

        public void DoBackwardStep()
        {
            if (step == 0)
                return;
            
            step--;
            
            // TODO: Not sure yet which order the steps should be handled
            // when rewinding

            if (OnDynamicBackwardStep != null)
                OnDynamicBackwardStep(step);
            
            if (OnStaticBackwardStep != null)
                OnStaticBackwardStep(step);

            ClearBuffers();
        }
    }
}
