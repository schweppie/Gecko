namespace Gameplay.Products
{
    public interface IProductProducer
    {
        bool CanProduceProduct();
        Product ProduceProduct();
    }
}
