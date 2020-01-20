using UnityEngine;

namespace Gameplay.Products
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Product")]
    public class Product : ScriptableObject
    {
        [SerializeField]
        private Color color;
    }
}
