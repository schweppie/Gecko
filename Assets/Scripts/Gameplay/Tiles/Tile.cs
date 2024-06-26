﻿using System;
using System.Collections.Generic;
using Gameplay.Field;
using Gameplay.Tiles.Components;
using Gameplay.Tiles.Reporters.Enter;
using Gameplay.Tiles.Reporters.Exit;
using Gameplay.Tiles.Reporters.Height;
using Gameplay.Tiles.Reporters.Normal;
using UnityEngine;

namespace Gameplay.Tiles
{
    public class Tile : IDisposable
    {
        private TileVisual visual;
        public TileVisual Visual => visual;

        private Vector3Int intPosition;
        public Vector3Int IntPosition => intPosition;

        public TileType Type { get; }

        private Dictionary<Type, TileComponent> tileComponents;

        private IOccupier occupier;
        public IOccupier Occupier => occupier;
        public bool IsOccupied { get { return occupier != null; } }

        private BaseHeightReporter heightReporter;
        public BaseHeightReporter HeightReporter => heightReporter;

        private BaseExitReporter exitReporter;
        public BaseExitReporter ExitReporter => exitReporter;

        private BaseEnterReporter enterReporter;
        public BaseEnterReporter EnterReporter => enterReporter;

        private BaseNormalReporter normalReporter;
        public BaseNormalReporter NormalReporter => normalReporter;

        public delegate void voidDelegate();
        public event voidDelegate OnDispose;

        public Tile(TileVisual visual)
        {
            this.visual = visual;

            heightReporter = new DefaultHeightReporter(this);
            exitReporter = new DefaultExitReporter(this);
            enterReporter = new DefaultEnterReporter(this);
            normalReporter = new DefaultNormalReporter(this);

            tileComponents = new Dictionary<Type, TileComponent>();
            foreach (TileComponent tileComponent in visual.TileComponents)
            {
                tileComponents[tileComponent.GetType()] = tileComponent;
                tileComponent.Initialize(this);
            }

            GameStepController.Instance.OnStaticStep += OnStaticStep;
        }

        public void SetHeightReporter(BaseHeightReporter reporter)
        {
            heightReporter = reporter;
        }

        public void SetExitReporter(BaseExitReporter reporter)
        {
            exitReporter = reporter;
        }

        public void RemoveTile()
        {
            Dispose();
        }

        public void SetEnterReporter(BaseEnterReporter reporter)
        {
            enterReporter = reporter;
        }

        public void SetNormalReporter(BaseNormalReporter reporter)
        {
            normalReporter = reporter;
        }

        private void OnStaticStep(int step)
        {
            foreach (var tileComponent in tileComponents.Values)
                tileComponent.DoStaticStep();
        }


        /// <summary>
        /// Normally the data (this tile) is created first before the visual, but as we want to build levels in the
        /// Unity editor in scenes, the visuals already exists while the data does not yet
        /// </summary>
        public static Tile ConstructTileFromVisual(TileVisual visual)
        {
            Tile tile = new Tile(visual);
            tile.intPosition = visual.IntPosition;

            visual.Initialize(tile);

            return tile;
        }

        public T GetComponent<T>() where T : TileComponent
        {
            TileComponent component;
            tileComponents.TryGetValue(typeof(T), out component);
            return component as T;
        }

        public void SetOccupier(IOccupier occupier)
        {
            this.occupier = occupier;
        }

        public void Disable()
        {
            visual.Hide();
            GameStepController.Instance.OnStaticStep -= OnStaticStep;
        }

        public void Enable()
        {
            GameStepController.Instance.OnStaticStep += OnStaticStep;
            visual.Show();
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
            FieldController.Instance.OverrideTileAtPosition(intPosition, null);
            GameStepController.Instance.OnStaticStep -= OnStaticStep;
        }
    }
}
