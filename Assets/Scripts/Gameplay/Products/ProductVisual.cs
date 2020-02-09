using DG.Tweening;
using UnityEngine;

namespace Gameplay.Products
{
    public class ProductVisual : MonoBehaviour
    {
        public Product Product;

        public void SetProduct(Product product)
        {
            Product = product;
        }

        public void AnimateFallOnToCarrier(Transform carrierTransform)
        {
            transform.SetParent(carrierTransform, true);
            transform.DOLocalMove(transform.localPosition - new Vector3(0, 1.2f, 0), .5f);
        }
    }
}
