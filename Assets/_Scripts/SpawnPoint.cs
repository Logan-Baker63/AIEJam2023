using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Spawnable
{
    [SerializeField] GameObject m_prefab;
    [SerializeField] int m_spawnWeight;

    public GameObject prefab { get { return m_prefab; } }
    public int weight { get { return m_spawnWeight; } }
}

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] List<Spawnable> m_spawnables;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }

    public void Spawn()
    {
        GameObject obj = Instantiate(GetRandomSpawnable(), transform.position, transform.rotation);
        obj.transform.SetParent(transform.parent);
    }

    GameObject GetRandomSpawnable()
    {
        int totalWeight = 0;
        foreach (Spawnable spawnable in m_spawnables) totalWeight += spawnable.weight;

        int randomWeight = Random.Range(0, totalWeight);
        for (int i = 0; i < m_spawnables.Count; ++i)
        {
            randomWeight -= m_spawnables[i].weight;
            if (randomWeight < 0)
            {
                return m_spawnables[i].prefab;
            }
        }

        Debug.LogWarning("Weight find failed");
        return m_spawnables[0].prefab;
    }
}
