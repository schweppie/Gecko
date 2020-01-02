using UnityEngine;

public class TileVisual : MonoBehaviour
{
    [SerializeField]
    private TileType type;

    public TileType TileType => type;

    public Vector3Int IntPosition
    {
        get
        {
            Vector3 position = transform.position;
            int x = Mathf.RoundToInt(position.x);
            int y = Mathf.RoundToInt(position.y);
            int z = Mathf.RoundToInt(position.z);
            return new Vector3Int(x, y, z);
        }
    }
}
