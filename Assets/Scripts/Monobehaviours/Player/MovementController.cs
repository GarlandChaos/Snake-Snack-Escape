using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody moverRigidbody = null;
    [SerializeField] private float forceStrength = 1f;

    public void Move()
    {
        if (InputController.Instance.Direction == Vector3.zero)
            return;

        moverRigidbody.transform.Translate(InputController.Instance.Direction * forceStrength * Time.fixedDeltaTime, Space.World);
    }
}