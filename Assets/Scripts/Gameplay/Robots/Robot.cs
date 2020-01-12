using System;
using System.Collections.Generic;
using Gameplay.Robots.Components;
using Gameplay.Tiles;
using UnityEngine;

namespace Gameplay.Robots
{
    public class Robot : IDisposable, IOccupier
    {
        private Tile tile;
        public Tile Tile => tile;
        
        private Vector3Int position;
        
        private Vector3Int direction;
        public Vector3Int Direction => direction;

        public Vector3Int Position => position;

        private RobotVisual robotVisual;
        public RobotVisual RobotVisual => robotVisual;

        private Dictionary<Type, RobotComponent> components = new Dictionary<Type, RobotComponent>();
        private RobotCommandComponent commandComponent = new RobotCommandComponent();

        public delegate void voidDelegate();
        public event voidDelegate OnDispose;

        public bool isDebugBot = false;

        public Robot(Tile startTile, Vector3Int direction)
        {
            tile = startTile;
            position = startTile.IntPosition;
            this.direction = direction;
        }
        
        public void Initialize()
        {
            components.Add(commandComponent.GetType(), commandComponent);

            foreach (RobotComponent component in components.Values)
                component.Initialize(this);
            
            GameStepController.Instance.OnDynamicForwardStep += OnDynamicForwardStep;
            GameStepController.Instance.OnDynamicBackwardStep += OnDynamicBackwardStep;
        }

        private void OnDynamicForwardStep(int step)
        {
            commandComponent.ExecuteNextCommand();
        }

        private void OnDynamicBackwardStep(int step)
        {
            commandComponent.ExecutePrevCommand();
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
        
        public T GetComponent<T>() where T : RobotComponent
        {
            RobotComponent component;
            components.TryGetValue(typeof(T), out component);
            return component as T;
        }

        public void PickNewStrategy()
        {
            commandComponent.UndoLastCommand();
            commandComponent.ExecuteNextCommand();
        }
    }
}
