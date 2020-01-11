using System.Collections.Generic;
using System.Linq;
using Gameplay.Tiles;
using Gameplay.Tiles.Components;
using JP.Framework.Extensions;
using JP.Framework.Flow;
using UnityEngine;

public class FieldController : SingletonBehaviour<FieldController>
{
    [SerializeField]
    private Transform tiles = null;
    
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

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
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

    public Tile GetTileBelowIntPosition(Vector3Int intPosition)
    {
        // This should probably be cached when new tiles have been added/after initial initialization
        var tilesBelow = positionsToTiles.Where(x => x.Key.y <= intPosition.y && x.Key.x == intPosition.x && x.Key.z == intPosition.z && x.Value.GetComponent<EmptyTileComponent>() == null);
        tilesBelow = tilesBelow.OrderByDescending(x => x.Key.y);

        return tilesBelow.FirstOrDefault().Value;
    }

    public Tile GetTileAboveIntPosition(Vector3Int intPosition)
    {
        // This should probably be cached when new tiles have been added/after initial initialization
        var tilesBelow = positionsToTiles.Where(x => x.Key.y > intPosition.y && x.Key.x == intPosition.x && x.Key.z == intPosition.z && x.Value.GetComponent<EmptyTileComponent>() == null);
        tilesBelow = tilesBelow.OrderBy(x => x.Key.y);

        return tilesBelow.FirstOrDefault().Value;
    }
}
