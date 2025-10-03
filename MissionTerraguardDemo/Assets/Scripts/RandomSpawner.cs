using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;  // Array of objects to spawn
    public Vector2 spawnAreaMin;         // Bottom-left corner of spawn area
    public Vector2 spawnAreaMax;         // Top-right corner of spawn area
    public int numberOfObjects = 5;      // How many objects to spawn

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Pick a random object from the array
            GameObject obj = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

            // Pick a random position within the spawn area
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPosition = new Vector2(randomX, randomY);

            // Instantiate the object at the random position
            Instantiate(obj, spawnPosition, Quaternion.identity);
        }
    }
}
