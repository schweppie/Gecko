public class Tile
{
    private TileVisual visual;
    
    public TileType Type { get; }

    public Tile(TileType tileType)
    {
        this.Type = tileType;
    }

    /// <summary>
    /// Normally the data (this tile) is created first before the visual, but as we want to build levels in the
    /// Unity editor in scenes, the visuals already exists while the data does not yet 
    /// </summary>
    public static Tile ConstructTileFromVisual(TileVisual visual)
    {
        Tile tile = new Tile(visual.TileType);
        tile.visual = visual;
        return tile;
    }
}
