using System.Collections.Generic;
using UnityEngine;
using Utility;

public class Field : SingletonBehaviour<Field>
{
    [SerializeField]
    private Transform tiles;

    private Dictionary<Vector3Int, Tile> positionsToTiles;
    
    void Awake()
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
            Vector3Int intPosition = tileVisual.IntPosition;
            positionsToTiles[intPosition] = tile;
        }
    }
}
