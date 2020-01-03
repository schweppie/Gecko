using Gameplay.Tiles;
using UnityEngine;
using Utility;

namespace Gameplay.Robots
{
    public class RobotsController : SingletonBehaviour<RobotsController>
    {
        [SerializeField]
        private RobotVisual robotPrefab;

        public RobotVisual CreateRobot(Tile startTile)
        {
            RobotVisual robot = Instantiate(robotPrefab);
            robot.CurrentTile = startTile;
            robot.Initialize();
            return robot;
        }
    }
}
