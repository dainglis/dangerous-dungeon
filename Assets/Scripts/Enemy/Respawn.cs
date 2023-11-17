using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public string RespawnTag = "Respawn";

    private static GameObject[] m_SpawnPoints = null;

    // Start is called before the first frame update
    void Start()
    {
        if (m_SpawnPoints == null)
        {
            m_SpawnPoints = GameObject.FindGameObjectsWithTag(RespawnTag);
        }
    }

    public void DoRespawn()
    {
        if (m_SpawnPoints == null) { return; }

        int index = Random.Range(0, m_SpawnPoints.Length);

        Vector3 spawnPosition = m_SpawnPoints[index].transform.position;

        transform.position = new Vector3(spawnPosition.x, transform.position.y, spawnPosition.z);
        Debug.Log($"Moving enemy to {transform.position}");
    }
}
