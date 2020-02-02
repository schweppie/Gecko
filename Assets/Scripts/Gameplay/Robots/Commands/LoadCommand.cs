using Gameplay.Field;
using Gameplay.Products;
using Gameplay.Stations.Components;

namespace Gameplay.Robots.Commands
{
    public class LoadCommand : RobotCommand
    {
        private GarbageDispenserComponent garbageDispenserComponent; // TODO should be generic

        public LoadCommand(GarbageDispenserComponent garbageDispenserComponent)
        {
            this.garbageDispenserComponent = garbageDispenserComponent;
        }

        public override void Execute()
        {
            FieldController.Instance.AddOccupier(robot.Position, robot);
            ProductVisual productVisual = garbageDispenserComponent.CreateProductVisual();
            robot.CarryProduct(productVisual);
        }
    }
}
