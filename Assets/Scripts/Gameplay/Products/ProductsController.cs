using JP.Framework.Flow;
using UnityEngine;

namespace Gameplay.Products
{
    public class ProductsController : SingletonBehaviour<ProductsController>
    {
        [SerializeField]
        private ProductVisualizer productVisualizerPrefab;
        
        public Product CreateProduct(ProductDefinition productDefinition, Transform positionTransform)
        {
            Product product;

            if (productDefinition.GetType() == typeof(MixedProductDefinition))
            {
                MixedProductDefinition mixedProductDefinition = productDefinition as MixedProductDefinition;
                product = new MixedProduct(mixedProductDefinition.ProductDefinitions);
            }
            else
            {
                product = new SingleProduct(productDefinition);                    
            }

            product.SetVisual(InstantiateProductVisual(product, positionTransform));
            
            return product;
        }

        private ProductVisualizer InstantiateProductVisual(Product product, Transform positionTransform)
        {
            ProductVisualizer visualizer = Instantiate(productVisualizerPrefab);
            visualizer.Initialize(product, positionTransform);

            return visualizer;
        }
    }
}
