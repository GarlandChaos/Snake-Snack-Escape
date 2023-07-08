using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingAgent : MonoBehaviour
{
    private List<Vector2Int> path = new List<Vector2Int>();

    public float distanceTreshold = 0.1f;

    public float moveSpeed;
    public bool Moving => path.Count > 0;

    private IEnumerator Move(Vector2 target)
    {
        Debug.Log("Start");
        while (path.Count > 0)
        {
            var currentTarget = PathfindingManager.Instance.ConvertPosToFloat(path[0]);

            while (Vector3.Distance(currentTarget, transform.position) > distanceTreshold)
            {
                Debug.Log(currentTarget);
                if (PathfindingManager.Instance.BlockedCell(path[0]))
                {
                    SetPath(target);
                    yield break;
                }

                transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);
                yield return null;
            }

            path.RemoveAt(0);
            yield return null;
        }
        Debug.Log("End");
    }

    public void SetPath(Vector3 target)
    {
        Stop();
        path = PathfindingManager.Instance.GetPath(target, transform.position);
        StartCoroutine(Move(target));
    }

    public void Stop()
    {
        StopAllCoroutines();
        path = new List<Vector2Int>();
    }

    private void OnDrawGizmosSelected()
    {
        if (PathfindingManager.Instance == null) return;

        Gizmos.color = Color.green;

        for (var i = 1; i < path.Count; i++)
        {
            var posA = PathfindingManager.Instance.ConvertPosToFloat(path[i]);
            var posB = PathfindingManager.Instance.ConvertPosToFloat(path[i - 1]);
            Gizmos.DrawLine(posA, posB);
        }
    }
}
