using Gameplay.Field;
using Gameplay.Robots.Commands;
using Gameplay.Tiles.Components;
using UnityEngine;

namespace Gameplay.Robots.Strategies
{
    public class FallStrategy : RobotCommandStrategy
    {
        public FallStrategy(Robot robot) : base(robot)
        {
        }

        public override int GetPriority()
        {
            return 5;
        }

        public override bool IsApplicable()
        {
            if (robot.Tile.GetComponent<EmptyTileComponent>() == null)
                return false;

            IOccupier currentOccupier = FieldController.Instance.GetOccupierAt(robot.Position + Vector3Int.down);

            if (currentOccupier != null)
                return false;

            return true;
        }

        public override RobotCommand GetCommand()
        {
            return new FallCommand();
        }
    }
}
