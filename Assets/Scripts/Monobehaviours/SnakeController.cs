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

    private Vector2Int _lastHeadPos;

    private List<Vector2Int> _snakePos = new();

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
        _bodyParts = new List<BodySection>
        {
            new(head.transform, GetGridPos(head.transform)),
            new(tail.transform, GetGridPos(tail.transform)),
        };

        UpdateSnakePositions();

        for (var i = 0; i < snakeSize; i++)
        {
            AddPart();
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
                StartCoroutine(MoveBody(_bodyParts[i], _snakePos[i]));
            }
        }
    }

    private void UpdateSnakePositions()
    {
        _snakePos.Insert(0, GetGridPos(head.transform));
        if(_snakePos.Count > snakeSize + 2)
        {
            _snakePos.RemoveAt(_snakePos.Count-1);
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

    public void AddPart()
    {
        var newBody = Instantiate(body, transform).transform;
        newBody.localPosition = head.transform.localPosition;
        _bodyParts.Insert(1, new BodySection(newBody, _bodyParts[0].position));
    }

    private Vector2Int GetGridPos(Transform tr)
    {
        return new Vector2Int(Mathf.FloorToInt(tr.transform.position.x + 0.5f), Mathf.FloorToInt(tr.transform.position.z + 0.5f));
    }
}
