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

        private TileComponent[] tileComponents;
        
        public Tile(TileVisual visual)
        {
            this.visual = visual;
            tileComponents = visual.TileComponents;
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

        public void DoStep()
        {
            for (int i = 0; i < tileComponents.Length; i++)
                tileComponents[i].DoStep();
        }
    }
}
