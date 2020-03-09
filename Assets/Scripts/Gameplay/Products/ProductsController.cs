using JP.Framework.Flow;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Products
{
    public class ProductsController : SingletonBehaviour<ProductsController>
    {
        public Product CreateProduct(ProductData productData, Transform positionTransform)
        {
            return CreateProduct(new List<ProductData>() { productData }, positionTransform);
        }

        public Product CreateProduct(List<ProductData> productDatas, Transform positionTransform)
        {
            Product product;

            if (productDatas.Count > 1)
                product = new MixedProduct(productDatas);
            else
            {
                ProductData productData = productDatas[0];

                if (productData.GetType() == typeof(MixedProductData))
                {
                    MixedProductData mixedProductData = productData as MixedProductData;
                    product = new MixedProduct(mixedProductData.ProductDatas);
                }
                else
                {
                    product = new SingleProduct(productDatas[0]);                    
                }
            }
  

            return product;
        }
    }
}
