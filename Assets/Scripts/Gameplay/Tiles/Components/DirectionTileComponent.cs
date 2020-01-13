using JP.Framework.Extensions;
using UnityEngine;

namespace Gameplay.Tiles.Components
{
    public class DirectionTileComponent : TileComponent
    {
        public Vector3Int GetDirection()
        {
            return transform.forward.RoundToIntVector();
        }
    }
}
