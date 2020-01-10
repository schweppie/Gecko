using System;

namespace Gameplay.Tiles.Components
{
    public class BlockingTileComponent : TileComponent, IOccupier
    {
        public override void DoNextStep()
        {
            GameStepController.Instance.AddOccupier(tile.IntPosition, this);
        }

        public void PickNewStrategy()
        {
            throw new Exception("BlockingTileComponent should never need to pick a new strategy");
        }
    }
}
