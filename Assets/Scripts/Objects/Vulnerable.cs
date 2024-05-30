using UnityEngine;
using UnityEngine.Events;

public class Vulnerable : MonoBehaviour, IVulnerable
{
    public float Points { get; set; } = 5;

    public bool broken = false;
    public UnityEvent Break;

    public void Update()
    {
        CheckBroken();
    }

    public void CheckBroken()
    {
        if (Points < 0 && !broken)
        {
            broken = true;
            Break?.Invoke();
        }
    }

    public void Hit(IProjectile projectile)
    {
        projectile.Decoration = true;
        projectile.Dispose();

        if (broken) { return; }
        Points--;
        Debug.Log($"Object {name} now has {Points} hitpoints");
    }
}
