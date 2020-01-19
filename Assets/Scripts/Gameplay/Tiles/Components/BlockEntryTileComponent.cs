namespace Gameplay.Tiles.Components
{
    // The BlockEntryTileComponent is different from BlockingTileComponent in that no one may enter this tile, but
    // the tile itself is not occupied. This is useful for the spawn station where the spawn tile may not be entered by
    // any robot except the spawning robot
    public class BlockEntryTileComponent : TileComponent
    {
    }
}
