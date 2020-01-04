using System;
using System.Collections.Generic;
using Gameplay.Tiles.Components;
using UnityEngine;

namespace Gameplay.Tiles
{
    public class Tile
    {
        private TileVisual visual;

        private Vector3Int intPosition;
        public Vector3Int IntPosition => intPosition;

        public TileType Type { get; }

        private Dictionary<Type, TileComponent> tileComponents;
        
        public Tile(TileVisual visual)
        {
            this.visual = visual;
            tileComponents = new Dictionary<Type, TileComponent>();
            foreach (TileComponent tileComponent in visual.TileComponents)
                tileComponents[tileComponent.GetType()] = tileComponent;
            
            GameStepController.Instance.OnStaticForwardStep += OnStaticForwardStep;
            GameStepController.Instance.OnStaticBackwardStep += OnStaticBackwardStep;
        }

        private void OnStaticForwardStep(int step)
        {
            foreach (var tileComponent in tileComponents.Values)
                tileComponent.DoNextStep();            
        }
        
        private void OnStaticBackwardStep(int step)
        {
            foreach (var tileComponent in tileComponents.Values)
                tileComponent.DoPrevStep();            
        }        

        /// <summary>
        /// Normally the data (this tile) is created first before the visual, but as we want to build levels in the
        /// Unity editor in scenes, the visuals already exists while the data does not yet 
        /// </summary>
        public static Tile ConstructTileFromVisual(TileVisual visual)
        {
            Tile tile = new Tile(visual);
            
            tile.visual = visual;
            tile.intPosition = visual.IntPosition;
            
            visual.LinkTile(tile);
            
            return tile;
        }

        public T GetComponent<T>() where T : TileComponent
        {
            TileComponent component;
            tileComponents.TryGetValue(typeof(T), out component);
            return component as T;
        }
    }
}
