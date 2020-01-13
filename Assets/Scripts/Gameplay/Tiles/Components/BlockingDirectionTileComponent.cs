using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public class BlockingDirectionTileComponent : TileComponent
    {
        public bool IsDirectionPerpendicular(Vector3Int direction)
        {
            return Mathf.Approximately(Mathf.Abs(Vector3.Dot(direction, transform.forward)), 1f);
        }
    }
}
