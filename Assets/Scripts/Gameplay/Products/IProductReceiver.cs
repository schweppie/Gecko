namespace Gameplay.Products
{
    public interface IProductReceiver
    {
        // Maybe should pass in a Product as well
        // If products have different weights/count for example
        bool CanReceiveProduct();

        void ReceiveProduct(Product product);
    }
}
