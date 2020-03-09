using System;
using System.Collections.Generic;

namespace Gameplay.Products
{
    public class MixedProduct : Product
    {
        public List<ProductData> ProductDatas { get; }

        public override bool IsMixedProduct => true;

        public override bool ContainsProduct(ProductData productData)
        {
            return ProductDatas.Contains(productData);
        }

        public MixedProduct(List<ProductData> productDatas)
        {
            // Need to make a new list, else we'll change the scriptable object
            ProductDatas = new List<ProductData>();
            ProductDatas.AddRange(productDatas);
        }

        public void RemoveProduct(ProductData productData)
        {
            if (!ContainsProduct(productData))
                throw new Exception("Can't remove product that doesn't exist!");

            ProductDatas.Remove(productData);
        }
    }
}
