using System.Collections.Generic;
using System.Linq;
using Gameplay.Tiles;
using UnityEngine;

namespace Gameplay.Stations
{
    public class StationVisual : MonoBehaviour
    {
        [SerializeField]
        private Transform tilesTransform = null;

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

        public void Initialize(Station station)
        {
            this.station = station;
        }

        [ContextMenu("Test remove")]
        private void TestRemove()
        {
            if (Application.isPlaying)
                station.TestRemove();
        }
    }
}
