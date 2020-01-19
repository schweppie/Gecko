using DG.Tweening;
using Gameplay.Field;
using Gameplay.Tiles;
using JP.Framework.Extensions;
using UnityEditor;
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

        private float heightPosition;
        private float oldHeightPosition;
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
            oldHeightPosition = oldPosition.y;
        }

        private void UnsubscribeEvents()
        {
            if (GameVisualizationController.Instance != null)
            {
                GameVisualizationController.Instance.OnGameVisualization -= OnGameVisualization;
                GameVisualizationController.Instance.OnVisualizationStart -= OnGameVisualizationStart;
            }

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

            // Find the visual tile that is below the visual robot
            Vector3Int worldTilePosition = transform.position.ToIntVector();
            Tile worldTile = FieldController.Instance.GetTileAtOrBelowIntPosition(worldTilePosition);

            // Update visual height
            position.y = GetVisualHeight(worldTile, t);

            transform.position = position;
            transform.forward = Vector3.Slerp(oldDirection, robot.Direction, t);

            // Rotate based on visual tile's normal
            if (worldTile != null)
            {
                Vector3 upVector = worldTile.NormalReporter.GetValue(robot);
                Vector3 forwardVector = Vector3.Cross(transform.right, upVector);
                rotationRoot.rotation = Quaternion.Slerp(rotationRoot.rotation, Quaternion.LookRotation(forwardVector, upVector), 10 * Time.deltaTime);
            }
        }

        private float GetVisualHeight(Tile worldTile, float t)
        {
            float targetHeight = robot.Tile.IntPosition.y;

            // If there is a tile below, get the tile's floor height and handle gravity
            if (worldTile != null)
            {
                // Store last known height when standing on tile
                // Will be used when not standing on tile anymore (see the else)
                oldHeightPosition = heightPosition;

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
                heightPosition = Mathf.Lerp(oldHeightPosition, targetHeight, t);
            }

            return heightPosition;
        }

#if UNITY_EDITOR
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

        private void OnDrawGizmos()
        {
            if (!isDebug && !robot.isDebugBot)
                return;

            Handles.Label(transform.position, robot.Tile.Visual.gameObject.name);
        }
#endif

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }
    }
}
