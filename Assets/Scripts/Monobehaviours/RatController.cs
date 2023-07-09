using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour, IDamageable
{
    private Vector2Int mapSize;
    private PathfindingAgent _pathfindingAgent;

    private void Start()
    {
        _pathfindingAgent = GetComponent<PathfindingAgent>();
        mapSize = PathfindingManager.Instance.mapSize;
    }

    private float _timer = 0f;

    private void Update()
    {
        if (!GameState.gameRunning) return;

        if(_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _timer = Random.Range(2f, 5f);
            var randomPos = new Vector2Int(Random.Range(0, mapSize.x), Random.Range(0, mapSize.x));
            _pathfindingAgent.SetPath(PathfindingManager.Instance.ConvertPosToFloat(randomPos), new List<Vector2Int>());
        }
    }

    public void TakeDamage()
    {
        RatsManager.Instance.RemoveRat(this);

        var dropKey = RatsManager.Instance.ShouldDropKey();

        if (dropKey)
        {
            KeyManager.Instance.SpawnKey(transform.position);
        }

        Destroy(gameObject);
    }
}
