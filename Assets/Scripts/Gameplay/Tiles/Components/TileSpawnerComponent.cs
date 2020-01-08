using Gameplay.Robots;
using JP.Framework.Extensions;
using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public class TileSpawnerComponent : TileComponent
    {
        private const int MAX_AVAILABLE = 4;
        private int spawned = 0;
        
        public override void DoNextStep()
        {
            if (spawned >= MAX_AVAILABLE)
                return;

            if (tile.IsOccupied)
                return;
            
            RobotsController.Instance.CreateRobot(tile, transform.forward.ToIntVector());

            spawned++;
        }

        public override void DoPrevStep()
        {
            spawned--;

            if (spawned <= 0)
                spawned = 0;
        }
    }
}
