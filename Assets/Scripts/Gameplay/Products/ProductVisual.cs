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

        public void AnimateFallOnToCarrier(Transform unanimatedCarrierTransform, Transform carrierTransform)
        {
            transform.SetParent(unanimatedCarrierTransform, true);
            transform.DOLocalMove(Vector3.zero, .5f)
                .OnComplete(() => transform.SetParent(carrierTransform, true));
        }

        public void AnimateToReceiver(Transform receiverTransform)
        {
            transform.SetParent(receiverTransform, true);
            transform.DOLocalMove(Vector3.zero, .5f);
        }
    }
}
