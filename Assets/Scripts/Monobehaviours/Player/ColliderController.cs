using UnityEngine;

public class ColliderController : MonoBehaviour
{
    [SerializeField] private IDamageable damageable = null;

    private void OnTriggerEnter(Collider other)
    {
        ICollectable collectable = other.GetComponent<ICollectable>();
        if (collectable != null)
            collectable.Collect();
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamager damager = collision.gameObject.GetComponent<IDamager>();
        if (damager != null)
            damageable.TakeDamage();
    }
}