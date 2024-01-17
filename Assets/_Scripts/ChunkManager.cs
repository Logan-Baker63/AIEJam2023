using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[System.Serializable]
public struct ChunkSpawn
{
    [SerializeField] GameObject m_chunkPrefab;
    [SerializeField] int m_weight;

    public GameObject prefab { get { return m_chunkPrefab; } }
    public int weight { get { return m_weight; } }
}

public class ChunkManager : MonoBehaviour
{
    [SerializeField] float m_chunkSize = 100;

    [SerializeField] List<ChunkSpawn> m_chunkSpawnPool;
    [SerializeField] List<Chunk> m_chunks = new();

    Runner m_runner;

    float m_lastChunkPosZ = -0;

    bool m_isFirstChunk = true;

    int m_chunkCounter;
    [SerializeField] GameObject m_miniBossChunk, m_bossChunk;
    [SerializeField] int m_miniBossFrequency = 10, m_bossFrequency = 40;

    private void Awake()
    {
        m_runner = FindObjectOfType<Runner>();

        m_chunkCounter = m_chunks.Count; // Should be 1

        SpawnChunk();
        //SpawnChunk();
    }

    public void ExitChunk(Chunk _chunk)
    {
        if (!m_isFirstChunk)
        {
            Chunk oldestChunk = m_chunks[0];
            Destroy(oldestChunk.gameObject);
            m_chunks.Remove(oldestChunk);
        }
        else m_isFirstChunk = false;

        //ShiftLevelBack();

        SpawnChunk();
    }

    void SpawnChunk()
    {
        m_chunkCounter++;

        GameObject chunkObj = Instantiate(GetRandomChunk(), new Vector3(0, 2, m_lastChunkPosZ + m_chunkSize), Quaternion.identity);
        chunkObj.transform.SetParent(transform);
        m_chunks.Add(chunkObj.GetComponent<Chunk>());
        m_lastChunkPosZ += m_chunkSize;

        m_runner.transform.SetParent(m_chunks[1].transform);
        Camera.main.transform.SetParent(m_chunks[1].transform);
        m_runner.m_followerParent.SetParent(m_chunks[1].transform);
    }

    GameObject GetRandomChunk()
    {
        if (m_chunkCounter % m_bossFrequency == 0) return m_bossChunk; // Return boss chunk
        else if (m_chunkCounter % m_miniBossFrequency == 0) return m_miniBossChunk; // Return mini boss chunk

        int totalWeight = 0;
        foreach (ChunkSpawn chunk in m_chunkSpawnPool) totalWeight += chunk.weight;

        int randomWeight = Random.Range(0, totalWeight);
        for (int i = 0; i < m_chunkSpawnPool.Count; ++i)
        {
            randomWeight -= m_chunkSpawnPool[i].weight;
            if (randomWeight < 0)
            {
                return m_chunkSpawnPool[i].prefab;
            }
        }

        Debug.LogWarning("Weight find failed");
        return m_chunkSpawnPool[0].prefab;
    }

    void ShiftLevelBack()
    {

        foreach (Transform child in transform)
        {
            child.position = new Vector3(child.position.x, child.position.y, child.position.z - m_chunkSize);
        }
        m_lastChunkPosZ -= m_chunkSize;
    }
}
