using UnityEngine;

namespace Gameplay.Stations.Components
{
    public abstract class StationComponent : MonoBehaviour
    {
        protected Station station;
        
        public void SetStation(Station station)
        {
            this.station = station;
        }
        
        public virtual void DoNextStep()
        {
        }

        public virtual void DoPrevStep()
        {
        }
    }
}
