using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Transform rotatorTransform = null;
    [SerializeField] private float rotateSpeed = 1f;

    private void FixedUpdate()
    {
        if (InputController.Instance.Direction == Vector3.zero)
            return;

        float angle = Vector3.SignedAngle(rotatorTransform.forward, InputController.Instance.Direction, Vector3.up);
        Quaternion desiredRotation = rotatorTransform.rotation * Quaternion.Euler(0f, angle, 0f);
        rotatorTransform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(rotatorTransform.eulerAngles.y, desiredRotation.eulerAngles.y, rotateSpeed * Time.fixedDeltaTime), 0f);
    }
}