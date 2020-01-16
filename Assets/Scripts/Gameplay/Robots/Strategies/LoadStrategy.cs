using Gameplay.Robots.Commands;
using Gameplay.Tiles;
using Gameplay.Tiles.Components;

namespace Gameplay.Robots.Strategies
{
    public class LoadStrategy : RobotCommandStrategy
    {
        public LoadStrategy(Robot robot) : base(robot)
        {
        }

        public override int GetPriority()
        {
            return 6;
        }

        public override bool IsApplicable()
        {
            if (robot.CarriesResource)
                return false;
            Tile tile = FieldController.Instance.GetTileAtIntPosition(robot.Position);
            var loadTileComponent = tile.GetComponent<LoadTileComponent>();
            return loadTileComponent != null;
        }

        public override RobotCommand GetCommand()
        {
            return new LoadCommand(); ;
        }
    }
}
