using System;
using System.Collections.Generic;

namespace Gameplay.Products
{
    public class MixedProduct : Product
    {
        public List<SingleProductDefinition> ProductDatas { get; }

        public override bool IsMixedProduct => true;

        public override bool ContainsProduct(SingleProductDefinition productDefinition)
        {
            return ProductDatas.Contains(productDefinition);
        }

        public override List<SingleProductDefinition> GetProductDatas()
        {
            return ProductDatas;
        }

        public MixedProduct(List<SingleProductDefinition> productDefinitions)
        {
            // Need to make a new list, else we'll change the scriptable object
            ProductDatas = new List<SingleProductDefinition>();

            for (int i = 0; i < productDefinitions.Count; i++)
                ProductDatas.Add(productDefinitions[i] as SingleProductDefinition);
        }

        public void RemoveProduct(SingleProductDefinition productDefinition)
        {
            if (!ContainsProduct(productDefinition))
                throw new Exception("Can't remove product that doesn't exist!");

            ProductDatas.Remove(productDefinition);

            Visualizer.Visualize();
        }
    }
}
