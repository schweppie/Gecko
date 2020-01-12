using Gameplay.Tiles.Components;
using JP.Framework.Extensions;
using UnityEngine;

namespace Gameplay.Tiles
{
    public class TileVisual : MonoBehaviour
    {
        private TileComponent[] tileComponents;
        public TileComponent[] TileComponents => tileComponents;
        
        private Tile tile;

        private void Awake()
        {
            tileComponents = GetComponents<TileComponent>();
        }

        public void LinkTile(Tile tile)
        {
            this.tile = tile;

            for (int i = 0; i < tileComponents.Length; i++)
                tileComponents[i].SetTile(tile);
        }
        
        public Vector3Int IntPosition => transform.position.ToIntVector();
    }
}
