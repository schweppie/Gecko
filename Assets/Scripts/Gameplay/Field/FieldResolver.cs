using System.Collections.Generic;
using Gameplay.Robots;
using Gameplay.Robots.Strategies;
using UnityEngine;

namespace Gameplay.Field
{
    public class FieldResolver
    {
        private Dictionary<IIntentionRequester, RobotCommandStrategy> intentions;

        public delegate void ResolveDelegate();

        public event ResolveDelegate OnResolveStart;
        public event ResolveDelegate OnResolveComplete;
        
        public FieldResolver()
        {
            intentions = new Dictionary<IIntentionRequester, RobotCommandStrategy>();
            GameStepController.Instance.OnDynamicForwardStep += OnForwardStep;
        }

        private void OnForwardStep(int step)
        {
            Resolve();
        }

        public void AddIntention(IIntentionRequester intentionRequester, RobotCommandStrategy strategy)
        {
            intentions.Add(intentionRequester, strategy);
        }

        private void Resolve()
        {
            OnResolveStart();
            
            HashSet<IIntentionRequester> intentionSuccesses = new HashSet<IIntentionRequester>();

            while (intentions.Count > 0)
            {
                foreach (var entry in intentions)
                {
                    Vector3Int position = entry.Value.GetMoveToPositionIntention();
                    if (!FieldController.Instance.GetTileAtIntPosition(position).IsOccupied)
                    {
                        //intentionSuccesses.Add(entry.)
                    }
                }
                
//                strategy = moves.first();
//
//                if(!strategy.canbedone())
//                    strategy.actor.ResolveFailed();
//                else
//                    strategy.actor.ResolveSucces();
//
//                moves.remove(strategy);
                //intentionSuccesses.Add()

                foreach (var intention in intentionSuccesses)
                {
                    intention.IntentionAccepted();
                    intentions.Remove(intention);
                }

                foreach (var entry in intentions)
                {
                    intentions.Remove(entry.Key);
                    entry.Key.IntentionDeclined();
                }
            }

            OnResolveComplete();
        }
    }
}
