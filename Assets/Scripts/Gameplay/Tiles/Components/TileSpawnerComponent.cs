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

        [SerializeField]
        private bool spawnDebugBots = false;
        
        public override void DoNextStep()
        {
            if (spawned >= spawnCount)
                return;
            
            if (FieldController.Instance.GetTileAtIntPosition(tile.IntPosition).IsOccupied)
                return;
            
            Robot robot = RobotsController.Instance.CreateRobot(tile, transform.forward.RoundToIntVector());
            robot.isDebugBot = spawnDebugBots;

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
