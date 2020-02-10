using UnityEngine;

namespace Gameplay.Products
{
    public abstract class Product : ICarryable
    {
        public abstract bool IsMixedProduct { get; }
        public abstract bool ContainsProduct(ProductData productData);

        private ProductVisual visual;
        public ProductVisual Visual => visual;

        public void SetVisual(ProductVisual visual)
        {
            this.visual = visual;
            visual.SetProduct(this);
        }
    }
}
