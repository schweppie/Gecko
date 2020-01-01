using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

public class Field : SingletonBehaviour<Field>
{
    [SerializeField]
    private Transform tiles;

    [SerializeField]
    private GameObject testRobotPrefab;

    private Dictionary<Vector3Int, Tile> positionsToTiles;
    
    void Awake()
    {
        Initialize();
        InvokeRepeating("SpawnRobotForTesting", 0,1);
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
            positionsToTiles[tile.IntPosition] = tile;
        }
    }

    private void SpawnRobotForTesting()
    {
        Tile startTile = positionsToTiles.First().Value;
        GameObject robot = GameObject.Instantiate(testRobotPrefab);
        robot.GetComponent<RobotVisual>().CurrentTile = startTile;
    }

    public Tile GetTileAtIntPosition(Vector3Int intPosition)
    {
        if (positionsToTiles.ContainsKey(intPosition))
            return positionsToTiles[intPosition];
        return null;
    }
}
