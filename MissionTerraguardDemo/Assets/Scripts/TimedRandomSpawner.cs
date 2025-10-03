using UnityEngine;
using System.Collections;

public class TimedRandomSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array of prefabs
    public Vector2 spawnAreaMin;        // Bottom-left of spawn area
    public Vector2 spawnAreaMax;        // Top-right of spawn area
    public float spawnDelay = 3f;       // Wait before first spawn
    public float lifeTime = 10f;        // How long the object stays

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(spawnDelay); // Wait before first spawn

        while (true)
        {
            // Pick random prefab
            GameObject objPrefab = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

            // Pick random position
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPos = new Vector2(randomX, randomY);

            // Instantiate object and give it a "life timer"
            GameObject currentObject = Instantiate(objPrefab, spawnPos, Quaternion.identity);
            currentObject.AddComponent<DestroyAfterTime>().lifeTime = lifeTime;

            // Wait until this object is destroyed before spawning another
            while (currentObject != null)
                yield return null;
        }
    }
}
