using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    #region Singleton
    public static LevelController Instance { get; private set; }

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

    [SerializeField] private Transform levelGround;
    [SerializeField] private Material levelMat;
    [SerializeField] private Material wallMat;
    [SerializeField] private Material wallDetailMat;
    [SerializeField] private Material wallDetailTopMat;

    [SerializeField] private Transform wallLeft;
    [SerializeField] private Transform wallRight;
    [SerializeField] private Transform wallTop;
    [SerializeField] private Transform wallBottom;
    [SerializeField] private Transform door;
    [SerializeField] private Transform player;

    [SerializeField] private PathfindingManager pathfindingManager;

    [SerializeField] private SnakeController snakeController;

    [SerializeField] private Camera cam;

    public Vector2Int size;

    [ContextMenu("TestSize")]
    public void SetLevelSize() => SetLevelSize(size);
    private void SetLevelSize(Vector2Int size)
    {
        levelGround.localScale = new Vector3(size.x/10f, 1f, size.y/10f);
        levelMat.mainTextureScale = new Vector2(size.x/2f, size.y/2f);

        wallLeft.position = new Vector3(-size.x/2f, 0, 0);
        wallLeft.localScale = new Vector3(1f, 1f, size.y);
        wallRight.position = new Vector3(size.x/2f, 0, 0);
        wallRight.localScale = new Vector3(1f, 1f, size.y);

        wallTop.position = new Vector3(0, 0, -size.y / 2f);
        wallTop.localScale = new Vector3(size.x, 1f, 1f);
        wallBottom.position = new Vector3(0, 0, size.y / 2f);
        wallBottom.localScale = new Vector3(size.x, 1f, 1f);

        door.position = new Vector3(0, 0, size.y / 2f);
        door.gameObject.SetActive(true);
        door.GetChild(0).gameObject.SetActive(false);

        pathfindingManager.mapSize = size;
        pathfindingManager.offSet = new Vector3(-size.x / 2f + 0.5f, 0, -size.y / 2f + 0.5f);
        pathfindingManager.UpdateCollisionMap();

        player.transform.position = new Vector3(0, 0, 0);
    }

    public void SetMap(StageData stage)
    {
        SetLevelSize(stage.levelSize);
        snakeController.ResetSnake(stage.snakeSize, stage.snakeSpeed, stage.snakePosition);

        levelMat.color = stage.groundColor;
        cam.backgroundColor = stage.backgroundColor;
        wallMat.color = stage.wallColor;
        wallDetailMat.color = stage.wallDetailColor;
        wallDetailTopMat.color = stage.wallDetailTopColor;

        RatsManager.Instance.SpawnRats(stage.rats);
    }
}
