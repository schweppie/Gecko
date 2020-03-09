using System;
using System.Collections.Generic;

namespace Gameplay.Products
{
    public class MixedProduct : Product
    {
        public List<SingleProductData> ProductDatas { get; }

        public override bool IsMixedProduct => true;

        public override bool ContainsProduct(SingleProductData productData)
        {
            return ProductDatas.Contains(productData);
        }

        public override List<SingleProductData> GetProductDatas()
        {
            return ProductDatas;
        }

        public MixedProduct(List<ProductData> productDatas)
        {
            // Need to make a new list, else we'll change the scriptable object
            ProductDatas = new List<SingleProductData>();

            for (int i = 0; i < productDatas.Count; i++)
                ProductDatas.Add(productDatas[i] as SingleProductData);
        }

        public void RemoveProduct(SingleProductData productData)
        {
            if (!ContainsProduct(productData))
                throw new Exception("Can't remove product that doesn't exist!");

            ProductDatas.Remove(productData);
            
            Visual.Visualize();
        }
    }
}
