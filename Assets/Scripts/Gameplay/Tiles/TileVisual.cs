using Gameplay.Tiles.Components;
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
        
        public Vector3Int IntPosition
        {
            get
            {
                Vector3 position = transform.position;
                int x = Mathf.RoundToInt(position.x);
                int y = Mathf.RoundToInt(position.y);
                int z = Mathf.RoundToInt(position.z);
                return new Vector3Int(x, y, z);
            }
        }
    }
}
