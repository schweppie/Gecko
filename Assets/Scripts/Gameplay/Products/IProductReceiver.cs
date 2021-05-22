using UnityEngine;

namespace Gameplay.Products
{
    public interface IProductReceiver
    {
        // Maybe should pass in a Product as well
        // If products have different weights/count for example
        bool CanReceiveProduct(Product product);
        void ReceiveProduct(Product product);
        Transform GetReceiverTransform();
    }
}
