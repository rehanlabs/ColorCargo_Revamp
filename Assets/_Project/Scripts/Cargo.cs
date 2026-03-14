using System.Collections;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject secondCubePrefab; // The second cube prefab to be spawned less frequently
    public float minSpawnInterval = 4f; // Minimum time interval between cube spawns
    public float maxSpawnInterval = 7f; // Maximum time interval between cube spawns
    public float startTimeForRandomSpawn = 30f; // Time after which random spawn starts

    private bool isRandomSpawnEnabled = false;
    private bool shouldSpawnSecondCube = false;

    private void Start()
    {
        // Start spawning cubes
        StartCoroutine(SpawnCubes());
    }

    private void Update()
    {
        // Enable random spawn after the specified start time
        if (!isRandomSpawnEnabled && Time.time >= startTimeForRandomSpawn)
        {
            isRandomSpawnEnabled = true;
        }
    }

    private void SpawnCube(GameObject prefab)
    {
        // Instantiate a new cube at the spawner's position
        GameObject newCube = Instantiate(prefab, transform.position, Quaternion.identity);
    }

    private IEnumerator SpawnCubes()
    {
        while (true)
        {
            // Determine which cube prefab to spawn
            GameObject prefabToSpawn;
            if (isRandomSpawnEnabled)
            {
                if (shouldSpawnSecondCube)
                {
                    prefabToSpawn = secondCubePrefab;
                    shouldSpawnSecondCube = false;
                }
                else
                {
                    prefabToSpawn = cubePrefab;
                    shouldSpawnSecondCube = Random.value < 0.5f; // Decide whether to spawn secondCubePrefab
                }
            }
            else
            {
                prefabToSpawn = cubePrefab;
            }

            // Spawn the determined cube prefab
            SpawnCube(prefabToSpawn);

            // Randomize the spawn interval between minSpawnInterval and maxSpawnInterval
            float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

            // Wait for the random interval before spawning the next cube
            yield return new WaitForSeconds(randomInterval);
        }
    }
}
