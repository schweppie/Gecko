namespace Gameplay.Products
{
    public class SingleProduct : Product
    {
        private ProductData productData;

        public override bool IsMixedProduct => false;

        public override bool ContainsProduct(ProductData productData)
        {
            return this.productData == productData;
        }

        public SingleProduct(ProductData productData)
        {
            this.productData = productData;
        }
    }
}
