using Gameplay.Products;
using Gameplay.Tiles.Components;
using UnityEngine;

namespace Gameplay.Stations.Components
{
    public class GarbageDispenserComponent : StationComponent, IProductProducer
    {
        [SerializeField]
        private MixedProductData mixedProductData;

        [SerializeField]
        private LoadTileComponent loadTileComponent;

        [SerializeField]
        private Transform productSpawnPoint;

        private void Start()
        {
            loadTileComponent.SetProductProducer(this);
            loadTileComponent.SetOutputProduct(mixedProductData);
        }

        public Product ProduceProduct(ProductData productData)
        {
            return ProductsController.Instance.CreateProduct(productData, productSpawnPoint);
        }

        public bool CanProduceProduct(ProductData productData)
        {
            return true;
        }
    }
}
