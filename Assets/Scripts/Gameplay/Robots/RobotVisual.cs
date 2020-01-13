using DG.Tweening;
using Gameplay.Tiles;
using Gameplay.Tiles.Components;
using JP.Framework.Extensions;
using UnityEngine;

namespace Gameplay.Robots
{
    public class RobotVisual : MonoBehaviour
    {
        [SerializeField]
        private bool isDebug = false;

        private Robot robot;

        private Vector3Int oldPosition;
        private Vector3Int oldDirection;

        private Transform visualTransform;
        
        public void Initialize(Robot robot)
        {
            this.robot = robot;

            transform.LookAt(transform.position + robot.Direction);
            transform.localPosition = robot.Position;

            SubscribeEvents();

            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutQuart);

            visualTransform = transform.GetChild(0);
        }

        public void OnDispose()
        {
            transform.DOScale(0, 1f)
                .OnComplete(() => Destroy(gameObject));
        }

        private void SubscribeEvents()
        {
            GameVisualizationController.Instance.OnGameVisualization += OnGameVisualization;
            GameVisualizationController.Instance.OnVisualizationStart += OnGameVisualizationStart;
            robot.OnDispose += OnDispose;
        }

        private void OnGameVisualizationStart()
        {
            Tile visualWorldTile = FieldController.Instance.GetTileAtOrBelowIntPosition(transform.position.RoundToIntVector());

            oldPosition = visualWorldTile.IntPosition;
            oldDirection = transform.forward.RoundToIntVector();
        }

        private void UnsubscribeEvents()
        {
            GameVisualizationController.Instance.OnGameVisualization -= OnGameVisualization;
            GameVisualizationController.Instance.OnVisualizationStart -= OnGameVisualizationStart;
            robot.OnDispose -= OnDispose;
        }

        public void AnimateMove()
        {
            visualTransform.DOKill();
            visualTransform.DOLocalRotate(new Vector3(-10f, 0f, 0f), 0.5f).SetEase(Ease.OutQuart);
        }

        public void AnimateIdle()
        {
            visualTransform.DOKill();
            visualTransform.DOLocalRotate(new Vector3(0f, 0f, 0f), 1f).SetEase(Ease.OutElastic);
        }

        private void OnGameVisualization(int step, float t)
        {
            float additionalHeight = 0f;

            Vector3Int worldTilePosition = transform.position.RoundToIntVector();
            Tile visualWorldTile = FieldController.Instance.GetTileAtOrBelowIntPosition(worldTilePosition);

            Debug.DrawLine(visualWorldTile.IntPosition, visualWorldTile.IntPosition + Vector3.up, Color.green);

            BlockingDirectionTileComponent heightTile = visualWorldTile.GetComponent<BlockingDirectionTileComponent>();
            if (heightTile != null)
                additionalHeight = heightTile.GetHeight(transform.position);

            transform.position = Vector3.Lerp(oldPosition, robot.Position + Vector3.up * additionalHeight, t);
            transform.forward = Vector3.Slerp(oldDirection, robot.Direction, t);
        }

        private void Update()
        {
            if (!isDebug && !robot.isDebugBot)
                return;

            Tile tileAbove = FieldController.Instance.GetTileAboveIntPosition(robot.Position);
            if (tileAbove != null)
                Debug.DrawLine(robot.Position + new Vector3(0, 0.5f, 0f), tileAbove.IntPosition, Color.magenta);

            Tile tileBelow = FieldController.Instance.GetTileBelowIntPosition(robot.Position);
            if (tileBelow != null)
                Debug.DrawLine(robot.Position + new Vector3(0, 0.5f, 0f), tileBelow.IntPosition, Color.yellow);
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }
    }
}
