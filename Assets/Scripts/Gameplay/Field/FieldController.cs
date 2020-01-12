using System.Collections.Generic;
using Gameplay.Stations;
using Gameplay.Tiles;
using JP.Framework.Flow;
using UnityEngine;

public class FieldController : SingletonBehaviour<FieldController>
{
    [SerializeField]
    private Transform tiles = null;

    [SerializeField]
    private Transform stations = null;

    [SerializeField]
    private TileVisual emptyTileVisualPrefab = null;

    private Dictionary<Vector3Int, Tile> positionsToTiles;

    void Awake()
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
            positionsToTiles[tile.IntPosition] = tile;
        }
    }

    private void InitializeStations()
    {
        foreach (Transform stationTransform in stations.transform)
        {
            StationVisual stationVisual = stationTransform.GetComponent<StationVisual>();
            if (stationVisual == null)
            {
                Debug.LogError("Did not find component '" + nameof(StationVisual) + "' on station", stationTransform);
                continue;
            }

            Station station = Station.ConstructStationFromVisual(stationVisual);
        }
    }

    public void ClearTileOccupations()
    {
        foreach(var pair in positionsToTiles)
            pair.Value.SetOccupier(null);
    }

    public Tile GetTileAtIntPosition(Vector3Int intPosition)
    {
        if (positionsToTiles.ContainsKey(intPosition))
            return positionsToTiles[intPosition];

        TileVisual emptyTile = Instantiate(emptyTileVisualPrefab);
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
        positionsToTiles[position] = newTile;
        return oldTile;
    }
}
