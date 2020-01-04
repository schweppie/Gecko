namespace Gameplay.Tiles.Components
{
    public class BlockingTileComponent : TileComponent
    {
        public override void DoStep()
        {
            GameStepController.Instance.PopulatePositionBuffer(tile.IntPosition);
        }
    }
}
