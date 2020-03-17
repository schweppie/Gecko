using Gameplay.Field;
using Gameplay.Products;
using Gameplay.Tiles.Components;

namespace Gameplay.Robots.Commands
{
    public class UnloadCommand : RobotCommand
    {
        private UnloadTileComponent unloadTileComponent;

        public UnloadCommand(UnloadTileComponent unloadTileComponent)
        {
            this.unloadTileComponent = unloadTileComponent;
        }

        public override void Execute()
        {
            FieldController.Instance.AddOccupier(robot.Position, robot);
            Product product = robot.Carryable as Product;
            unloadTileComponent.UnloadProduct(product);

            product.Visualizer.AnimateToReceiver(unloadTileComponent.GetReceiverTransform());

            robot.UnloadProduct();
        }
    }
}
