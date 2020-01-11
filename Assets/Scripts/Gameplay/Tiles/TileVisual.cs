﻿using Gameplay.Tiles.Components;
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

            if (FieldController.Instance.GetTileAtIntPosition(IntPosition + Vector3Int.down).GetComponent<BlockingTileComponent>() == null)
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
