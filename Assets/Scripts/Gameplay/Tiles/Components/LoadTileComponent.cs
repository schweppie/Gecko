using System.Collections.Generic;
using Gameplay.Products;

namespace Gameplay.Tiles.Components
{
    public class LoadTileComponent : TileComponent
    {
        private Stack<Product> loadableProducts;
        private IProductProducer productProducer;

        public override void Initialize(Tile tile)
        {
            base.Initialize(tile);
            
            loadableProducts = new Stack<Product>();
        }

        public void SetProductProducer(IProductProducer productProducer)
        {
            this.productProducer = productProducer;
        }
        
        public void AddProductToStack(Product product)
        {
            loadableProducts.Push(product);
        }

        public bool CanLoadProduct()
        {
            if (productProducer != null)
                return productProducer.CanProduceProduct();
            
            return loadableProducts.Count > 0;
        }

        public Product LoadProduct()
        {
            if (productProducer != null)
                return productProducer.ProduceProduct();
            
            return loadableProducts.Pop();
        }
    }
}
