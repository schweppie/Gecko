using Gameplay.Tiles;
using UnityEngine;
using Utility;

namespace Gameplay.Robots
{
    public class RobotsController : SingletonBehaviour<RobotsController>
    {
        [SerializeField]
        private RobotVisual robotPrefab;

        public Robot CreateRobot(Tile startTile, Vector3Int direction)
        {
            Robot robot = new Robot(startTile, direction);
            robot.Initialize();
            
            RobotVisual robotVisual = Instantiate(robotPrefab);
            robotVisual.Initialize(robot);
            
            return robot;
        }
    }
}
