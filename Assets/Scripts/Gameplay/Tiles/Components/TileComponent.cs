using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public abstract class TileComponent : MonoBehaviour
    {
        protected Tile tile;
        
        public void SetTile(Tile tile)
        {
            this.tile = tile;
        }
        
        public virtual void DoNextStep()
        {
        }

        public virtual void DoPrevStep()
        {
        }
    }
}
