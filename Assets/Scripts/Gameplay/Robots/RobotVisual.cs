using DG.Tweening;
using Gameplay.Tiles;
using UnityEngine;

namespace Gameplay.Robots
{
    public class RobotVisual : MonoBehaviour
    {
        public Tile CurrentTile;
        private Tile oldTile;
        
        private Vector3Int direction;

        public void Initialize()
        {
            var randomDir = Random.Range(1, 5);
            direction = new Vector3Int(randomDir == 1 ? 1 : 0 + randomDir == 2 ? -1 : 0, 0,
                randomDir == 3 ? 1 : 0 + randomDir == 4 ? -1 : 0);
            transform.LookAt(transform.position + direction);
            transform.localPosition = CurrentTile.IntPosition;
            
            GameStepController.Instance.OnGameStep += OnGameStep;
            GameVisualizationController.Instance.OnGameVisualization += OnGameVisualization;
            GameVisualizationController.Instance.OnVisualizationStart += OnGameVisualizationStart;

            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutQuart);
        }

        private void OnGameVisualizationStart()
        {
            if (CurrentTile == null)
            {
                // TODO This is a hack to let the bot move 1 forward and then fall, seperate from the visualization
                GameVisualizationController.Instance.OnVisualizationStart -= OnGameVisualizationStart;
                MoveForwardAndFall();
            }
        }

        private void OnGameVisualization(int step, float t)
        {
            if (oldTile == null)
                return;
            
            transform.position = Vector3.Lerp(oldTile.IntPosition, CurrentTile.IntPosition, t);
        }

        private void OnGameStep(int step)
        {
            oldTile = CurrentTile;
            Vector3Int nextPosition = CurrentTile.IntPosition + direction;
            Tile nextTile = FieldController.Instance.GetTileAtIntPosition(nextPosition);
            CurrentTile = nextTile;
        }

        private void MoveForwardAndFall()
        {
            transform.DOLocalMove(transform.localPosition + direction, 1f)
                .SetEase(Ease.Linear)
                .OnComplete(() => FallAndDestroy());            
        }

        private void FallAndDestroy()
        {
            transform.DOLocalMove(transform.localPosition + new Vector3(0, -2, 0), 1f)
                .SetEase(Ease.InCubic)
                .OnComplete(() => Destroy(gameObject));
        }

        private void OnDestroy()
        {
            GameStepController.Instance.OnGameStep -= OnGameStep;
            GameVisualizationController.Instance.OnGameVisualization -= OnGameVisualization;
            GameVisualizationController.Instance.OnVisualizationStart -= OnGameVisualizationStart;
        }
    }
}
