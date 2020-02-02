using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Products
{
    [RequireComponent(typeof(ProductVisual))]
    public class MixedProductVisual : MonoBehaviour
    {
        private static readonly int Color = Shader.PropertyToID("_Color");

        private ProductVisual productVisual;
        private ProductVisual ProductVisual
        {
            get
            {
                if (productVisual == null)
                    productVisual = GetComponent<ProductVisual>();
                return productVisual;
            }
        }

        public void SetupMixedProduct()
        {
            var meshRenderers = GetComponentsInChildren<MeshRenderer>();
            MixedProduct mixedProduct = ProductVisual.Product as MixedProduct;
            if (mixedProduct == null)
                throw new Exception("MixedProductVisual could not be setup as associated product is not a mixed product");
            List<ProductData> productDatas = mixedProduct.ProductDatas;
            int productIndex = 0;
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.material.SetColor(Color, productDatas[productIndex].Color);
                productIndex = (productIndex + 1) % productDatas.Count;
            }
        }
    }
}
