using DG.Tweening;
using Gameplay.Field;
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
        public TileComponent[] TileComponents
        {
            get
            {
                if (tileComponents == null)
                    tileComponents = GetComponents<TileComponent>();

                return tileComponents;
            }
        }

        private Tile tile;

        private MeshRenderer[] renderers;

        private GameObject tileBottomVisualInstance;

        private void Awake()
        {
            tileComponents = GetComponents<TileComponent>();
            renderers = GetComponentsInChildren<MeshRenderer>();
            FieldController.Instance.OnUpdateVisualsEvent += OnUpdateVisuals;
        }

        public void Hide()
        {
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].enabled = false;
        }

        public void Show()
        {
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].enabled = true;
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

        public void Initialize(Tile tile)
        {
            this.tile = tile;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            tile.OnDispose += OnDispose;
        }

        private void UnsubscribeEvents()
        {
            tile.OnDispose -= OnDispose;
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        public Vector3Int IntPosition => transform.position.ToIntVector();

        [ContextMenu("Force Remove")]
        private void ForceRemove()
        {
            if (Application.isPlaying)
                tile.RemoveTile();
        }

        public void OnDispose()
        {
            transform.DOScale(0, 1f)
                .OnComplete(() => Destroy(gameObject));
        }
    }
}
