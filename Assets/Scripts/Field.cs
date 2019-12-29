using System.Collections.Generic;
using UnityEngine;
using Utility;

public class Field : SingletonBehaviour<Field>
{
    [SerializeField]
    private int fieldSize = 5;
    
    [SerializeField]
    private GameObject baseTile;

    [SerializeField]
    private Robot robot;
    
    private List<List<GameObject>> tiles = new List<List<GameObject>>();
    
    void Awake()
    {
        transform.position = new Vector3(-fieldSize/2.0f,0,-fieldSize/2.0f);
        for (int y = 0; y < fieldSize; y++)
        {
            List<GameObject> tileRow = new List<GameObject>();
            tiles.Add(tileRow);
            for (int x = 0; x < fieldSize; x++)
            {
                GameObject tile = GameObject.Instantiate(baseTile, transform);
                tile.transform.localPosition = new Vector3(x, 0, y);
                tileRow.Add(tile);
            }
        }

        robot.transform.SetParent(transform);
        Vector2Int robotPos = new Vector2Int(
            Mathf.Clamp(Mathf.RoundToInt(robot.transform.localPosition.x), 0, fieldSize-1),
            Mathf.Clamp(Mathf.RoundToInt(robot.transform.localPosition.z), 0, fieldSize-1));
        robot.transform.localPosition = new Vector3(robotPos.x, robot.transform.localPosition.y, robotPos.y);
    }

    public bool IsPositionOnGrid(Vector2Int position)
    {
        return GetTileOnPosition(position) != null;
    }

    public GameObject GetTileOnPosition(Vector2Int position)
    {
        if (position.x < 0 || position.x >= fieldSize)
            return null;
        if (position.y < 0 || position.y >= fieldSize)
            return null;
        return tiles[position.y][position.x];
    }

    void Update()
    {
        
    }
}
