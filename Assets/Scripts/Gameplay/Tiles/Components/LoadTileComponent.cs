﻿using Gameplay.Products;

namespace Gameplay.Tiles.Components
{
    public class LoadTileComponent : TileComponent
    {
        // In the future a load tile may have multiple product producers, for now just one
        public IProductProducer ProductProducer;
    }
}
