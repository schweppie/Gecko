﻿using System.Collections.Generic;
using Gameplay;
using Gameplay.Tiles;
using Gameplay.Tiles.Components;
using UnityEngine;
using Utility;

public class FieldController : SingletonBehaviour<FieldController>
{
    [SerializeField]
    private Transform tiles;
    
    [SerializeField]
    private TileVisual emptyTileVisualPrefab;

    private Dictionary<Vector3Int, Tile> positionsToTiles;

    void Awake()
    {
        Initialize();
        GameStepController.Instance.OnForwardStep += OnForwardStep;
    }

    private void OnForwardStep(int step)
    {
        foreach (var tile in positionsToTiles.Values)
            tile.DoStep();        
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
}