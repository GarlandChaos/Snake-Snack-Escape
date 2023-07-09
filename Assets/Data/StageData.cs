using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Data/StageData")]
public class StageData : ScriptableObject
{
    public int stageId;
    public int keysRequired;
    public Vector2Int levelSize;
    public Vector2Int snakePosition;
    public int snakeSize;
    public float snakeSpeed;
    public int rats;

    [Header("Colors")]
    public Color groundColor;
    public Color backgroundColor;
    public Color wallColor;
    public Color wallDetailColor;
    public Color wallDetailTopColor;
}
