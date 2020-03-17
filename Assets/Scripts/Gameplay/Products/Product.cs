using System.Collections.Generic;

namespace Gameplay.Products
{
    public abstract class Product : ICarryable
    {
        public abstract bool IsMixedProduct { get; }
        public abstract bool ContainsProduct(SingleProductDefinition productDefinition);
        public abstract List<SingleProductDefinition> GetProductDatas();

        private ProductVisualizer visualizer;
        public ProductVisualizer Visualizer => visualizer;

        public void SetVisual(ProductVisualizer visualizer)
        {
            this.visualizer = visualizer;
        }
    }
}
