using Gameplay.Robots;
using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public class TileSpawnerComponent : TileComponent
    {
        public override void DoStep()
        {
            RobotController.Instance.CreateRobot(tile);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
                DoStep();
        }
    }
}
