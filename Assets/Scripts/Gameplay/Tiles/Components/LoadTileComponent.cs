using Gameplay.Products;
using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public class LoadTileComponent : TileComponent
    {
        private ProductData productData = null;

        public void SetOutputProduct(ProductData productData)
        {
            this.productData = productData;
        }
        
        private IProductProducer productProducer;

        public void SetProductProducer(IProductProducer productProducer)
        {
            this.productProducer = productProducer;
        }

        public bool CanLoadProduct()
        {
            return productProducer.CanProduceProduct(productData);
        }

        public Product LoadProduct()
        {
            return productProducer.ProduceProduct(productData);
        }
    }
}
