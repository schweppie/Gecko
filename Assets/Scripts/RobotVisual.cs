using DG.Tweening;
using UnityEngine;

public class RobotVisual : MonoBehaviour
{
    public Tile CurrentTile;
    private Vector3Int direction;

    void Start()
    {
        var randomDir = Random.Range(1, 5);
        direction = new Vector3Int(randomDir==1?1:0 + randomDir==2?-1:0, 0,randomDir==3?1:0 + randomDir==4?-1:0);
        transform.LookAt(transform.position + direction);
        transform.localPosition = CurrentTile.IntPosition;
        DoStep();
    }

    private void DoStep()
    {
        if (CurrentTile == null)
        {
            FallAndDestroy();
            return;
        }

        Vector3Int nextPosition = CurrentTile.IntPosition + direction;
        Tile nextTile = Field.Instance.GetTileAtIntPosition(nextPosition);
        CurrentTile = nextTile;
        transform.DOLocalMove(nextPosition, 1f).OnComplete(OnTweenEnd).SetEase(Ease.Linear);
    }

    private void FallAndDestroy()
    {
        transform.DOLocalMove(transform.localPosition + new Vector3(0, -2, 0), 1f)
            .SetEase(Ease.InCubic)
            .OnComplete(() => Destroy(gameObject));
    }

    private void OnTweenEnd()
    {
        DoStep();
    }
}
