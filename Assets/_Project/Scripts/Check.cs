using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public GameObject[] trainObjects;   // References to all train objects
    public GameObject[] objectsToMove;  // The objects to move after train movements
    public float delayTime = 5f;        // Delay before moving the object

    private int objectsMoved = 0;       // Number of objects moved so far
    private bool[] trainObjectsMoving;   // Flags to track if each train object has started moving
    private float[] timers;              // Timers for each train object
    private bool[] objectMovedFlags;     // Flags to track if an object has been moved for each train movement

    void Start()
    {
        trainObjectsMoving = new bool[trainObjects.Length];
        timers = new float[trainObjects.Length];
        objectMovedFlags = new bool[trainObjects.Length];
    }

    void Update()
    {
        for (int i = 0; i < trainObjects.Length; i++)
        {
            if (!objectMovedFlags[i])
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
                        Vector3 newPosition = new Vector3(trainObjects[i].transform.position.x, objectsToMove[objectsMoved].transform.position.y, objectsToMove[objectsMoved].transform.position.z);
                        objectsToMove[objectsMoved].transform.position = newPosition;

                        objectMovedFlags[i] = true;
                        objectsMoved++;

                        if (objectsMoved >= objectsToMove.Length)
                        {
                            enabled = false; // Disable the script after all objects are moved
                        }
                    }
                }
            }
        }
    }
}
