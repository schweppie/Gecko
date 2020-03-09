using Gameplay.Field;
using Gameplay.Products;
using Gameplay.Tiles.Components;

namespace Gameplay.Robots.Commands
{
    public class LoadCommand : RobotCommand
    {
        private LoadTileComponent loadTileComponent;

        public LoadCommand(LoadTileComponent loadTileComponent)
        {
            this.loadTileComponent = loadTileComponent;
        }

        public override void Execute()
        {
            FieldController.Instance.AddOccupier(robot.Position, robot);
            Product product = loadTileComponent.LoadProduct();
            //product.Visual.AnimateFallOnToCarrier(robot.RobotVisual.UnanimatedCarryTransform, robot.RobotVisual.CarryTransform);
            robot.LoadProduct(product);
        }
    }
}
