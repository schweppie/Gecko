using System.Collections.Generic;
using Gameplay.Products;
using Gameplay.Tiles.Components;
using UnityEngine;

namespace Gameplay.Stations.Components
{
    public class SplitterStationComponent : StationComponent, IProductProducer, IProductReceiver
    {
        [SerializeField]
        private UnloadTileComponent unloadTile;
        
        [SerializeField]
        private LoadTileComponent splittedProductLoadTile;
        
        [SerializeField]
        private LoadTileComponent restProductLoadTile;

        [SerializeField]
        private ProductData splitProductData;

        [SerializeField]
        private ProductData mixedProductData;
        
        private Stack<Product> splitProducts;
        private Stack<MixedProduct> mixedRestProducts;
        
        public override void Initialize(Station station)
        {
            base.Initialize(station);
            
            splitProducts = new Stack<Product>();
            mixedRestProducts = new Stack<MixedProduct>();
            
            unloadTile.SetInputProduct(mixedProductData);
            unloadTile.SetProductReceiver(this);
            
            splittedProductLoadTile.SetOutputProduct(splitProductData);
            splittedProductLoadTile.SetProductProducer(this);
            
            restProductLoadTile.SetOutputProduct(mixedProductData);
            restProductLoadTile.SetProductProducer(this);
        }

        public bool CanProduceProduct(ProductData productData)
        {
            if (productData.GetType() == mixedProductData.GetType())
                return mixedRestProducts.Count > 0;

            if (productData.GetType() == splitProductData.GetType())
                return splitProducts.Count > 0;

            return false;
        }

        public Product ProduceProduct(ProductData productData)
        {
            if (productData.GetType() == mixedProductData.GetType())
                return mixedRestProducts.Pop();

            return splitProducts.Pop();
        }

        public bool CanReceiveProduct(Product product)
        {
            return product.IsMixedProduct;
        }

        public void ReceiveProduct(Product product)
        {
            // Is always a mixed product, since it is checked using `CanReceiveProduct`
            MixedProduct mixedProduct = product as MixedProduct;

            while (mixedProduct.ContainsProduct(splitProductData))
            {
                Product splitProduct = ProductsController.Instance.CreateProduct(splitProductData, GetReceiverTransform());
                splitProducts.Push(splitProduct);
                mixedProduct.RemoveProduct(splitProductData);
            }
            
            mixedRestProducts.Push(mixedProduct);
        }

        public Transform GetReceiverTransform()
        {
            return station.Visual.transform;
        }
    }
}
