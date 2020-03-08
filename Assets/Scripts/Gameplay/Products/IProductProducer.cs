namespace Gameplay.Products
{
    public interface IProductProducer
    {
        bool CanProduceProduct(ProductData productData);
        Product ProduceProduct(ProductData productData);
    }
}
