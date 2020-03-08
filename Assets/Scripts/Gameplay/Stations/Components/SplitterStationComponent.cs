using System.Collections;
using System.Collections.Generic;
using Gameplay.Products;
using Gameplay.Tiles.Components;
using UnityEngine;

namespace Gameplay.Stations.Components
{
    public class SplitterStationComponent : StationComponent, IProductProducer
    {
        [SerializeField]
        private UnloadTileComponent unloadTile;
        
        [SerializeField]
        private LoadTileComponent splittedProductLoadTile;
        
        [SerializeField]
        private LoadTileComponent restProductLoadTile;

        [SerializeField]
        private ProductData splitProductData;

        [SerializeField]
        private ProductData mixedProductData;
        
        public override void Initialize(Station station)
        {
            base.Initialize(station);
            
            splittedProductLoadTile.SetOutputProduct(splitProductData);
            splittedProductLoadTile.SetProductProducer(this);
            
            restProductLoadTile.SetOutputProduct(mixedProductData);
            
        }

        public bool CanProduceProduct(ProductData productData)
        {
        }

        public Product ProduceProduct(ProductData productData)
        {
                        
        }
    }
}
