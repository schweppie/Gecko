using Gameplay.Products;

namespace Gameplay.Tiles.Components
{
    public class UnloadTileComponent : TileComponent
    {
        // In the future a load tile may have multiple product producers, for now just one
        public IProductReceiver ProductReceiver;
    }
}
