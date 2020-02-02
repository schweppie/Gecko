using UnityEngine;

namespace Gameplay.Products
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ProductData")]
    public class ProductData : ScriptableObject
    {
        [SerializeField]
        private Color color;
        public Color Color => color;
    }
}
