using System;
using UnityEngine;

public interface IProjectile
{
    public Vector3 Position { get; set; }

    public Vector3 Start { get; set; }

    public Vector3 End { get; set; }

    public float Speed { get; set; }

    public Action OnDispose { get; set; }

    public IProjectile Activate();

    public IProjectile Dispose();

    public IProjectile SetTrajectory(Vector3 start, Vector3 end, float speed);
}
