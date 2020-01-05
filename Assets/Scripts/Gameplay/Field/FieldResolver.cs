﻿using System.Collections.Generic;
using Gameplay.Robots;
using Gameplay.Robots.Components;
using Gameplay.Robots.Strategies;
using Gameplay.Tiles;
using UnityEngine;

namespace Gameplay.Field
{
    public class FieldResolver
    {
        private Dictionary<IIntentionRequester, RobotCommandStrategy> intentions;
        private Dictionary<IIntentionRequester, RobotCommandStrategy> newIntentions;

        public delegate void ResolveDelegate();

        public event ResolveDelegate OnResolveStart;
        public event ResolveDelegate OnResolveComplete;
        
        private HashSet<Vector3Int> newOccupiedPositions = new HashSet<Vector3Int>();
        
        public FieldResolver()
        {
            intentions = new Dictionary<IIntentionRequester, RobotCommandStrategy>();
            newIntentions = new Dictionary<IIntentionRequester, RobotCommandStrategy>();
            GameStepController.Instance.OnDynamicForwardStep += OnForwardStep;
        }

        private void OnForwardStep(int step)
        {
            Resolve();
        }

        public void AddIntention(IIntentionRequester intentionRequester, RobotCommandStrategy strategy)
        {
            newIntentions.Add(intentionRequester, strategy);
        }

        public void AddNewIntensionsToIntensions()
        {
            foreach (var entry in newIntentions)
                intentions.Add(entry.Key, entry.Value);
            newIntentions.Clear();
        }

        private void Resolve()
        {
            newOccupiedPositions.Clear();
            intentions.Clear();
            
            OnResolveStart?.Invoke();

            AddNewIntensionsToIntensions();
            
            foreach (var entry in intentions)
            {
                Vector3Int origin = entry.Value.GetIntentOrigin();
                Vector3Int target = entry.Value.GetIntentTarget();
                if (origin == target)
                {
                    newOccupiedPositions.Add(target);
                    entry.Key.IntentionAccepted();
                }
            }
            
            FilterIntersectingIntentensions();

            HashSet<IIntentionRequester> removeRequesters = new HashSet<IIntentionRequester>();
            while (intentions.Count > 0)
            {
                AddNewIntensionsToIntensions();
                removeRequesters.Clear();
                
                foreach (var entry in intentions)
                {
                    Vector3Int origin = entry.Value.GetIntentOrigin();
                    Vector3Int target = entry.Value.GetIntentTarget();
                    if (origin == target)
                    {
                        removeRequesters.Add(entry.Key);
                        continue;
                    }

                    Tile tileTarget = FieldController.Instance.GetTileAtIntPosition(target);
                    if (tileTarget.IsOccupied)
                    {
                        if (tileTarget.Occupier is Robot)
                        {
                            if (newOccupiedPositions.Contains(target))
                            {
                                entry.Key.IntentionDeclined();
                            }
                            else
                            {
                                newOccupiedPositions.Add(target);
                                entry.Key.IntentionAccepted();
                            }
                        }
                        else
                        {
                            entry.Key.IntentionDeclined();
                        }
                    }
                    else
                    {
                        newOccupiedPositions.Add(target);
                        entry.Key.IntentionAccepted();
                    }
                    removeRequesters.Add(entry.Key);
                }
                
                foreach (var requester in removeRequesters)
                    intentions.Remove(requester);
            }

            OnResolveComplete?.Invoke();
        }

        private void FilterIntersectingIntentensions()
        {
            HashSet<IIntentionRequester> removeRequesters = new HashSet<IIntentionRequester>();
            
            foreach (var entry in intentions)
            {
                if (removeRequesters.Contains(entry.Key))
                    continue;
                
                Vector3Int origin = entry.Value.GetIntentOrigin();
                Vector3Int target = entry.Value.GetIntentTarget();

                Robot otherRobot = FieldController.Instance.GetTileAtIntPosition(target).Occupier as Robot;
                if (otherRobot == null)
                    continue;
                IIntentionRequester otherRequester = otherRobot.GetComponent<RobotCommandComponent>();
                Vector3Int otherTarget = intentions[otherRequester].GetIntentTarget();
                if (origin == otherTarget)
                {
                    removeRequesters.Add(entry.Key);
                    removeRequesters.Add(otherRequester);
                }
            }

            foreach (var requester in removeRequesters)
            {
                intentions.Remove(requester);
                requester.IntentionDeclined();
            }
        }
    }
}
