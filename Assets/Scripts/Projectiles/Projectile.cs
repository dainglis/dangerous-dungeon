using System;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    private const float Epsilon = 0.001f;

    private Vector3 m_Start;
    private Vector3 m_End;
    private float m_Speed;


    // IProjectile interface properties
    public Vector3 Position { get => transform.position; set => transform.position = value; }
    public Vector3 Start { get => m_Start; set => m_Start = value; }
    public Vector3 End { get => m_End; set => m_End = value; }
    public float Speed { get => m_Speed; set => m_Speed = value; }
    public Action OnDispose { get; set; }

    private bool disposed = false;


    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_End, m_Speed * Time.deltaTime);

        if (!disposed && ShouldDispose()) { Dispose(); }
    }

    public IProjectile SetTrajectory(Vector3 start, Vector3 end, float speed)
    {
        m_Start = start;
        m_End = end;
        m_Speed = speed;

        transform.position = start;
        transform.LookAt(end);

        return this;
    }

    public bool ShouldDispose()
    {
        return Vector3.Distance(transform.position, m_End) < Epsilon;
    }

    public IProjectile Activate()
    {
        disposed = false;
        enabled = true;

        return this;
    }

    public IProjectile Dispose()
    {
        if (disposed) return this;

        disposed = true;
        enabled = false;
        OnDispose?.Invoke();

        return this;
    }
}
