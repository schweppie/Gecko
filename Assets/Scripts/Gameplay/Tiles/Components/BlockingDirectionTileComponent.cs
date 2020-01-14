using Gameplay.Tiles.Reporters;
using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public class BlockingDirectionTileComponent : TileComponent
    {
        [SerializeField]
        private AnimationCurve heightCurve;

        public override void Initialize(Tile tile)
        {
            base.Initialize(tile);
            tile.Visual.SetHeightReporter(new CurveHeightReporter(heightCurve, tile));
        }

        public bool IsDirectionPerpendicular(Vector3Int direction)
        {
            return Mathf.Approximately(Mathf.Abs(Vector3.Dot(direction, transform.forward)), 1f);
        }
    }
}
