using Gameplay.Robots;

namespace Gameplay.Tiles.Reporters
{
    public class DefaultHeightReporter : BaseHeightReporter
    {
        public DefaultHeightReporter(Tile tile) : base(tile)
        {
        }

        public override float GetValue(Robot robot, float t)
        {
            return tile.IntPosition.y;
        }
    }
}
