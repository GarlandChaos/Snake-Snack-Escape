using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingAgent : MonoBehaviour
{
    [SerializeField]
    private bool debugVisualization;

    private List<Vector2Int> path = new List<Vector2Int>();

    public float distanceTreshold = 0.1f;

    public float moveSpeed;
    public bool Moving => path.Count > 0;

    [SerializeField]
    private Transform rotateTransform;
    [SerializeField]
    private Transform aim;

    private IEnumerator Move(Vector2 target, List<Vector2Int> blockedCells)
    {
        while (path.Count > 0)
        {
            var currentTarget = PathfindingManager.Instance.ConvertPosToFloat(path[0]);

            var initialEuler = rotateTransform.eulerAngles.y;
            aim.position = transform.position;
            aim.forward = currentTarget - aim.position;

            var t = 0f;

            while (Vector3.Distance(currentTarget, transform.position) > distanceTreshold)
            {
                if (PathfindingManager.Instance.BlockedCell(path[0]))
                {
                    SetPath(target, blockedCells);
                    yield break;
                }

                if (t < 1f)
                {
                    t += (moveSpeed * 2) * Time.deltaTime;
                    rotateTransform.eulerAngles = new Vector3(0, Mathf.LerpAngle(initialEuler, aim.eulerAngles.y, t), 0);
                }
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);

                yield return null;
            }

            path.RemoveAt(0);
            yield return null;
        }
    }

    public void SetPath(Vector3 target, List<Vector2Int> blockedCells)
    {
        Stop();
        path = PathfindingManager.Instance.GetPath(target, transform.position, blockedCells);

        StartCoroutine(Move(target, blockedCells));
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
        if (!debugVisualization) return;
        if (PathfindingManager.Instance == null) return;

        Gizmos.color = Color.yellow;

        for (var i = 1; i < path.Count; i++)
        {
            var posA = PathfindingManager.Instance.ConvertPosToFloat(path[i]);
            var posB = PathfindingManager.Instance.ConvertPosToFloat(path[i - 1]);
            Gizmos.DrawLine(posA, posB);
        }
    }
}
