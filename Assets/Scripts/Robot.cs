using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Robot : MonoBehaviour
{
    private Vector2Int currentGridPosition = new Vector2Int();
    private Vector2Int direction = new Vector2Int(0,1);

    void Start()
    {
        currentGridPosition = GetGridPosition(transform.localPosition);
        DoStep();
    }

    private void DoStep()
    {
        Vector2Int nextGridPosition = currentGridPosition + direction;
        Vector3 nextPosition = new Vector3(nextGridPosition.x, transform.localPosition.y, nextGridPosition.y);
        transform.DOLocalMove(nextPosition, 1f).OnComplete(OnTweenEnd).SetEase(Ease.InOutCubic);
    }

    private void OnTweenEnd()
    {
        currentGridPosition = GetGridPosition(transform.localPosition);
        DoStep();
    }

    private Vector2Int GetGridPosition(Vector3 position)
    {
        return new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
    }
    
    void Update()
    {
        //Field.Instance.GetTileOnPosition()
        //transform.localPosition += transform.forward * Time.deltaTime;
    }
}
