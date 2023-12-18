using UnityEngine;

public class SpawnRandoms : MonoBehaviour
{
    public GameObject objectToSpawn; // The prefab of the object to spawn
    public float spawnRadius = 5f; // The radius within which to spawn objects
    public int numberOfObjects = 10; // The number of objects to spawn

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * spawnRadius; // Get a random position within the spawn radius
            randomPosition.y = 0f; // Ensure the objects are spawned on the same plane as the spawner
            Instantiate(objectToSpawn, transform.position + randomPosition, Quaternion.identity); // Spawn the object at the random position

        }
    }
}