using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField]
    private GameObject head;
    [SerializeField]
    private GameObject body;
    [SerializeField]
    private GameObject tail;
    private List<BodySection> _bodyParts = new();
    [SerializeField]
    private int snakeSize = 0;
    [SerializeField]
    private float snakeSpeed = 1f;
    [SerializeField]
    private PathfindingAgent snakeHeadAgent;

    private Vector2Int _lastHeadPos;

    public List<Vector2Int> snakePositions = new();

    private TargetSetter _targetSetter;

    [System.Serializable]
    private class BodySection
    {
        public Transform transform;
        public Vector2Int position;

        public BodySection(Transform transform, Vector2Int position)
        {
            this.transform = transform;
            this.position = position;
        }
    }

    private void Start()
    {
        snakeHeadAgent.moveSpeed = snakeSpeed;

        _targetSetter = head.GetComponent<TargetSetter>();

        _bodyParts = new List<BodySection>
        {
            new(head.transform, GetGridPos(head.transform)),
            new(tail.transform, GetGridPos(tail.transform)),
        };

        snakePositions = new List<Vector2Int>();

        UpdateSnakePositions();

        var size = snakeSize;
        for (var i = 0; i < size; i++)
        {
            AddBodyPart();
        }
    }

    private void Update()
    {
        var headPos = GetGridPos(head.transform);
        if (_lastHeadPos != headPos)
        {
            _lastHeadPos = headPos;
            _bodyParts[0].position = headPos;

            UpdateSnakePositions();

            StopAllCoroutines();

            for(var i = 1; i < snakePositions.Count; i++)
            {
                if (_bodyParts.Count <= i) continue;
                StartCoroutine(MoveBody(_bodyParts[i], snakePositions[i]));
            }
        }

        UpdateDirections();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //AddBodyPart();
            ResetSnake(Random.Range(3, 10), Random.Range(1f, 3f), Vector2Int.zero);
        }
    }

    private void UpdateSnakePositions()
    {
        snakePositions.Insert(0, GetGridPos(head.transform));
        if(snakePositions.Count > snakeSize + 2)
        {
            snakePositions.RemoveAt(snakePositions.Count-1);
        }

        _targetSetter.blockedCells = snakePositions;
    }

    private void UpdateDirections()
    {
        for (var i = 1; i < _bodyParts.Count; i++)
        {
            _bodyParts[i].transform.forward = _bodyParts[i - 1].transform.position - _bodyParts[i].transform.position;
            _bodyParts[i].transform.GetChild(1).transform.position = Vector3.Lerp(_bodyParts[i - 1].transform.position, _bodyParts[i].transform.position, 0.5f);
            _bodyParts[i].transform.GetChild(1).transform.eulerAngles = new Vector3(0, Mathf.LerpAngle(_bodyParts[i - 1].transform.eulerAngles.y, _bodyParts[i].transform.eulerAngles.y, 0.5f), 0);
        }
    }

    private IEnumerator MoveBody(BodySection body, Vector2Int target)
    {
        var t = 0f;
        var originalPos = body.transform.position;
        var targetPos = PathfindingManager.Instance.ConvertPosToFloat(target);

        while (t < 1f)
        {
            t += snakeSpeed * Time.deltaTime;
            body.transform.position = Vector3.Lerp(originalPos, targetPos, t);
            yield return null;
        }

        body.position = GetGridPos(body.transform);
    }

    public void AddBodyPart()
    {
        snakeSize++;
        var newBody = Instantiate(body, transform).transform;
        newBody.localPosition = head.transform.localPosition;
        _bodyParts.Insert(1, new BodySection(newBody, _bodyParts[0].position));
    }

    private Vector2Int GetGridPos(Transform tr)
    {
        return PathfindingManager.Instance.ConvertPosToInt(new Vector2(tr.position.x, tr.position.z));
    }

    public void ResetSnake(int snakeSize, float speed, Vector2Int pos)
    {
        StopAllCoroutines();

        for(var i = 1; i < _bodyParts.Count - 1; i++)
        {
            Destroy(_bodyParts[i].transform.gameObject);
        }
        _bodyParts = new List<BodySection>();

        snakeSpeed = speed;
        this.snakeSize = snakeSize;

        var position = PathfindingManager.Instance.ConvertPosToFloat(pos);

        head.transform.position = position;
        tail.transform.position = position;

        Start();
    }
}
