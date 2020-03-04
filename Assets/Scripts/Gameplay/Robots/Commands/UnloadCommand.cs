using Gameplay.Field;
using Gameplay.Products;

namespace Gameplay.Robots.Commands
{
    public class UnloadCommand : RobotCommand
    {
        private IProductReceiver productReceiver;

        public UnloadCommand(IProductReceiver productReceiver)
        {
            this.productReceiver = productReceiver;
        }

        public override void Execute()
        {
            FieldController.Instance.AddOccupier(robot.Position, robot);
            Product product = robot.Carryable as Product;
            productReceiver.ReceiveProduct(product);

            product.Visual.AnimateToStation(productReceiver.GetReceiverTransform());

            robot.UnloadProduct();
        }
    }
}
