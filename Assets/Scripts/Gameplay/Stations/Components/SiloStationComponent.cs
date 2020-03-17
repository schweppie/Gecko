using Gameplay.Products;
using Gameplay.Tiles.Components;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Stations.Components
{
    public class SiloStationComponent : StationComponent, IProductReceiver, IProductProducer
    {
        [SerializeField]
        private UnloadTileComponent unloadTileComponent;

        [SerializeField]
        private LoadTileComponent loadTileComponent;

        [SerializeField]
        private SingleProductDefinition siloProduct;

        [SerializeField]
        private Transform productReceiveTransform;
        
        private Stack<Product> products;

        private void Start()
        {
            loadTileComponent.SetProductProducer(this);
            unloadTileComponent.SetProductReceiver(this);

            products = new Stack<Product>();
        }

        public bool CanReceiveProduct(Product product)
        {
            return !product.IsMixedProduct && product.ContainsProduct(siloProduct);
        }

        public void ReceiveProduct(Product product)
        {
            products.Push(product);
        }

        public Transform GetReceiverTransform()
        {
            return productReceiveTransform;
        }

        public bool CanProduceProduct()
        {
            return products.Count > 0;
        }

        public Product ProduceProduct()
        {
            return products.Pop();
        }
    }
}
