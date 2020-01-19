using System.Collections.Generic;
using System.Linq;
using Gameplay.Stations.Components;
using Gameplay.Tiles;
using UnityEngine;

namespace Gameplay.Stations
{
    public class StationVisual : MonoBehaviour
    {
        [SerializeField]
        private Transform tilesTransform = null;

        private StationComponent[] stationComponents;
        public StationComponent[] StationComponents => stationComponents;

        private List<TileVisual> localTileVisuals = null;
        public List<TileVisual> LocalTileVisuals
        {
            get
            {
                if (localTileVisuals == null)
                {
                    localTileVisuals = new List<TileVisual>();
                    localTileVisuals = tilesTransform.GetComponentsInChildren<TileVisual>().ToList();
                }

                return localTileVisuals;
            }
        }

        private Station station;

        private void Awake()
        {
            stationComponents = GetComponents<StationComponent>();
        }

        public void Initialize(Station station)
        {
            this.station = station;
            for (int i = 0; i < stationComponents.Length; i++)
                stationComponents[i].Initialize(station);
        }

        [ContextMenu("Test remove")]
        private void TestRemove()
        {
            if (Application.isPlaying)
                station.TestRemove();
        }
    }
}
