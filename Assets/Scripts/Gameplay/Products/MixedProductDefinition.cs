using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Products
{
    [CreateAssetMenu(fileName = "MixedProductData", menuName = "ScriptableObjects/MixedProductData")]
    public class MixedProductDefinition : ProductDefinition
    {
        [SerializeField]
        private List<SingleProductDefinition> productDefinitions;
        public List<SingleProductDefinition> ProductDefinitions => productDefinitions;
    }
}
