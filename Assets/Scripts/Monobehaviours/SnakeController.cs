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

    private List<Vector2Int> _snakePos = new();

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

    private void Awake()
    {
        snakeHeadAgent.moveSpeed = snakeSpeed;

        _targetSetter = head.GetComponent<TargetSetter>();

        _bodyParts = new List<BodySection>
        {
            new(head.transform, GetGridPos(head.transform)),
            new(tail.transform, GetGridPos(tail.transform)),
        };

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

            for(var i = 1; i < _snakePos.Count; i++)
            {
                if (_bodyParts.Count <= i) continue;
                StartCoroutine(MoveBody(_bodyParts[i], _snakePos[i]));
            }
        }

        UpdateDirections();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //AddBodyPart();
        }
    }

    private void UpdateSnakePositions()
    {
        _snakePos.Insert(0, GetGridPos(head.transform));
        if(_snakePos.Count > snakeSize + 2)
        {
            _snakePos.RemoveAt(_snakePos.Count-1);
        }

        _targetSetter.blockedCells = _snakePos;
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
        var targetPos = new Vector3(target.x, 0, target.y);

        while(t < 1f)
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
        return new Vector2Int(Mathf.FloorToInt(tr.transform.position.x + 0.5f), Mathf.FloorToInt(tr.transform.position.z + 0.5f));
    }
}
