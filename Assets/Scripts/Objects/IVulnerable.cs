public interface IVulnerable
{
    public float HitPoints { get; set; }

    public bool IsDamaged => HitPoints <= 0;

    public void HandleCollision(IProjectile projectile)
    {
        projectile.Decoration = true;

        // Eventually projectiles may "pierce" and should 
        // not be disposed on contact
        projectile.Dispose();

        // Avoids negative hitpoints
        if (IsDamaged) { return; }

        // Subtract from hitpoints on collision
        HitPoints--;
    }
}
