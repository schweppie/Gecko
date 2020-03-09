using UnityEngine;

namespace Gameplay.Products
{
    [CreateAssetMenu(fileName = "SingleProductData", menuName = "ScriptableObjects/SingleProductData")]
    public class SingleProductData : ProductData
    {
        [SerializeField]
        private GameObject singleProductVisualPrefab;
        public GameObject SingleProductVisualPrefab => singleProductVisualPrefab;
    }
}
