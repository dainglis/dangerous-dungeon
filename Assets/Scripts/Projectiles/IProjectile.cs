using UnityEngine;
using UnityEngine.Events;

public interface IProjectile
{
    public float Speed { get; set; }

    public Vector3 Position { get; set; }

    public Vector3 Start { get; set; }

    public Vector3 End { get; set; }

    public object Decoration { get; set; }

    public UnityAction OnDispose { get; set; }

    public IProjectile Activate();

    public IProjectile Dispose();

    public IProjectile SetTrajectory(Vector3 start, Vector3 end, float speed);
}
