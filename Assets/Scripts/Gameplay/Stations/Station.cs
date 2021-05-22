using System;
using System.Collections.Generic;
using Gameplay.Field;
using Gameplay.Stations.Components;
using Gameplay.Tiles;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.Stations
{
    public class Station
    {
        private StationVisual visual;
        public StationVisual Visual => visual;

        private List<Tile> stationTiles;
        private List<Tile> oldOverriddenTiles;

        private Dictionary<Type, StationComponent> stationComponents;

        public Station(StationVisual visual)
        {
            this.visual = visual;
            visual.Initialize(this);

            stationComponents = new Dictionary<Type, StationComponent>();
            foreach (StationComponent stationComponent in visual.StationComponents)
                stationComponents[stationComponent.GetType()] = stationComponent;

            CreateStationTiles();
            GameStepController.Instance.OnStaticStep += OnStaticStep;
        }

        private void CreateStationTiles()
        {
            stationTiles = new List<Tile>();
            oldOverriddenTiles = new List<Tile>();
            foreach (TileVisual localTileVisual in visual.LocalTileVisuals)
            {
                Tile newTile = Tile.ConstructTileFromVisual(localTileVisual);
                stationTiles.Add(newTile);
                Vector3Int globalTilePosition = localTileVisual.IntPosition;
                Tile oldTile = FieldController.Instance.OverrideTileAtPosition(globalTilePosition, newTile);

                // There isn't always an existing tile already on a station's tile
                if (oldTile == null)
                    continue;

                oldTile.Disable();
                oldOverriddenTiles.Add(oldTile);
            }
        }

        private void OnStaticStep(int step)
        {
            foreach (var stationComponent in stationComponents.Values)
                stationComponent.DoNextStep();
        }

        public void TestRemove()
        {
            for (int i = 0; i < stationTiles.Count; i++)
            {
                FieldController.Instance.OverrideTileAtPosition(stationTiles[i].IntPosition, oldOverriddenTiles[i]);
                stationTiles[i].Disable();
            }
            stationTiles.Clear();
            oldOverriddenTiles.Clear();
            GameStepController.Instance.OnStaticStep -= OnStaticStep;
            Object.Destroy(visual.gameObject);
        }

        /// <summary>
        /// Normally the data (this station) is created first before the visual, but as we want to build levels in the
        /// Unity editor in scenes, the visuals already exists while the data does not yet
        /// </summary>
        public static Station ConstructStationFromVisual(StationVisual visual)
        {
            Station station = new Station(visual);
            return station;
        }
    }
}
