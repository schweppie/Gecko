using System;
using System.Collections.Generic;
using Gameplay.Field;
using Gameplay.Products;
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

        private ICarryable carryable = null;
        public bool IsCarrying => carryable != null;

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

            GameStepController.Instance.OnDynamicStep += OnDynamicStep;
        }

        private void OnDynamicStep(int step)
        {
            commandComponent.ExecuteNextCommand();
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

            GameStepController.Instance.OnDynamicStep -= OnDynamicStep;
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

        public void CarryProduct(Product product)
        {
            if (IsCarrying)
                throw new Exception("Was already carrying");
            carryable = product;
        }
    }
}
