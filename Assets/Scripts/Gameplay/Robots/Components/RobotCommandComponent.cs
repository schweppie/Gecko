using System.Collections.Generic;
using System.Linq;
using Gameplay.Field;
using Gameplay.Robots.Commands;
using Gameplay.Robots.Strategies;
using JP.Framework.Strategy;
using UnityEngine;

namespace Gameplay.Robots.Components
{
    public class RobotCommandComponent : RobotComponent, IIntentionRequester
    {
        private List<RobotCommand> commands = new List<RobotCommand>();
        public List<RobotCommand> Commands => commands;

        private StrategyContainer<RobotCommandStrategy> commandStrategyContainer;

        private RobotCommandStrategy strategy;

        private MoveStrategy moveStrategy;

        private RobotCommand robotCommand;
        
        public override void Initialize(Robot robot)
        {
            base.Initialize(robot);
            
            commandStrategyContainer = new StrategyContainer<RobotCommandStrategy>();
            commandStrategyContainer.AddStrategy(new SpawnStrategy(robot));
            commandStrategyContainer.AddStrategy(new DoNothingStrategy(robot));
            commandStrategyContainer.AddStrategy(new CollectStrategy(robot));
            commandStrategyContainer.AddStrategy(new FallStrategy(robot));
            commandStrategyContainer.AddStrategy(new ChangeDirectionStrategy(robot));
            moveStrategy = new MoveStrategy(robot);
            commandStrategyContainer.AddStrategy(moveStrategy);
            commandStrategyContainer.AddStrategy(new WaitStrategy(robot));

            FieldController.Instance.FieldResolver.OnResolveStart += OnResolveStart;
            GameStepController.Instance.OnPrewarmCommands += OnPrewarmCommands;
            GameStepController.Instance.OnExecuteCommands += OnExecuteCommands;
        }

        private void OnPrewarmCommands(int step)
        {
            robotCommand?.Prewarm();
        }

        private void OnExecuteCommands(int step)
        {
            robotCommand?.Execute();
        }

        private void OnResolveStart()
        {
            PickStrategy(true);
        }

        public void PickStrategy(bool firstTime)
        {
            if (firstTime)
                moveStrategy.ResetUsed();
            strategy = commandStrategyContainer.GetApplicableStrategy();

            FieldController.Instance.FieldResolver.AddIntention(this, strategy);
        }

        public RobotCommand GetNextCommand()
        {
            robotCommand = strategy.GetCommand();
            
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
        
        void IIntentionRequester.IntentionAccepted()
        {
            Debug.Log("ACCEPTED");
            robotCommand = GetNextCommand();
        }

        void IIntentionRequester.IntentionDeclined()
        {
            Debug.Log("DECLINED");
            PickStrategy(false);
        }
    }
}
