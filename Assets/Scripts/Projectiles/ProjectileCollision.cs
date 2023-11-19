using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileCollision : MonoBehaviour
{

    public UnityEvent<GameObject> OnCollision;


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collided with {collision.gameObject.name}");
        OnCollision?.Invoke(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Triggered by {other.gameObject.name}");
        OnCollision?.Invoke(other.gameObject);
    }
}
