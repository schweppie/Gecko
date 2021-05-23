using System.Collections.Generic;
using Gameplay.Stations;
using System.Linq;
using Gameplay.Tiles;
using Gameplay.Tiles.Components;
using JP.Framework.Extensions;
using JP.Framework.Flow;
using UnityEngine;

namespace Gameplay.Field
{
    public class FieldController : SingletonBehaviour<FieldController>
    {
        [SerializeField]
        private Transform tiles = null;

        [SerializeField]
        private Transform stations = null;

        [SerializeField]
        private TileVisual emptyTileVisualPrefab = null;

        [SerializeField]
        private GameObject tileBottomVisualPrefab;
        public GameObject TileBottomVisualPrefab => tileBottomVisualPrefab;

        private Dictionary<Vector3Int, Tile> positionsToTiles;

        private BoundsInt bounds;
        public BoundsInt Bounds => bounds;

        public delegate void UpdateVisualsDelegate();
        public event UpdateVisualsDelegate OnUpdateVisualsEvent;

        private Dictionary<Vector3Int, IOccupier> occupationBuffer = new Dictionary<Vector3Int, IOccupier>();
        public Dictionary<Vector3Int, IOccupier> OccupationBuffer => occupationBuffer;

        private Dictionary<Vector3Int, IOccupier> oldOccupationBuffer = new Dictionary<Vector3Int, IOccupier>();
        public Dictionary<Vector3Int, IOccupier> OldOccupationBuffer => oldOccupationBuffer;


        [SerializeField]
        private StationVisual tempInstantiateTestVisual;

        // For debugging
        private int emptyTiles = 0;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            InitializeTiles();
            InitializeStations();
        }

        private void InitializeTiles()
        {
            positionsToTiles = new Dictionary<Vector3Int, Tile>();
            foreach (Transform tileTransform in tiles.transform)
            {
                TileVisual tileVisual = tileTransform.GetComponent<TileVisual>();
                if (tileVisual == null)
                {
                    Debug.LogError("Did not find component '" + nameof(TileVisual) + "' on tile", tileTransform);
                    continue;
                }

                Tile tile = Tile.ConstructTileFromVisual(tileVisual);

                if (positionsToTiles.ContainsKey(tile.IntPosition))
                    Debug.LogError("Tile already exists!" + tile.IntPosition);

                positionsToTiles[tile.IntPosition] = tile;

                if (!bounds.Contains(tile.IntPosition))
                {
                    bounds.Add(tile.IntPosition);
                }
            }

            OnUpdateVisualsEvent?.Invoke();
        }

        private void InitializeStations()
        {
            StationVisual[] stationVisuals = stations.GetComponentsInChildren<StationVisual>();

            foreach (StationVisual stationVisual in stationVisuals)
                AddStationFromVisual(stationVisual);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
                DebugAddStation();
        }

        public void DebugAddStation()
        {
            StationVisual stationVisualInstance = Instantiate(tempInstantiateTestVisual, new Vector3(8,2,24), Quaternion.identity, stations.transform);
            AddStationFromVisual(stationVisualInstance);
        }

        public void AddStationFromVisual(StationVisual stationVisual)
        {
            Station station = Station.ConstructStationFromVisual(stationVisual);
        }

        public Tile GetTileAtIntPosition(Vector3Int intPosition)
        {
            if (positionsToTiles.ContainsKey(intPosition))
                return positionsToTiles[intPosition];

            emptyTiles++;

            TileVisual emptyTile = Instantiate(emptyTileVisualPrefab);
            emptyTile.name = "EmptyTile " + emptyTiles;
            emptyTile.transform.position = intPosition;
            Tile tile = Tile.ConstructTileFromVisual(emptyTile);
            positionsToTiles[tile.IntPosition] = tile;
            return tile;
        }

        /// <summary>
        /// used to place a new tile on an existing positions
        /// returns the old overriden tile
        /// </summary>
        public Tile OverrideTileAtPosition(Vector3Int position, Tile newTile)
        {
            positionsToTiles.TryGetValue(position, out Tile oldTile);

            if (newTile != null)
                positionsToTiles[position] = newTile;
            else
                positionsToTiles.Remove(position);
            return oldTile;
        }

        public Tile GetTileAtOrBelowIntPosition(Vector3Int intPosition)
        {
            // This should probably be cached when new tiles have been added/after initial initialization
            var tilesBelow = positionsToTiles.Where(i => i.Key.y <= intPosition.y && i.Key.x == intPosition.x && i.Key.z == intPosition.z && i.Value.GetComponent<EmptyTileComponent>() == null);
            tilesBelow = tilesBelow.OrderByDescending(i => i.Key.y);

            return tilesBelow.FirstOrDefault().Value;
        }

        public Tile GetTileBelowIntPosition(Vector3Int intPosition)
        {
            // This should probably be cached when new tiles have been added/after initial initialization
            var tilesBelow = positionsToTiles.Where(i => i.Key.y < intPosition.y && i.Key.x == intPosition.x && i.Key.z == intPosition.z && i.Value.GetComponent<EmptyTileComponent>() == null);
            tilesBelow = tilesBelow.OrderByDescending(i => i.Key.y);

            return tilesBelow.FirstOrDefault().Value;
        }

        public Tile GetTileAboveIntPosition(Vector3Int intPosition)
        {
            // This should probably be cached when new tiles have been added/after initial initialization
            var tilesBelow = positionsToTiles.Where(i => i.Key.y > intPosition.y && i.Key.x == intPosition.x && i.Key.z == intPosition.z && i.Value.GetComponent<EmptyTileComponent>() == null);
            tilesBelow = tilesBelow.OrderBy(i => i.Key.y);

            return tilesBelow.FirstOrDefault().Value;
        }

        public IOccupier GetOccupierAt(Vector3Int position)
        {
            if (occupationBuffer.ContainsKey(position))
                return occupationBuffer[position];

            return null;
        }

        public IOccupier GetOldOccupierAt(Vector3Int position)
        {
            if (oldOccupationBuffer.ContainsKey(position))
                return oldOccupationBuffer[position];

            return null;
        }

        public void AddOccupier(Vector3Int position, IOccupier occupier)
        {
            occupationBuffer[position] = occupier;
        }

        public void RemoveOccupier(Vector3Int position)
        {
            occupationBuffer.Remove(position);
        }

        public void ClearBuffers()
        {
            oldOccupationBuffer.Clear();
            foreach (var pair in occupationBuffer)
                oldOccupationBuffer.Add(pair.Key, pair.Value);
            occupationBuffer.Clear();
        }

        public void WriteOccupiersToTiles()
        {
            ClearTileOccupations();

            foreach (var pair in occupationBuffer)
            {
                Tile tile = GetTileAtIntPosition(pair.Key);
                tile.SetOccupier(pair.Value);
            }
        }

        private void ClearTileOccupations()
        {
            foreach (var pair in positionsToTiles)
                pair.Value.SetOccupier(null);
        }
    }
}
