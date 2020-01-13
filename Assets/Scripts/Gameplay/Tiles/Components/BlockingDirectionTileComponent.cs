using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public class BlockingDirectionTileComponent : TileComponent
    {
        [SerializeField]
        private AnimationCurve heightCurve;

        public float GetHeight(Vector3 worldPosition)
        {
            worldPosition.y = transform.position.y;

            Vector3 tileEnterPosition = transform.position - (transform.forward * 0.5f);

            Vector3 pointDelta = worldPosition - tileEnterPosition;
            float t = Vector3.Dot(pointDelta, transform.forward);
            Vector3 projected = tileEnterPosition + transform.forward * t;

            Debug.DrawLine(projected, projected + Vector3.up, Color.magenta);

            return heightCurve.Evaluate(Vector3.Distance(tileEnterPosition, projected));
        }

        public bool IsDirectionPerpendicular(Vector3Int direction)
        {
            return Mathf.Approximately(Mathf.Abs(Vector3.Dot(direction, transform.forward)), 1f);
        }
    }
}
