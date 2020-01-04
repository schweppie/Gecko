using Gameplay.Robots;
using JP.Framework.Extensions;
using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public class TileSpawnerComponent : TileComponent
    {
        public override void DoStep()
        {
            if (GameStepController.Instance.IsPositionBlocked(tile.IntPosition))
                return;
            
            RobotsController.Instance.CreateRobot(tile, transform.forward.ToIntVector());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
                DoStep();
        }
    }
}
