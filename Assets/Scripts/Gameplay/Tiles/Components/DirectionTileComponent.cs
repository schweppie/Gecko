using DG.Tweening;
using JP.Framework.Extensions;
using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public class DirectionTileComponent : TileComponent
    {
        private bool flipped = false;
        
        public Vector3Int GetDirection()
        {
            return transform.forward.ToIntVector();
        }

        public void FlipDirection()
        {
            flipped = !flipped;
            transform.DOLocalRotate(new Vector3(0, flipped ? 180 : 0, 0), .3f);
            //transform.localScale = new Vector3(1, 1, flipped?-1:1);
        }
    }
}
