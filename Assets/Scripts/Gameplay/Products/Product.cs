using UnityEngine;

namespace Gameplay.Products
{
    public abstract class Product : ICarryable
    {
        public abstract bool IsMixedProduct { get; }
        public abstract bool ContainsProduct(ProductData productData);

        public ProductVisual visual;
    }
}
