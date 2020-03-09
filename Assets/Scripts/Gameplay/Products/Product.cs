using System.Collections.Generic;

namespace Gameplay.Products
{
    public abstract class Product : ICarryable
    {
        public abstract bool IsMixedProduct { get; }
        public abstract bool ContainsProduct(SingleProductData productData);
        public abstract List<SingleProductData> GetProductDatas();

        private ProductVisual visual;
        public ProductVisual Visual => visual;

        public void SetVisual(ProductVisual visual)
        {
            this.visual = visual;
        }
    }
}
