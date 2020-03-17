using Gameplay.Products;
using Gameplay.Tiles.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Stations.Components
{
    public class SplitterStationComponent : StationComponent, IProductReceiver
    {
        [SerializeField]
        private UnloadTileComponent unloadTile;

        [SerializeField]
        private LoadTileComponent splittedProductLoadTile;

        [SerializeField]
        private LoadTileComponent restProductLoadTile;

        [SerializeField]
        private SingleProductDefinition splitProductDefinition;

        public override void Initialize(Station station)
        {
            base.Initialize(station);

            unloadTile.SetProductReceiver(this);
        }

        public bool CanReceiveProduct(Product product)
        {
            return product.IsMixedProduct;
        }

        public void ReceiveProduct(Product product)
        {
            // Is always a mixed product, since it is checked using `CanReceiveProduct`
            MixedProduct mixedProduct = product as MixedProduct;

            while (mixedProduct.ContainsProduct(splitProductDefinition))
            {
                Product splitProduct = ProductsController.Instance.CreateProduct(splitProductDefinition, GetReceiverTransform());
                splittedProductLoadTile.AddProductToStack(splitProduct);
                mixedProduct.RemoveProduct(splitProductDefinition);
            }

            restProductLoadTile.AddProductToStack(mixedProduct);
        }

        public Transform GetReceiverTransform()
        {
            return station.Visual.transform;
        }
    }
}
