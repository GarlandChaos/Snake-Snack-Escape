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
    private List<RatController> _rats;

    public void SpawnRat()
    {
        var newRat = Instantiate(rat, transform);
        _rats.Add(newRat);

        newRat.transform.position = pathfiding.ConvertPosToFloat(new Vector2Int(Random.Range(0, pathfiding.mapSize.x), Random.Range(0, pathfiding.mapSize.x)));
    }
}
