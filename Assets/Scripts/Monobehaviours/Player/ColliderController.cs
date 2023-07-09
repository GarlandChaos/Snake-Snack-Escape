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
        if (!canInteract) return;

        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
            interactable.Interact();
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamager damager = collision.gameObject.GetComponent<IDamager>();
        if (damager != null)
            damageable.TakeDamage();
    }
}