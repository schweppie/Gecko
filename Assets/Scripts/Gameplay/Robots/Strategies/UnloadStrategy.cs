using Gameplay.Field;
using Gameplay.Products;
using Gameplay.Robots.Commands;
using Gameplay.Tiles;
using Gameplay.Tiles.Components;

namespace Gameplay.Robots.Strategies
{
    public class UnloadStrategy : RobotCommandStrategy
    {
        public UnloadStrategy(Robot robot) : base(robot)
        {
        }

        public override int GetPriority()
        {
            return 7;
        }

        public override bool IsApplicable()
        {
            if (!robot.IsCarrying)
                return false;
            Tile tile = FieldController.Instance.GetTileAtIntPosition(robot.Position);
            var unloadTileComponent = tile.GetComponent<UnloadTileComponent>();
            return unloadTileComponent != null && unloadTileComponent.CanUnloadProduct(robot.Carryable as Product);
        }

        public override RobotCommand GetCommand()
        {
            Tile tile = FieldController.Instance.GetTileAtIntPosition(robot.Position);
            var unloadTileComponent = tile.GetComponent<UnloadTileComponent>();
            return new UnloadCommand(unloadTileComponent);
        }
    }
}
