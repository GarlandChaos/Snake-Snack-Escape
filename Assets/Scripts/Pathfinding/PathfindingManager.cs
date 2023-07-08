using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PathfindingManager : MonoBehaviour
{
    public static PathfindingManager Instance;

    public bool[,] collisionMap;
    public Vector2Int mapSize;
    public float cellSize;

    public Vector3 offSet;

    void Awake()
    {
        Instance = this;
        collisionMap = new bool[mapSize.x, mapSize.y];
        UpdateCollisionMap();
    }
    public string[] collisionLayers;
    private RaycastHit2D hit;
    private LayerMask mask;

    public int maxSearchs = 1000;
    public void UpdateCollisionMap()
    {
        mask = LayerMask.GetMask(collisionLayers);
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                var origin = new Vector2(x * cellSize, y * cellSize) + new Vector2(offSet.x, offSet.y);
                hit = Physics2D.CircleCast(origin, cellSize / 3f, Vector2.up, 0f, mask);
                collisionMap[x, y] = hit.collider != null;
            }
        }
    }

    public List<Vector2Int> GetPath(Vector3 origin, Vector3 target) => GetPath(new Vector2(origin.x, origin.z), new Vector2(target.x, target.z));
    public List<Vector2Int> GetPath(Vector2 origin, Vector2 target)
    {
        var initCell = ConvertPosToInt(origin);
        var targetCell = ConvertPosToInt(target);

        return GetPath(initCell, targetCell);
    }

    public Vector2Int ConvertPosToInt(Vector3 pos) => ConvertPosToInt(new Vector2(pos.x, pos.z));
    public Vector2Int ConvertPosToInt(Vector2 pos)
    {
        var halfCell = cellSize / 2f;
        pos -= (Vector2)offSet;
        return new Vector2Int(Mathf.FloorToInt((pos.x + halfCell) / cellSize), Mathf.FloorToInt((pos.y + halfCell) / cellSize));
    }

    public Vector3 ConvertPosToFloat(Vector2Int pos)
    {
        return new Vector3((pos.x) * cellSize, 0, (pos.y) * cellSize) + offSet;
    }
    public List<Vector2Int> GetPath(Vector2Int origin, Vector2Int target)
    {
        var cellsToCheck = new List<Vector2Int>() { target };
        var checkedCells = new Dictionary<Vector2Int, int>() { { target, 1 } };

        var searchStep = 0;

        while (cellsToCheck.Count > 0)
        {
            var currentCell = cellsToCheck[0];
            for (var x = -1; x < 2; x++)
            {
                for (var y = -1; y < 2; y++)
                {
                    var newCell = new Vector2Int(x + currentCell.x, y + currentCell.y);
                    if (x == 0 && y == 0 || x != 0 && y != 0) continue;
                    if (cellsToCheck.Contains(newCell) || checkedCells.ContainsKey(newCell)) continue;

                    if (BlockedCell(newCell) && newCell != origin && newCell != target)
                    {
                        checkedCells.Add(newCell, -1);
                    }
                    else
                    {
                        cellsToCheck.Add(newCell);
                        checkedCells.Add(newCell, checkedCells[currentCell] + 1);
                    }
                }
            }

            cellsToCheck.RemoveAt(0);

            if (currentCell == origin) break;

            searchStep += 1;
            if (searchStep >= 1000) return new List<Vector2Int>();
        }

        var value = new List<Vector2Int>() { origin };

        for (var i = checkedCells[origin] - 1; i > 1; i--)
        {
            var currentCell = value[value.Count - 1];
            var neighbours = new Vector2Int[]
            {
                currentCell + new Vector2Int(1, 0), currentCell + new Vector2Int(0, 1),
                currentCell + new Vector2Int(-1, 0), currentCell + new Vector2Int(0, -1),
                //diagonal movement.
                //currentCell + new Vector2Int(1, 1), currentCell + new Vector2Int(-1, -1),
                //currentCell + new Vector2Int(-1, 1), currentCell + new Vector2Int(1, -1),
            };

            for (var j = 0; j < neighbours.Length; j++)
            {
                if (checkedCells.ContainsKey(neighbours[j]) && checkedCells[neighbours[j]] == i)
                {
                    value.Add(neighbours[j]);
                    break;
                }
            }
        }
        value.Add(target);
        value.Reverse();

        value.RemoveAt(0);
        return value;
    }

    public bool BlockedCell(Vector2Int cell)
    {
        if (cell.x < 0 || cell.y < 0 || cell.x >= collisionMap.GetLength(0) || cell.y >= collisionMap.GetLength(1)) return false;
        return collisionMap[cell.x, cell.y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        for (var x = 0; x < mapSize.x; x++)
        {
            for (var y = 0; y < mapSize.y; y++)
            {
                Gizmos.DrawWireCube(new Vector3(x * cellSize, 0, y * cellSize) + offSet, Vector3.one * cellSize * 0.9f);
            }
        }
    }
}
