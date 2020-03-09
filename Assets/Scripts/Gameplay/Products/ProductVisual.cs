using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Products
{
    public class ProductVisual : MonoBehaviour
    {
        private Product product;

        private List<GameObject> visualProducts;

        public void Initialize(Product product, Transform positionTransform)
        {
            this.product = product;
            
            transform.SetParent(positionTransform);
            transform.localPosition = Vector3.zero;
            
            visualProducts = new List<GameObject>();
            
            Visualize();
        }

        public void Visualize()
        {
            for (int i = 0; i < visualProducts.Count; i++)
                Destroy(visualProducts[i].gameObject);

            var productDatas = product.GetProductDatas();
            
            for (int i = 0; i < productDatas.Count; i++)
            {
                GameObject visualProduct = Instantiate(productDatas[i].SingleProductVisualPrefab);
                visualProduct.transform.SetParent(transform);
                
                visualProduct.transform.localPosition = new Vector3(Random.Range(-0.15f, 0.15f), 0f, Random.Range(-0.15f, 0.15f));
                visualProduct.transform.localRotation = Quaternion.Euler(Random.Range(0, 360),Random.Range(0, 360),Random.Range(0, 360));
                
                visualProducts.Add(visualProduct);
            }
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
