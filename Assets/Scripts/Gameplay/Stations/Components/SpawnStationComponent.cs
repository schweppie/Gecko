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

        public override void DoNextStep()
        {
            if (spawned >= spawnCount)
                return;

            switch (state)
            {
                case State.Idle:
                    bool spawnedRobot = SpawnRobot();
                    if (spawnedRobot)
                        state = State.OpenDoor;
                    break;
                case State.OpenDoor:
                    OpenDoor();
                    state = State.CloseDoor;
                    break;
                case State.CloseDoor:
                    CloseDoor();
                    spawned++;
                    state = State.Idle;
                    break;
            }
        }

        private bool SpawnRobot()
        {
            Tile spawnTile = FieldController.Instance.GetTileAtIntPosition(spawnerTileVisual.IntPosition);
            if (spawnTile.IsOccupied)
                return false;
            
            Robot robot = RobotsController.Instance.CreateRobot(spawnTile, spawnerTileVisual.transform.forward.ToIntVector());
            robot.isDebugBot = spawnDebugBots;
            return true;
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
