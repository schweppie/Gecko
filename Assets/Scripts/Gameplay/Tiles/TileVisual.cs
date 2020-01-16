using Gameplay.Tiles.Components;
using JP.Framework.Extensions;
using UnityEngine;

namespace Gameplay.Tiles
{
    public class TileVisual : MonoBehaviour
    {
        [SerializeField]
        private bool needsBottom = false;

        private TileComponent[] tileComponents;
        public TileComponent[] TileComponents => tileComponents;
        
        private Tile tile;

        private GameObject tileBottomVisualInstance;

        private void Awake()
        {
            tileComponents = GetComponents<TileComponent>();
            FieldController.Instance.OnUpdateVisualsEvent += OnUpdateVisuals;
        }

        private void OnUpdateVisuals()
        {
            if (!needsBottom)
                return;

            if (FieldController.Instance.GetTileAtIntPosition(IntPosition + Vector3Int.down).GetComponent<BlockingTileComponent>() == null 
                && tileBottomVisualInstance == null)
            {
                tileBottomVisualInstance = Instantiate(FieldController.Instance.TileBottomVisualPrefab);
                tileBottomVisualInstance.transform.SetParent(transform, false);
            }
            else
                Destroy(tileBottomVisualInstance);
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
