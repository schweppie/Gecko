using Gameplay.Products;
using Gameplay.Tiles.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Stations.Components
{
    public class GarbageDispenserComponent : StationComponent, IProductProducer
    {
        [SerializeField]
        private MixedProductDefinition mixedProductDefinition;

        [SerializeField]
        private LoadTileComponent loadTileComponent;

        [SerializeField]
        private Transform productSpawnPoint;

        private void Start()
        {
            loadTileComponent.SetProductProducer(this);
        }

        public Product ProduceProduct()
        {
            return ProductsController.Instance.CreateProduct(mixedProductDefinition, productSpawnPoint);
        }

        public bool CanProduceProduct()
        {
            return true;
        }
    }
}
