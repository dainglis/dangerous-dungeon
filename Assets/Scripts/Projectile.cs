using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static string ResourcePath = "Projectile";
    private static GameObject ProjectileResource;


    // TODO pooling
    //public Projectile[] ProjectilesPool;


    private Vector3 m_Start;
    private Vector3 m_End;
    private float m_Speed;

    private const float Epsilon = 0.001f;


    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_End, m_Speed * Time.deltaTime);

        //transform.position = Vector3.Lerp(m_Start, m_End, m_Distance);
        //m_Distance += m_Speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, m_End) < Epsilon )
        {
            Dispose();

        }
    }

    public static Projectile CreateProjectile(Vector3 start, Vector3 end, float speed)
    {
        // First, lazy-load
        if (!ProjectileResource) { ProjectileResource = Resources.Load<GameObject>(ResourcePath); }

        // If failed, error check
        if (!ProjectileResource) 
        { 
            Debug.LogWarning("No projectile resource loaded"); 
            return null; 
        }

        GameObject go = Instantiate(ProjectileResource);

        Projectile projectile = go.GetComponent<Projectile>();

        projectile.SetTrajectory(start, end, speed);
        return projectile;
    }

    public void SetTrajectory(Vector3 start, Vector3 end, float speed)
    {
        m_Start = start;
        m_End = end;
        m_Speed = speed;

        transform.position = start;
        transform.LookAt(end);
    }

    public void Dispose()
    {
        // TODO object pooling
        Destroy(gameObject);
    }
}
