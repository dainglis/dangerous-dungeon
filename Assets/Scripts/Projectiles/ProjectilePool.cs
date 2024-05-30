using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    private string m_Label;

    private IObjectPool<IProjectile> m_Projectiles = null;


    // Will refactor this when defining different projectile types
    public GameObject ProjectileResource;

    public ParticleSystem ParticleSystem;

    public int ActiveProjectiles { get; private set; }


    public IObjectPool<IProjectile> Projectiles
    {
        get
        {
            m_Projectiles ??= new ObjectPool<IProjectile>(CreateProjectile, GetProjectile, ReleaseProjectile, DestroyProjectile,
                collectionCheck: true, defaultCapacity: 10, maxSize: 1000);

            return m_Projectiles;
        }
    }

    private IProjectile CreateProjectile()
    {
        IProjectile projectile = Instantiate(ProjectileResource)
            .GetComponent<IProjectile>();

        // Connect projectiles to particle system
        projectile.OnDispose += () =>
        {
            if (!ParticleSystem) { return; }
            // Perhaps we can better decorate projectiles using a decorator pattern... at a later date
            if (projectile.Decoration is bool particle && particle)
            {
                ParticleSystem.transform.position = projectile.Position;
                ParticleSystem.Play();
            }
        };

        // Event to release projectile from pool when disposed
        projectile.OnDispose += () => m_Projectiles.Release(projectile);

        return projectile;
    }

    private void GetProjectile(IProjectile projectile) 
    {
        ++ActiveProjectiles;
        m_Label = $"Active Projectiles: {ActiveProjectiles}";
    }

    private void ReleaseProjectile(IProjectile projectile)
    {
        projectile.Position = Vector3.down * 2;
        --ActiveProjectiles;
        m_Label = $"Active Projectiles: {ActiveProjectiles}";
    }

    private void DestroyProjectile(IProjectile projectile)
    {

    }

    void Start()
    {
        if (!ProjectileResource)
        {
            Debug.LogWarning("No projectile resource loaded");
        }

        StartCoroutine(InitCoroutine());
    }

    private IEnumerator InitCoroutine()
    {
        yield return null;
    }

    private void OnGUI()
    {
        GUILayout.Space(24);
        GUILayout.Label(m_Label);
    }
}
