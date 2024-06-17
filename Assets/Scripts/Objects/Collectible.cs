using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public interface ICollectible
{
    object Collect();
}
public class Collectible : MonoBehaviour, ICollectible
{

    public UnityEvent<Collectible> Collected;

    public object Collect()
    {
        Collected?.Invoke(this);


        // Pass something back to the collector
        return 1;
    }

}
