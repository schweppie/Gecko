using Gameplay.Tiles.Reporters.Enter;
using Gameplay.Tiles.Reporters.Exit;
using Gameplay.Tiles.Reporters.Height;
using Gameplay.Tiles.Reporters.Normal;
using UnityEngine;

namespace Gameplay.Tiles.Components
{
    [RequireComponent(typeof(BlockingDirectionTileComponent))]
    public class SlopeTileComponent : TileComponent
    {
        public override void Initialize(Tile tile)
        {
            base.Initialize(tile);

            tile.SetHeightReporter(new SlopeHeightReporter(tile));
            tile.SetExitReporter(new SlopeExitReporter(tile));
            tile.SetEnterReporter(new SlopeEnterReporter(tile));
            tile.SetNormalReporter(new SlopeNormalReporter(tile));
        }
    }
}
