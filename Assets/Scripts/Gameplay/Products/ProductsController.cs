using JP.Framework.Flow;
using System.Collections.Generic;

namespace Gameplay.Products
{
    public class ProductsController : SingletonBehaviour<ProductsController>
    {
        public Product CreateProduct(ProductData productData)
        {
            return CreateProduct(new List<ProductData>() { productData });
        }

        public Product CreateProduct(List<ProductData> productDatas)
        {
            Product product;

            if (productDatas.Count > 1)
                product = new MixedProduct(productDatas);
            else
                product = new SingleProduct(productDatas[0]);

            return product;
        }
    }
}
