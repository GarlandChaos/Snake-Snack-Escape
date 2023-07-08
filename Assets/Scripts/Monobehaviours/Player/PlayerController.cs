using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private MovementController movementController = null;
    [SerializeField] private RotationController rotationController = null;

    private void FixedUpdate()
    {
        movementController.Move();
        rotationController.Rotate();
    }

    public void TakeDamage()
    {
        //Invoke event to show game over UI, show damage particles, etc
        Destroy(gameObject);
    }
}