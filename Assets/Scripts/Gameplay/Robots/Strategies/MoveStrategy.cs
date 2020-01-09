using Gameplay.Robots.Commands;
using Gameplay.Tiles.Components;

namespace Gameplay.Robots.Strategies
{
    public class MoveStrategy : RobotCommandStrategy
    {
        public MoveStrategy(Robot robot) : base(robot)
        {
        }

        public override int GetPriority()
        {
            return 6;
        }

        public override bool IsApplicable()
        {
            // Don't move while on empty tile (falling)
            if (robot.Tile.GetComponent<EmptyTileComponent>() != null)
                return false;

            var occupationBuffer = GameStepController.Instance.OccupationBuffer;
            var oldOccupationBuffer = GameStepController.Instance.OldOccupationBuffer;

            if (!occupationBuffer.ContainsKey(robot.Position + robot.Direction))
            {
                if(oldOccupationBuffer.ContainsKey(robot.Position + robot.Direction))
                {
                    IOccupier oldOccupier =  GameStepController.Instance.GetOldOccupierAt(robot.Position + robot.Direction);
                    IOccupier currentOccupier =  GameStepController.Instance.GetOccupierAt(robot.Position);

                    if (oldOccupier == currentOccupier)
                    {
                        commandComponent.AddInvalidOccupier(oldOccupier);
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public override RobotCommand GetCommand()
        {
            RobotCommand moveCommand = new MoveCommand();
            return moveCommand;
        }
    }
}
