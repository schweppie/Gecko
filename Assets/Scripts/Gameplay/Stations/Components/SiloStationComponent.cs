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

        private Stack<Product> products;

        private void Start()
        {
            loadTileComponent.ProductProducer = this;
            unloadTileComponent.ProductReceiver = this;

            products = new Stack<Product>();
        }

        public bool CanReceiveProduct()
        {
            return true;            
        }

        public void ReceiveProduct(Product product)
        {
            products.Push(product);
        }

        public Product ProduceProduct()
        {
            return products.Pop();
        }

        public bool CanProduceProduct()
        {
            return products.Count > 0;
        }

        public Transform GetReceiverTransform()
        {
            return station.Visual.transform;
        }
    }
}