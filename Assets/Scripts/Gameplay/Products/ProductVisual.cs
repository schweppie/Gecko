using DG.Tweening;
using UnityEngine;

namespace Gameplay.Products
{
    public class ProductVisual : MonoBehaviour
    {
        public Product Product;

        public void Initialize(Product product)
        {
            Product = product;
            product.visual = this;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            GameVisualizationController.Instance.OnGameVisualization += OnGameVisualization;
            GameVisualizationController.Instance.OnVisualizationStart += OnGameVisualizationStart;
        }

        private void UnsubscribeEvents()
        {
            if (GameVisualizationController.Instance != null)
            {
                GameVisualizationController.Instance.OnGameVisualization -= OnGameVisualization;
                GameVisualizationController.Instance.OnVisualizationStart -= OnGameVisualizationStart;
            }
        }

        private void OnGameVisualizationStart()
        {
        }

        private void OnGameVisualization(int step, float t)
        {
            // Interpolate position based on t
//            Vector3 position = Vector3.Lerp(oldPosition, robot.Position, t);
        }

        public void SetCarrier(Transform carrierTransform)
        {
            transform.SetParent(carrierTransform, true);
            transform.DOLocalMove(transform.localPosition - new Vector3(0, 1.2f, 0), .5f);
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }
    }
}
