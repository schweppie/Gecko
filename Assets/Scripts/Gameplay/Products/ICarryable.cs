﻿namespace Gameplay.Products
{
    public interface ICarryable
    {
        bool IsMixedProduct { get; }
        bool ContainsProduct(SingleProductDefinition productDefinition);
    }
}
