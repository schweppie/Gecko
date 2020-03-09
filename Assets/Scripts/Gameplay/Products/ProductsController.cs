using JP.Framework.Flow;
using UnityEngine;

namespace Gameplay.Products
{
    public class ProductsController : SingletonBehaviour<ProductsController>
    {
        [SerializeField]
        private ProductVisual productVisualizerPrefab;
        
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

            product.SetVisual(InstantiateProductVisual(product, positionTransform));
            
            return product;
        }

        private ProductVisual InstantiateProductVisual(Product product, Transform positionTransform)
        {
            ProductVisual visual = Instantiate(productVisualizerPrefab);
            visual.Initialize(product, positionTransform);

            return visual;
        }
    }
}
