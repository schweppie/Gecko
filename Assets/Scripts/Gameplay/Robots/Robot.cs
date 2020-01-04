using System;
using System.Collections.Generic;
using Gameplay.Robots.Components;
using Gameplay.Tiles;
using JP.Framework.Commands;
using UnityEngine;

namespace Gameplay.Robots
{
    public class Robot : IDisposable
    {
        private Tile tile;
        public Tile Tile => tile;
        
        private Vector3Int position;
        
        private Vector3Int direction;
        public Vector3Int Direction => direction;

        public Vector3Int Position => position;

        private RobotVisual robotVisual;
        public RobotVisual RobotVisual => robotVisual;

        private List<RobotComponent> components = new List<RobotComponent>();
        private RobotCommandComponent commandComponent = new RobotCommandComponent();

        public delegate void voidDelegate();
        public event voidDelegate OnDispose;
        
        public Robot(Tile startTile, Vector3Int direction)
        {
            tile = startTile;
            position = startTile.IntPosition;
            this.direction = direction;
        }
        
        public void Initialize()
        {
            components.Add(commandComponent);

            foreach (RobotComponent component in components)
                component.Initialize(this);
            
            GameStepController.Instance.OnDynamicForwardStep += OnDynamicForwardStep;
            GameStepController.Instance.OnDynamicBackwardStep += OnDynamicBackwardStep;
        }

        private void OnDynamicForwardStep(int step)
        {
            commandComponent.GetNextCommand().Execute();
        }

        private void OnDynamicBackwardStep(int step)
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
            position = position + direction;
            Tile nextTile = FieldController.Instance.GetTileAtIntPosition(position);
            tile = nextTile;
        }
        
        public void Dispose()
        {
            OnDispose?.Invoke();
            
            GameStepController.Instance.OnDynamicForwardStep -= OnDynamicForwardStep;
            GameStepController.Instance.OnDynamicBackwardStep -= OnDynamicBackwardStep;
        }

        public void SetVisual(RobotVisual robotVisual)
        {
            this.robotVisual = robotVisual;
        }
    }
}
