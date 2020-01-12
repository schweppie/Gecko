using System;
using System.Collections.Generic;
using Gameplay.Tiles;
using JP.Framework.Flow;
using UnityEngine;

namespace Gameplay.Robots
{
    public class RobotsController : SingletonBehaviour<RobotsController>
    {
        [SerializeField]
        private RobotVisual robotPrefab = null;

        private List<Robot> robots = new List<Robot>();

        private void Awake()
        {
            GameStepController.Instance.OnDynamicForwardStep += OnDynamicForwardStep;
        }

        private void OnDynamicForwardStep(int step)
        {
            HashSet<Robot> primaryRobotExecutionSet = new HashSet<Robot>();
            HashSet<Robot> secundaryRobotExecutionSet = new HashSet<Robot>();

            foreach (Robot robot in robots)
                primaryRobotExecutionSet.Add(robot);

            while (primaryRobotExecutionSet.Count > 0)
            {
                secundaryRobotExecutionSet.Clear();
                foreach (Robot robot in primaryRobotExecutionSet)
                    robot.OnDynamicForwardStep(step, secundaryRobotExecutionSet);

                primaryRobotExecutionSet.Clear();
                foreach (Robot robot in secundaryRobotExecutionSet)
                    robot.OnDynamicForwardStep(step, primaryRobotExecutionSet);
            }
        }

        public Robot CreateRobot(Tile startTile, Vector3Int direction)
        {
            Robot robot = new Robot(startTile, direction);
            robot.Initialize();

            robots.Add(robot);

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
