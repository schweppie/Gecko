using JP.Framework.Flow;
using UnityEngine;

namespace Gameplay.Products
{
    public class ProductsController : SingletonBehaviour<ProductsController>
    {
        public Product CreateProduct(ProductData productData, Transform positionTransform)
        {
            Product product;

            if (productData.GetType() == typeof(MixedProductData))
            {
                MixedProductData mixedProductData = productData as MixedProductData;
                product = new MixedProduct(mixedProductData.ProductDatas);
            }
            else
            {
                product = new SingleProduct(productData);                    
            }

            return product;
        }
    }
}
