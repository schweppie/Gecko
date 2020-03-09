using System.Collections.Generic;

namespace Gameplay.Products
{
    public class SingleProduct : Product
    {
        private ProductData productData;
        public ProductData ProductData => productData;

        public override bool IsMixedProduct => false;

        public override bool ContainsProduct(SingleProductData productData)
        {
            return this.productData == productData;
        }

        public override List<SingleProductData> GetProductDatas()
        {
            return new List<SingleProductData>() {productData as SingleProductData};
        }

        public SingleProduct(ProductData productData)
        {
            this.productData = productData;
        }
    }
}
