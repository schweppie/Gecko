using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public class TileTypeComponent : TileComponent
    {
        [SerializeField]
        private TileType type = TileType.Basic;
        public TileType TileType => type;        
    }
}
