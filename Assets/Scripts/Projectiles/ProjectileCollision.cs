using UnityEngine;
using UnityEngine.Events;

public class ProjectileCollision : MonoBehaviour
{

    public UnityEvent<IProjectile> OnCollision;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<IProjectile>(out var projectile)) { return; }

        Debug.Log($"Triggered by {other.gameObject.name}");
        OnCollision?.Invoke(projectile);
    }
}
