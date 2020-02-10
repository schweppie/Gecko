using DG.Tweening;
using Gameplay.Field;
using Gameplay.Robots;
using Gameplay.Tiles;
using JP.Framework.Extensions;
using UnityEngine;

namespace Gameplay.Stations.Components
{
    public class SpawnStationComponent : StationComponent
    {
        [SerializeField]
        private int spawnCount = 4;

        [SerializeField]
        private TileVisual spawnerTileVisual;

        [SerializeField]
        private Transform doorPivot = null;

        [SerializeField]
        private bool spawnDebugBots = false;

        enum State
        {
            Idle,
            OpenDoor,
            CloseDoor
        }

        private State state = State.Idle;
        private int spawned = 0;
        private Robot lastSpawnedRobot = null;

        private void Start()
        {
            GameStepController.Instance.OnDynamicStepComplete += OnDynamicStepComplete;
        }

        private void OnDestroy()
        {
            GameStepController.Instance.OnDynamicStepComplete -= OnDynamicStepComplete;
        }

        public override void DoNextStep()
        {
            if (spawned >= spawnCount)
                return;

            switch (state)
            {
                case State.Idle:
                    Robot robot = SpawnRobot();
                    if (robot != null)
                    {
                        state = State.OpenDoor;
                        lastSpawnedRobot = robot;
                    }
                    break;

                case State.OpenDoor:
                    break;

                case State.CloseDoor:
                    CloseDoor();
                    spawned++;
                    state = State.Idle;
                    break;
            }
        }

        private void OnDynamicStepComplete(int step)
        {
            Vector3Int exitPosition = spawnerTileVisual.IntPosition + spawnerTileVisual.transform.forward.ToIntVector();
            Tile exitTile = FieldController.Instance.GetTileAtIntPosition(exitPosition);
            if (state == State.OpenDoor)
            {
                if (exitTile.Occupier == lastSpawnedRobot)
                { // spawned robot is going to exit
                    OpenDoor();
                    state = State.CloseDoor;
                }
            }
        }

        private Robot SpawnRobot()
        {
            Tile spawnTile = FieldController.Instance.GetTileAtIntPosition(spawnerTileVisual.IntPosition);
            if (spawnTile.IsOccupied)
                return null;

            Robot robot = RobotsController.Instance.CreateRobot(spawnTile, spawnerTileVisual.transform.forward.ToIntVector());
            robot.isDebugBot = spawnDebugBots;
            return robot;
        }

        private void OpenDoor()
        {
            doorPivot.DOScaleY(.05f, .5f);
        }

        private void CloseDoor()
        {
            doorPivot.DOScaleY(1f, .5f);
        }
    }
}
