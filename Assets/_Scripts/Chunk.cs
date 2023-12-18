using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    ChunkManager m_chunkManager;

    private void Awake()
    {
        m_chunkManager = FindObjectOfType<ChunkManager>();
        foreach (SpawnPoint spawner in GetComponentsInChildren<SpawnPoint>()) spawner.Spawn();
    }

    public void ExitChunk(Collider _collider)
    {
        m_chunkManager.ExitChunk(this);
    }
}
