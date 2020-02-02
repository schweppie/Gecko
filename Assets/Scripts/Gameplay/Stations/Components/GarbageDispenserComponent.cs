using System.Collections.Generic;
using Gameplay.Products;
using Gameplay.Tiles.Components;
using UnityEngine;

namespace Gameplay.Stations.Components
{
    public class GarbageDispenserComponent : StationComponent
    {
        [SerializeField]
        private List<ProductData> products;

        [SerializeField]
        private MixedProductVisual mixedProductVisual;

        [SerializeField]
        private LoadTileComponent loadTileComponent;

        [SerializeField]
        private Transform productSpawnPoint;

        private void Start()
        {
            loadTileComponent.GarbageDispenserComponent = this;
        }

        public ProductVisual CreateProductVisual()
        {
            GameObject newProductVisualGO = Instantiate(mixedProductVisual.gameObject);
            var newProductVisual = newProductVisualGO.GetComponent<ProductVisual>();
            newProductVisual.Initialize(new MixedProduct(products));
            var newMixedProductVisual = newProductVisualGO.GetComponent<MixedProductVisual>();
            newMixedProductVisual.SetupMixedProduct();
            newProductVisualGO.transform.position = productSpawnPoint.position;
            newProductVisualGO.transform.rotation = productSpawnPoint.rotation;
            return newProductVisual;
        }
    }
}
