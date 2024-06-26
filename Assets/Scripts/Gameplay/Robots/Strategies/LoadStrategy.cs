﻿using Gameplay.Field;
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
            return 7;
        }

        public override bool IsApplicable()
        {
            if (robot.IsCarrying)
                return false;
            Tile tile = FieldController.Instance.GetTileAtIntPosition(robot.Position);
            var loadTileComponent = tile.GetComponent<LoadTileComponent>();
            return loadTileComponent != null && loadTileComponent.CanLoadProduct();
        }

        public override RobotCommand GetCommand()
        {
            Tile tile = FieldController.Instance.GetTileAtIntPosition(robot.Position);
            var loadTileComponent = tile.GetComponent<LoadTileComponent>();
            return new LoadCommand(loadTileComponent);
        }
    }
}
