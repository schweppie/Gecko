using System.Collections.Generic;

namespace Gameplay.Products
{
    public class SingleProduct : Product
    {
        private ProductDefinition productDefinition;
        public ProductDefinition ProductDefinition => productDefinition;

        public override bool IsMixedProduct => false;

        public override bool ContainsProduct(SingleProductDefinition productDefinition)
        {
            return this.productDefinition == productDefinition;
        }

        public override List<SingleProductDefinition> GetProductDatas()
        {
            return new List<SingleProductDefinition>() {productDefinition as SingleProductDefinition};
        }

        public SingleProduct(ProductDefinition productDefinition)
        {
            this.productDefinition = productDefinition;
        }
    }
}
