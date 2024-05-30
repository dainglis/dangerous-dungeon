using UnityEngine;
using UnityEngine.Events;

public class Vulnerable : MonoBehaviour, IVulnerable
{
    public float HitPoints { get; set; } = 5;

    public bool broken = false;
    public UnityEvent Break;

    public void Update()
    {
        CheckStatus();
    }

    public void CheckStatus()
    {
        if ((this as IVulnerable).IsDamaged && !broken)
        {
            broken = true;
            Break?.Invoke();
        }
    }

    public virtual void Hit(IProjectile projectile)
    {
        (this as IVulnerable).HandleCollision(projectile);

        Debug.Log($"Object {name} now has {HitPoints} hitpoints");
    }
}
