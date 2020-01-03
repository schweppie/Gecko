using UnityEngine;

namespace Utility
{
    public static class Extensions
    {
        public static Vector3Int ToIntVector(this Vector3 vector)
        {
            int x = Mathf.RoundToInt(vector.x);
            int y = Mathf.RoundToInt(vector.y);
            int z = Mathf.RoundToInt(vector.z);
            return new Vector3Int(x, y, z);            
        }
    }
}
