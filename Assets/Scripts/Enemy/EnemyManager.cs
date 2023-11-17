using UnityEngine;
using UnityEngine.Pool;


public class EnemyManager : MonoBehaviour
{
    public string RespawnTag = "Respawn";


    public float SpawnTime = 2f;
    private float m_Elapsed = 0f;

    private GameObject[] m_SpawnPoints = null;


    public GameObject[] EnemyResources;
    private Vector3 m_CachePosition = Vector3.up * 1000;

    protected IObjectPool<IEnemy> m_EnemyPool;
    public IObjectPool<IEnemy> EnemyPool
    {
        get
        {
            m_EnemyPool ??= new ObjectPool<IEnemy>(CreateEnemy, SpawnEnemy, ReleaseEnemy, DestroyEnemy,
                collectionCheck: true, defaultCapacity: 10, maxSize: 100);

            return m_EnemyPool;
        }
    }

    public GameObject[] SpawnPoints
    {
        get
        {
            m_SpawnPoints ??= GameObject.FindGameObjectsWithTag(RespawnTag);

            return m_SpawnPoints;
        }
    }

    public IEnemy CreateEnemy()
    {
        Debug.Log("Create");

        var obj = Instantiate(
            EnemyResources[Random.Range(0, EnemyResources.Length)],
            m_CachePosition,
            Quaternion.identity,
            transform);

        IEnemy enemy = obj.GetComponent<IEnemy>();
        enemy.Controller.enabled = false;
        enemy.OnDeath += m_EnemyPool.Release;

        return enemy;
    }

    public void SpawnEnemy(IEnemy enemy)
    {
        Debug.Log("Spawning an Orc!");
        if (SpawnPoints == null) { return; }

        int index = Random.Range(0, SpawnPoints.Length);

        enemy.Controller.enabled = false;
        enemy.Controller.transform.position = SpawnPoints[index].transform.position;
        enemy.Controller.enabled = true;
    }

    public void ReleaseEnemy(IEnemy enemy)
    {
        Debug.Log("Release");
        enemy.Controller.enabled = false;
        enemy.Controller.transform.position = m_CachePosition;
    }

    public void DestroyEnemy(IEnemy enemy)
    {
        Debug.Log("Destroy");
    }


    private void Update()
    {
        m_Elapsed += Time.deltaTime;

        if (m_Elapsed >= SpawnTime)
        {
            EnemyPool.Get();
            m_Elapsed = 0f;
        }
    }
}
