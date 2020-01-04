using System.Collections.Generic;
using System.Linq;
using Gameplay.Robots.Commands;
using Gameplay.Tiles.Components;

namespace Gameplay.Robots.Components
{
    public class RobotCommandComponent : RobotComponent
    {
        private List<RobotCommand> commands = new List<RobotCommand>();
        
        public RobotCommand GetNextCommand()
        {
            // Todo determine which command to instantiate
            RobotCommand robotCommand;

            if (commands.Count == 0)
                robotCommand = new SpawnRobotCommand();
            else if (commands.Last().GetType() == typeof(CollectRobotCommand)
                     || commands.Last().GetType() == typeof(DoNothingCommand))
                robotCommand = new DoNothingCommand();
            else if (robot.Tile.GetComponent<CollectorTileComponent>())
                robotCommand = new CollectRobotCommand();
            else if (robot.Tile.GetComponent<EmptyTileComponent>())
                robotCommand = new FallRobotCommand();
            else
                robotCommand = new MoveRobotCommand();
            
            robotCommand.Initialize(robot);
            commands.Add(robotCommand);
            
            return robotCommand;
        }

        public RobotCommand GetPrevCommand()
        {
            if (commands.Count == 0)
                return null;
            
            // Todo determine which command to instantiate
            RobotCommand robotCommand = commands.Last();
            commands.RemoveAt(commands.Count-1);
            return robotCommand;            
        }
    }
}
