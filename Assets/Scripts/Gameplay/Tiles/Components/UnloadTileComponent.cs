using Gameplay.Products;
using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public class UnloadTileComponent : TileComponent
    {
        private ProductData productData = null;

        public void SetInputProduct(ProductData productData)
        {
            this.productData = productData;
        }
        
        private IProductReceiver productReceiver;

        public void SetProductReceiver(IProductReceiver productReceiver)
        {
            this.productReceiver = productReceiver;
        }

        public bool CanUnloadProduct(Product product)
        {
            return productReceiver.CanReceiveProduct(product);
        }

        public void UnloadProduct(Product product)
        {
            productReceiver.ReceiveProduct(product);
        }

        public Transform GetReceiverTransform()
        {
            return productReceiver.GetReceiverTransform();
        }
      
    }
}
