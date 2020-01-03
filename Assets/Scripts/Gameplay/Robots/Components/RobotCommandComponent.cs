using System.Collections.Generic;
using System.Linq;
using Commands.Robots;

namespace Gameplay.Robots.Components
{
    public class RobotCommandComponent : RobotComponent
    {
        private List<RobotCommand> commands = new List<RobotCommand>();
        
        public RobotCommand GetNextCommand()
        {
            // Todo determine which command to instantiate
            RobotCommand robotCommand;
            robotCommand = new MoveRobotCommand();
            
            robotCommand.Initialize(robot);
            
            commands.Add(robotCommand);
            
            return robotCommand;
        }

        public RobotCommand GetPrevCommand()
        {
            // Todo determine which command to instantiate
            RobotCommand robotCommand = commands.Last();
            commands.RemoveAt(commands.Count-1);
            return robotCommand;            
        }
    }
}
