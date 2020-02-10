using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public abstract class TileComponent : MonoBehaviour
    {
        protected Tile tile;
        public Tile Tile => tile;
        
        public virtual void Initialize(Tile tile)
        {
            this.tile = tile;
        }
        
        public virtual void DoStaticStep()
        {
        }
    }
}
