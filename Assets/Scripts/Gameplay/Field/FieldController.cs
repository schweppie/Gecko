using System.Collections.Generic;
using Gameplay.Tiles;
using JP.Framework.Flow;
using UnityEngine;

namespace Gameplay.Field
{
    public class FieldController : SingletonBehaviour<FieldController>
    {
        [SerializeField]
        private Transform tiles = null;
    
        [SerializeField]
        private TileVisual emptyTileVisualPrefab = null;

        private Dictionary<Vector3Int, Tile> positionsToTiles;
        private FieldResolver fieldResolver;
        public FieldResolver FieldResolver => fieldResolver;

        void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            positionsToTiles = new Dictionary<Vector3Int, Tile>();
            fieldResolver = new FieldResolver();
            
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
}
