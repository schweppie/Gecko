﻿using DG.Tweening;
using UnityEngine;

namespace Gameplay.Robots
{
    public class RobotVisual : MonoBehaviour
    {
        private Robot robot;
        
        public void Initialize(Robot robot)
        {
            this.robot = robot;

            transform.LookAt(transform.position + robot.Direction);
            transform.localPosition = robot.Position;

            SubscribeEvents();

            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutQuart);
        }

        private void OnDispose()
        {
            transform.DOScale(0, 1f)
                .OnComplete(() => Destroy(gameObject));
        }

        private void SubscribeEvents()
        {
            GameVisualizationController.Instance.OnGameVisualization += OnGameVisualization;
            robot.OnDispose += OnDispose;
        }
        
        private void UnsubscribeEvents()
        {
            GameVisualizationController.Instance.OnGameVisualization -= OnGameVisualization;
            robot.OnDispose -= OnDispose;
        }

        private void OnGameVisualization(int step, float t)
        {
            transform.position = Vector3.Lerp(robot.OldPosition, robot.Position, t);
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }
    }
}
