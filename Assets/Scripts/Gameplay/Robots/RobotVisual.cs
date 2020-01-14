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

        private float heightPosition;
        private float yVelocity = 0f;
        private const float GRAVITY = -1f;
        
        public void Initialize(Robot robot)
        {
            this.robot = robot;

            transform.LookAt(transform.position + robot.Direction);
            transform.localPosition = robot.Position;

            SubscribeEvents();

            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutQuart);

            heightPosition = transform.position.y;

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
            oldPosition = transform.position.RoundToIntVector();
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
            // Interpolate position based on t
            Vector3 position = Vector3.Lerp(oldPosition, robot.Position, t);

            // Update height
            position.y = GetVisualHeight(t);

            transform.position = position;
            transform.forward = Vector3.Slerp(oldDirection, robot.Direction, t);
        }

        private float GetVisualHeight(float t)
        {
            // Find the visual tile that is below the visual robot
            Vector3Int worldTilePosition = transform.position.RoundToIntVector();
            Tile worldTile = FieldController.Instance.GetTileAtOrBelowIntPosition(worldTilePosition);

            float targetHeight = robot.Tile.IntPosition.y - 1;

            // If there is a tile below, get the tile's floor height and handle gravity
            if (worldTile != null)
            {
                // Get the height of the visual world tile
                targetHeight = worldTile.Visual.HeightReporter.GetValue(robot, t);

                // If in air, increase fall velocity and update y position
                if (heightPosition > targetHeight)
                {
                    yVelocity = yVelocity - (10f * Time.deltaTime);
                    heightPosition += yVelocity * Time.deltaTime;
                }

                // If below height of tile, reset to tileheight
                if (heightPosition < targetHeight)
                {
                    yVelocity = 0f;
                    heightPosition = targetHeight;
                }
            }
            else
            {
                // No tile below, lets just lerp
                heightPosition = Mathf.Lerp(oldPosition.y, targetHeight, t);
            }

            return heightPosition;
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
