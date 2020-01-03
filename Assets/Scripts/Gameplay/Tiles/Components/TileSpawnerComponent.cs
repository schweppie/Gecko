using Gameplay.Robots;
using UnityEngine;
using Utility;

namespace Gameplay.Tiles.Components
{
    public class TileSpawnerComponent : TileComponent
    {
        public override void DoStep()
        {
            RobotsController.Instance.CreateRobot(tile, transform.forward.ToIntVector());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
                DoStep();
        }
    }
}
