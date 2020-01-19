using Gameplay.Field;
using System;

namespace Gameplay.Tiles.Components
{
    public class BlockingTileComponent : TileComponent, IOccupier
    {
        public override void DoStaticStep()
        {
            FieldController.Instance.AddOccupier(tile.IntPosition, this);
        }

        public void PickNewStrategy()
        {
            throw new Exception("BlockingTileComponent should never need to pick a new strategy");
        }
    }
}
