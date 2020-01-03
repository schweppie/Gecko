using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public class TileTypeComponent : TileComponent
    {
        [SerializeField] private TileType type;
        public TileType TileType => type;        
    }
}
