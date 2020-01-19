using System;
using Gameplay.Tiles.Components;
using UnityEngine;

namespace Gameplay.Stations.Components
{
    public class DynamicDirectionStationComponent : StationComponent
    {
        [SerializeField]
        private DirectionTileComponent directionTileComponent;

        private IOccupier lastOccupier;

        private void Start()
        {
            GameStepController.Instance.OnDynamicStepComplete += OnDynamicStepComplete;
        }

        private void OnDestroy()
        {
            GameStepController.Instance.OnDynamicStepComplete -= OnDynamicStepComplete;
        }

        private void OnDynamicStepComplete(int step)
        {
            if (lastOccupier != null && !directionTileComponent.Tile.IsOccupied)
                directionTileComponent.FlipDirection();
            lastOccupier = directionTileComponent.Tile.Occupier;
        }
    }
}
