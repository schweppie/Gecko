using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Products
{
    [CreateAssetMenu(fileName = "MixedProductData", menuName = "ScriptableObjects/MixedProductData")]
    public class MixedProductData : ProductData
    {
        [SerializeField]
        private List<ProductData> productDatas;
        public List<ProductData> ProductDatas => productDatas;
    }
}
