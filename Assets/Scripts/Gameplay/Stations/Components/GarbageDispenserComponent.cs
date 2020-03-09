using System.Collections.Generic;
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

        Product IProductProducer.ProduceProduct(ProductData productData)
        {
            // TODO: ProductsController should be responsible for instantiating
            // product objects and product visual objects, should not be in station
            MixedProduct mixedProduct = ProductsController.Instance.CreateProduct(productData, productSpawnPoint) as MixedProduct;
/*
            GameObject newProductVisualGO = Instantiate(mixedProductVisual.gameObject);
            var newProductVisual = newProductVisualGO.GetComponent<ProductVisual>();
            mixedProduct.SetVisual(newProductVisual);
            var newMixedProductVisual = newProductVisualGO.GetComponent<MixedProductVisual>();
            newMixedProductVisual.SetupMixedProduct();
            newProductVisualGO.transform.position = productSpawnPoint.position;
            newProductVisualGO.transform.rotation = productSpawnPoint.rotation;
            
            */
            
            
            return mixedProduct;
        }

        public bool CanProduceProduct(ProductData productData)
        {
            return true;
        }
    }
}
