using System;
using System.Collections.Generic;
using Commands;
using Gameplay.Robots.Components;
using Gameplay.Tiles;
using UnityEngine;

namespace Gameplay.Robots
{
    public class Robot : IDisposable
    {
        private Tile tile;
        private Tile oldTile;
        
        private Vector3Int direction;
        public Vector3Int Direction => direction;

        public Vector3Int OldPosition => oldTile.IntPosition;
        public Vector3Int Position => tile.IntPosition;

        private List<RobotComponent> components = new List<RobotComponent>();
        private RobotCommandComponent commandComponent = new RobotCommandComponent();

        public delegate void voidDelegate();
        public event voidDelegate OnDispose;
        
        public Robot(Tile startTile, Vector3Int direction)
        {
            tile = startTile;
            oldTile = startTile;
            this.direction = direction;
        }
        
        public void Initialize()
        {
            components.Add(commandComponent);

            foreach (RobotComponent component in components)
                component.Initialize(this);
            
            GameStepController.Instance.OnForwardStep += OnForwardStep;
            GameStepController.Instance.OnBackwardStep += OnBackwardStep;
        }

        private void OnForwardStep(int step)
        {
            commandComponent.GetNextCommand().Execute();
        }

        private void OnBackwardStep(int step)
        {
            Command command = commandComponent.GetPrevCommand();

            if (command == null)
            {
                Dispose();
                return;
            }
            
            command.Undo();
        }
        
        public void SetDirection(Vector3Int direction)
        {
            this.direction = direction;
        }

        public void Move(Vector3Int direction)
        {
            oldTile = tile;
            Vector3Int nextPosition = tile.IntPosition + direction;
            Tile nextTile = FieldController.Instance.GetTileAtIntPosition(nextPosition);
            tile = nextTile;
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
            
            GameStepController.Instance.OnForwardStep -= OnForwardStep;
            GameStepController.Instance.OnBackwardStep -= OnBackwardStep;
        }
    }
}
