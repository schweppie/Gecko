using Gameplay.Robots;
using JP.Framework.Extensions;
using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public class TileSpawnerComponent : TileComponent
    {
        [SerializeField]
        private int spawnCount = 4;
        private int spawned = 0;
        
        public override void DoNextStep()
        {
            if (spawned >= spawnCount)
                return;
            
            if (GameStepController.Instance.GetOccupierAt(tile.IntPosition) != null)
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
