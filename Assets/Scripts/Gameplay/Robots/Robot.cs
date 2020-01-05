using System;
using System.Collections.Generic;
using Gameplay.Field;
using Gameplay.Robots.Components;
using Gameplay.Tiles;
using JP.Framework.Commands;
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

        public Robot(Tile startTile, Vector3Int direction)
        {
            tile = startTile;
            tile.SetOccupier(this);
            position = startTile.IntPosition;
            this.direction = direction;
        }
        
        public void Initialize()
        {
            components.Add(commandComponent.GetType(), commandComponent);

            foreach (RobotComponent component in components.Values)
                component.Initialize(this);
            
            GameStepController.Instance.OnDynamicBackwardStep += OnDynamicBackwardStep;
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
            tile.ReleaseOccupier(this);
            tile = nextTile;
            tile.SetOccupier(this);
        }
        
        public void Dispose()
        {
            OnDispose?.Invoke();
            
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
    }
}
