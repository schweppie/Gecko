using Gameplay.Products;
using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public class UnloadTileComponent : TileComponent
    {
        [SerializeField]
        private ProductData productData = null; 
        
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
            //productReceiver.ProduceProduct(productData);
        }
      
    }
}
