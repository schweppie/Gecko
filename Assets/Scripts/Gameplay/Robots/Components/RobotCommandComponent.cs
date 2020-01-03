using System.Collections.Generic;
using System.Linq;
using Commands.Robots;
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
