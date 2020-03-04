using Gameplay.Field;
using Gameplay.Products;
using Gameplay.Stations.Components;

namespace Gameplay.Robots.Commands
{
    public class LoadCommand : RobotCommand
    {
        private IProductProducer productProducer;

        public LoadCommand(IProductProducer productProducer)
        {
            this.productProducer = productProducer;
        }

        public override void Execute()
        {
            FieldController.Instance.AddOccupier(robot.Position, robot);
            Product product = productProducer.ProduceProduct();
            product.Visual.AnimateFallOnToCarrier(robot.RobotVisual.UnanimatedCarryTransform, robot.RobotVisual.CarryTransform);
            robot.LoadProduct(product);
        }
    }
}
