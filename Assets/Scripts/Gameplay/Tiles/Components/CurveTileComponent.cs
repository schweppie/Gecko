using Gameplay.Tiles.Reporters.Height;
using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public class CurveTileComponent : TileComponent
    {
        [SerializeField]
        private AnimationCurve heightCurve;

        public override void Initialize(Tile tile)
        {
            base.Initialize(tile);

            tile.SetHeightReporter(new CurveHeightReporter(tile, heightCurve));
        }
    }
}
