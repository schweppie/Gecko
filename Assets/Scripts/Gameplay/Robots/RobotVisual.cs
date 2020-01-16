using DG.Tweening;
using Gameplay.Tiles;
using JP.Framework.Extensions;
using UnityEngine;

namespace Gameplay.Robots
{
    public class RobotVisual : MonoBehaviour
    {
        [SerializeField]
        private Transform animationRoot;

        [SerializeField]
        private Transform rotationRoot;

        [SerializeField]
        private bool isDebug = false;

        private Robot robot;

        private Vector3Int oldPosition;
        private Vector3Int oldDirection;

        private float xAngle;
        private float heightPosition;
        private float yVelocity = 0f;
        private const float GRAVITY = 20f;
        
        public void Initialize(Robot robot)
        {
            this.robot = robot;

            transform.LookAt(transform.position + robot.Direction);
            transform.localPosition = robot.Position;

            SubscribeEvents();

            animationRoot.localScale = Vector3.zero;
            animationRoot.DOScale(Vector3.one, 1f).SetEase(Ease.OutQuart);

            heightPosition = transform.position.y;
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
            oldPosition = transform.position.ToIntVector();
            oldDirection = transform.forward.ToIntVector();
        }

        private void UnsubscribeEvents()
        {
            GameVisualizationController.Instance.OnGameVisualization -= OnGameVisualization;
            GameVisualizationController.Instance.OnVisualizationStart -= OnGameVisualizationStart;
            robot.OnDispose -= OnDispose;
        }

        public void AnimateMove()
        {
            animationRoot.DOKill();
            animationRoot.DOLocalRotate(new Vector3(-10f, 0f, 0f), 0.5f).SetEase(Ease.OutQuart);
        }

        public void AnimateIdle()
        {
            animationRoot.DOKill();
            animationRoot.DOLocalRotate(new Vector3(0f, 0f, 0f), 1f).SetEase(Ease.OutElastic);
        }

        private void OnGameVisualization(int step, float t)
        {
            Vector3 oldVisualPosition = transform.position;

            // Interpolate position based on t
            Vector3 position = Vector3.Lerp(oldPosition, robot.Position, t);

            // Update height
            position.y = GetVisualHeight(t);

            transform.position = position;
            transform.forward = Vector3.Slerp(oldDirection, robot.Direction, t);

            // Rotate based on delta
            float angle = Mathf.Clamp((oldVisualPosition.y - position.y) * 800f, -45, 15);
            xAngle = Mathf.Lerp(xAngle, angle, 10f * Time.deltaTime);
            rotationRoot.localRotation = Quaternion.Euler(xAngle, 0f, 0f);
        }

        private float GetVisualHeight(float t)
        {
            // Find the visual tile that is below the visual robot
            Vector3Int worldTilePosition = transform.position.ToIntVector();
            Tile worldTile = FieldController.Instance.GetTileAtOrBelowIntPosition(worldTilePosition);

            float targetHeight = robot.Tile.IntPosition.y;

            // If there is a tile below, get the tile's floor height and handle gravity
            if (worldTile != null)
            {
                // Get the height of the visual world tile
                targetHeight = worldTile.HeightReporter.GetValue(robot);

                // If in air, increase fall velocity and update y position
                // TODO this should use an equation, so we can roll back time using T as well
                if (heightPosition > targetHeight)
                {
                    yVelocity = yVelocity - (GRAVITY * Time.deltaTime);
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

            Debug.DrawLine(robot.Tile.IntPosition + new Vector3(0.5f, 0f, -0.5f), robot.Tile.IntPosition + new Vector3(0.5f, 0f, 0.5f), Color.red);
            Debug.DrawLine(robot.Tile.IntPosition + new Vector3(-0.5f, 0f, -0.5f), robot.Tile.IntPosition + new Vector3(-0.5f, 0f, 0.5f), Color.red);
            Debug.DrawLine(robot.Tile.IntPosition + new Vector3(-0.5f, 0f, 0.5f), robot.Tile.IntPosition + new Vector3(0.5f, 0f, 0.5f), Color.red);
            Debug.DrawLine(robot.Tile.IntPosition + new Vector3(-0.5f, 0f, -0.5f), robot.Tile.IntPosition + new Vector3(0.5f, 0f, -0.5f), Color.red);

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
