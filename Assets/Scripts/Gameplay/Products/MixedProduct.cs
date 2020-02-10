using System.Collections.Generic;

namespace Gameplay.Products
{
    public class MixedProduct : Product
    {
        public List<ProductData> ProductDatas { get; }

        public override bool IsMixedProduct => true;

        public override bool ContainsProduct(ProductData productData)
        {
            return ProductDatas.Contains(productData);
        }

        public MixedProduct(List<ProductData> productDatas)
        {
            this.ProductDatas = productDatas;
        }
    }
}
