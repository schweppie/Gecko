using System.Collections.Generic;
using System.Linq;
using Gameplay.Robots.Commands;
using Gameplay.Robots.Strategies;
using JP.Framework.Strategy;

namespace Gameplay.Robots.Components
{
    public class RobotCommandComponent : RobotComponent
    {
        private List<RobotCommand> commands = new List<RobotCommand>();
        public List<RobotCommand> Commands => commands;

        private StrategyContainer<RobotCommandStrategy> commandStrategyContainer;
        
        public override void Initialize(Robot robot)
        {
            base.Initialize(robot);
            
            commandStrategyContainer = new StrategyContainer<RobotCommandStrategy>();
            commandStrategyContainer.AddStrategy(new SpawnStrategy(robot));
            commandStrategyContainer.AddStrategy(new DoNothingStrategy(robot));
            commandStrategyContainer.AddStrategy(new CollectStrategy(robot));
            commandStrategyContainer.AddStrategy(new FallStrategy(robot));
            commandStrategyContainer.AddStrategy(new MoveStrategy(robot));
        }

        public RobotCommand GetNextCommand()
        {
            RobotCommand robotCommand = commandStrategyContainer.GetApplicableStrategy().GetCommand();
            
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
