using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSetter : MonoBehaviour
{
    private PathfindingAgent _agent;
    [SerializeField] private Transform target;
    Vector3 lastPos;

    private void Awake()
    {
        _agent = GetComponent<PathfindingAgent>();
    }

    private void Update()
    {
        if(lastPos != target.position)
        {
            lastPos = target.position;
            _agent.SetPath(target.position);
        }
    }
}
