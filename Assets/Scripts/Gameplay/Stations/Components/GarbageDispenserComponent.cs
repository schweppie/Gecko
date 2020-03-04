using System.Collections.Generic;
using Gameplay.Products;
using Gameplay.Tiles.Components;
using UnityEngine;

namespace Gameplay.Stations.Components
{
    public class GarbageDispenserComponent : StationComponent, IProductProducer
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
            loadTileComponent.ProductProducer = this;
        }

        Product IProductProducer.ProduceProduct()
        {
            // TODO: ProductsController should be responsible for instantiating
            // product objects and product visual objects, should not be in station
            MixedProduct mixedProduct = ProductsController.Instance.CreateProduct(products) as MixedProduct;

            GameObject newProductVisualGO = Instantiate(mixedProductVisual.gameObject);
            var newProductVisual = newProductVisualGO.GetComponent<ProductVisual>();
            mixedProduct.SetVisual(newProductVisual);
            var newMixedProductVisual = newProductVisualGO.GetComponent<MixedProductVisual>();
            newMixedProductVisual.SetupMixedProduct();
            newProductVisualGO.transform.position = productSpawnPoint.position;
            newProductVisualGO.transform.rotation = productSpawnPoint.rotation;
            return mixedProduct;
        }

        public bool CanProduceProduct()
        {
            return true;
        }
    }
}
