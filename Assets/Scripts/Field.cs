using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField]
    private int fieldSize = 5;
    
    [SerializeField]
    private GameObject baseTile;
    
    private List<List<GameObject>> tiles = new List<List<GameObject>>();
    
    void Start()
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
    }

    void Update()
    {
        
    }
}
