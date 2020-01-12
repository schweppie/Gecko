using System.Collections.Generic;
using Gameplay.Tiles;
using UnityEngine;

namespace Gameplay.Stations
{
    public class Station : IOccupier
    {
        private StationVisual visual;

        private List<Tile> stationTiles;
        private List<Tile> oldOverriddenTiles;

        public Station(StationVisual visual)
        {
            this.visual = visual;
            visual.Initialize(this);
            CreateStationTiles();
            GameStepController.Instance.OnStaticForwardStep += OnStaticForwardStep;
            GameStepController.Instance.OnStaticBackwardStep += OnStaticBackwardStep;
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
                oldTile.Disable();
                oldOverriddenTiles.Add(oldTile);
            }
        }

        private void OnStaticForwardStep(int step)
        {
        }

        private void OnStaticBackwardStep(int step)
        {
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
            GameStepController.Instance.OnStaticForwardStep -= OnStaticForwardStep;
            GameStepController.Instance.OnStaticBackwardStep -= OnStaticBackwardStep;
            Object.Destroy(visual.gameObject);
        }

        public void PickNewStrategy()
        {
            throw new System.NotImplementedException();
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
