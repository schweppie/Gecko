using Gameplay.Robots;

namespace Gameplay.Tiles.Reporters.Height
{
    public class DefaultHeightReporter : BaseHeightReporter
    {
        public DefaultHeightReporter(Tile tile) : base(tile)
        {
        }

        public override float GetValue(Robot robot)
        {
            return tile.IntPosition.y;
        }
    }
}
