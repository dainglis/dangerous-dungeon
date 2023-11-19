using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    private string m_Label;

    private IObjectPool<IProjectile> m_Projectiles = null;



    // Will refactor this when defining different projectile types
    public GameObject ProjectileResource;

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
        projectile.OnDispose += () => m_Projectiles.Release(projectile);

        return projectile;
    }

    private void GetProjectile(IProjectile projectile) 
    {
        // projectile.GameObject.SetActive(true); responsibility of the spawner
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

    // Start is called before the first frame update
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
