namespace Gameplay.Tiles.Components
{
    public class BlockingTileComponent : TileComponent
    {
        public override void DoNextStep()
        {
            GameStepController.Instance.AddOccupier(tile.IntPosition, tile);
        }
    }
}
