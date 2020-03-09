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
        private ProductData siloProduct;
        
        private Stack<Product> products;

        private void Start()
        {
            loadTileComponent.SetProductProducer(this);
            loadTileComponent.SetOutputProduct(siloProduct);
            
            unloadTileComponent.SetProductReceiver(this);
            unloadTileComponent.SetInputProduct(siloProduct);

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
            return station.Visual.transform;
        }

        public bool CanProduceProduct(ProductData productData)
        {
            return products.Count > 0 && productData.GetType() == siloProduct.GetType();
        }

        public Product ProduceProduct(ProductData productData)
        {
            return products.Pop();
        }
    }
}
