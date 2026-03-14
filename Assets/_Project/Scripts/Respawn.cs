using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject[] trainObjects;   // References to all train objects
    public GameObject[] objectsToSpawn; // The objects to spawn after train movements
    public float delayTime = 5f;        // Delay before spawning the new object

    private int objectsSpawned = 0;     // Number of objects spawned so far
    private bool[] trainObjectsMoving;   // Flags to track if each train object has started moving
    private float[] timers;              // Timers for each train object
    private bool[] objectSpawnedFlags;   // Flags to track if an object has been spawned for each train movement

    void Start()
    {
        trainObjectsMoving = new bool[trainObjects.Length];
        timers = new float[trainObjects.Length];
        objectSpawnedFlags = new bool[trainObjects.Length];
    }

    void Update()
    {
        for (int i = 0; i < trainObjects.Length; i++)
        {
            if (!objectSpawnedFlags[i])
            {
                if (!trainObjectsMoving[i])
                {
                    if (trainObjects[i].GetComponent<Train>().IsMovementStarted())
                    {
                        trainObjectsMoving[i] = true;
                    }
                }

                if (trainObjectsMoving[i])
                {
                    timers[i] += Time.deltaTime;

                    if (timers[i] >= delayTime)
                    {
                        Vector3 spawnPosition = new Vector3(trainObjects[i].transform.position.x, transform.position.y, transform.position.z);
                        GameObject spawnedObject = Instantiate(objectsToSpawn[objectsSpawned], spawnPosition, trainObjects[i].transform.rotation);
                        spawnedObject.name = objectsToSpawn[objectsSpawned].name;

                        spawnedObject.SetActive(true);

                        objectSpawnedFlags[i] = true;
                        objectsSpawned++;

                        if (objectsSpawned >= objectsToSpawn.Length)
                        {
                            enabled = false; // Disable the script after all objects are spawned
                        }
                    }
                }
            }
        }
    }
}