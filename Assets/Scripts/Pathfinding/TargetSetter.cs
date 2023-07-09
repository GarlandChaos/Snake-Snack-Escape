using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSetter : MonoBehaviour
{
    private PathfindingAgent _agent;
    Vector3 lastPos;

    private Transform _target;

    public List<Vector2Int> blockedCells = new List<Vector2Int>();

    private void Awake()
    {
        _agent = GetComponent<PathfindingAgent>();
        _target = RatsManager.Instance.GetClosestRat(transform);
    }

    private void Update()
    {
        var newTarget = RatsManager.Instance.GetClosestRat(transform);
        if(newTarget != _target)
        {
            _target = newTarget;
            _agent.SetPath(_target.position, blockedCells);
            lastPos = _target.position;
        }

        if(lastPos != _target.position)
        {
            lastPos = _target.position;
            _agent.SetPath(_target.position, blockedCells);
        }
    }
}
