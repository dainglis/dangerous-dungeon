public interface IVulnerable
{
    public float Points { get ; set; }

    public abstract void Hit(IProjectile projectile);
}
