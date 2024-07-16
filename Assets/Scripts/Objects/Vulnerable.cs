using UnityEngine;
using UnityEngine.Events;

public class Vulnerable : MonoBehaviour, IVulnerable
{
    private bool dirty = false;

    private float restoreCache;
    public float RestoreTime = 5;

    public float HitPoints { get; set; } = 5;

    public bool IsDamaged => HitPoints <= 0;

    [SerializeField] private UnityEvent m_Damaged;
    [SerializeField] private UnityEvent m_Restored;

    public UnityEvent Damaged => m_Damaged;

    public UnityEvent Restored => m_Restored;

    public void Update()
    {
        if (dirty)
        {
            dirty = false;

            if (IsDamaged)
            {
                Damaged?.Invoke();
                restoreCache = 0;
            }
            else
            {
                Restored?.Invoke();
            }
        }

        if (IsDamaged)
        {
            restoreCache += Time.deltaTime;
        }

        if (restoreCache > RestoreTime)
        {
            dirty = true;
        }
    }

    public virtual void Hit(IProjectile projectile)
    {
        (this as IVulnerable).HandleCollision(projectile);

        Debug.Log($"Object {name} now has {HitPoints} hitpoints");

        if (IsDamaged && !dirty)
        {
            dirty = true;
        }
    }
}
