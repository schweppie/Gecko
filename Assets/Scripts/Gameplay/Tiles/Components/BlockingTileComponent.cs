namespace Gameplay.Tiles.Components
{
    public class BlockingTileComponent : TileComponent
    {
        public override void DoNextStep()
        {
            GameStepController.Instance.PopulatePositionBuffer(tile.IntPosition);
        }
    }
}
