using UnityEngine;

public class Tile
{
    private TileVisual visual;
    
    private Vector3Int intPosition;
    public Vector3Int IntPosition => intPosition;

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
        tile.intPosition = visual.IntPosition;
        return tile;
    }
}
