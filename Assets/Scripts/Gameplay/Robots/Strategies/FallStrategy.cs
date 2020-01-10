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
            return 4;
        }

        public override bool IsApplicable()
        {
            if (robot.Tile.GetComponent<EmptyTileComponent>() == null)
                return false;

            IOccupier currentOccupier = GameStepController.Instance.GetOccupierAt(robot.Position + Vector3Int.down);

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
