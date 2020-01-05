using Gameplay.Tiles;
using JP.Framework.Flow;
using UnityEngine;

namespace Gameplay.Robots
{
    public class RobotsController : SingletonBehaviour<RobotsController>
    {
        [SerializeField]
        private RobotVisual robotPrefab = null;

        public Robot CreateRobot(Tile startTile, Vector3Int direction)
        {
            Robot robot = new Robot(startTile, direction);
            robot.Initialize();
            return robot;
        }

        public RobotVisual CreateRobotVisual(Robot robot)
        {
            RobotVisual robotVisual = Instantiate(robotPrefab);
            robotVisual.Initialize(robot);

            robot.SetVisual(robotVisual);

            return robotVisual;
        }
    }
}
