namespace Gameplay.Tiles.Reporters.Height
{
    public abstract class BaseHeightReporter : DataReporter<float>
    {
        public BaseHeightReporter(Tile tile) : base(tile)
        {
        }
    }
}
