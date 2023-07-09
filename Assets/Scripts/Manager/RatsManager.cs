using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatsManager : MonoBehaviour
{
    #region Singleton
    public static RatsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public PathfindingManager pathfiding;
    public RatController rat;
    private List<RatController> _rats = new List<RatController>();
    [SerializeField] private Transform player;

    public Transform GetClosestRat(Transform snakeHead)
    {
        var tr = player;

        var distance = 1000f;

        foreach(var rat in _rats)
        {
            var newDist = Vector3.Distance(snakeHead.position, rat.transform.position);
            if(newDist < distance)
            {
                distance = newDist;
                tr = rat.transform;
            }
        }

        return tr;
    }

    public bool ShouldDropKey()
    {
        if (_rats.Count <= StageController.Instance.KeysLeft) return true;

        return Random.Range(0, 100) > 90;
    }

    public void SpawnRats(int amount)
    {
        for (var i = 0; i < _rats.Count; i++)
        {
            Destroy(_rats[i].gameObject);
        }

        _rats = new List<RatController>();

        for (var i = 0; i < amount; i++)
        {
            SpawnRat();
        }
    }

    public void SpawnRat()
    {
        var newRat = Instantiate(rat, transform);
        _rats.Add(newRat);

        newRat.transform.position = pathfiding.ConvertPosToFloat(new Vector2Int(Random.Range(-pathfiding.mapSize.x + 1, 1), Random.Range(-pathfiding.mapSize.y + 1, 1)));
    }

    public void RemoveRat(RatController rat) => _rats.Remove(rat);
}
