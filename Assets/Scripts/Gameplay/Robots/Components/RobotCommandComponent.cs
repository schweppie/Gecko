using System.Collections.Generic;
using System.Linq;
using Gameplay.Robots.Commands;
using Gameplay.Robots.Strategies;
using JP.Framework.Strategy;
using UnityEngine;

namespace Gameplay.Robots.Components
{
    public class RobotCommandComponent : RobotComponent
    {
        private List<RobotCommand> commands = new List<RobotCommand>();
        public List<RobotCommand> Commands => commands;

        private StrategyContainer<RobotCommandStrategy> commandStrategyContainer;

        /// <summary>
        /// This set holds the other occupiers which require to pick a new strategy
        /// when this robot's command has been executed.
        /// 
        /// It can populated in the IsApplicable() from a strategy.
        /// </summary>
        private HashSet<IOccupier> invalidOccupiers = new HashSet<IOccupier>();

        private void ResetInvalidOccupiers()
        {
            invalidOccupiers.Clear();
        }

        public void AddInvalidOccupier(IOccupier occupier)
        {
            invalidOccupiers.Add(occupier);
        }

        private void HandleInvalidOccupiers()
        {
            if (invalidOccupiers.Count > 1)
                Debug.LogWarning("Handling multiple invalid occupiers! Should be 1, unless you know what you are doing");

            foreach (IOccupier occupier in invalidOccupiers)
                occupier.PickNewStrategy();
        }
        
        public override void Initialize(Robot robot)
        {
            base.Initialize(robot);
            
            commandStrategyContainer = new StrategyContainer<RobotCommandStrategy>();
            commandStrategyContainer.AddStrategy(new SpawnStrategy(robot));
            commandStrategyContainer.AddStrategy(new DoNothingStrategy(robot));
            commandStrategyContainer.AddStrategy(new CollectStrategy(robot));
            commandStrategyContainer.AddStrategy(new FallStrategy(robot));
            commandStrategyContainer.AddStrategy(new ChangeDirectionStrategy(robot));
            commandStrategyContainer.AddStrategy(new LoadStrategy(robot));
            commandStrategyContainer.AddStrategy(new MoveStrategy(robot));
            commandStrategyContainer.AddStrategy(new WaitStrategy(robot));
        }

        public void ExecuteNextCommand()
        {
            ResetInvalidOccupiers();

            RobotCommand robotCommand = commandStrategyContainer.GetApplicableStrategy().GetCommand();
            commands.Add(robotCommand);

            robotCommand.Initialize(robot);
            robotCommand.Execute();

            HandleInvalidOccupiers();
        }

        public void ExecutePrevCommand()
        {
            if (commands.Count == 0)
            {
                robot.Dispose();
                return;
            }
            
            RobotCommand robotCommand = commands.Last();
            commands.RemoveAt(commands.Count-1);

            robotCommand.Undo();
        }
        public void UndoLastCommand()
        {
            commands.Last().Undo();
            commands.RemoveAt(commands.Count - 1);
        }
    }
}
