using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public bool canInteract = true;
    private IDamageable damageable = null;

    private void Awake()
    {
        damageable = GetComponent<IDamageable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameState.gameRunning) return;
        if (!canInteract) return;

        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
            interactable.Interact();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!GameState.gameRunning) return;
        IDamager damager = collision.gameObject.GetComponent<IDamager>();
        if (damager != null)
            damageable.TakeDamage();
    }
}