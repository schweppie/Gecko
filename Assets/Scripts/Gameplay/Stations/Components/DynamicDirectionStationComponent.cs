using Gameplay.Tiles.Components;
using UnityEngine;

namespace Gameplay.Stations.Components
{
    public class DynamicDirectionStationComponent : StationComponent
    {
        [SerializeField]
        private DirectionTileComponent directionTileComponent;

        private IOccupier lastOccupier;

        public override void DoNextStep()
        {
            if (lastOccupier != null && !directionTileComponent.Tile.IsOccupied)
            {
                directionTileComponent.FlipDirection();
            }

            lastOccupier = directionTileComponent.Tile.Occupier;
        }
    }
}
