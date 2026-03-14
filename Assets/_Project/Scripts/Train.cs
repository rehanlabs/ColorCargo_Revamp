using UnityEngine;

public class Train : MonoBehaviour
{
    public CargoContainer.CargoColor trainColor;
    public GameObject[] cargoContainers; // Array to hold references to the cargo containers

    public int activateThreshold = 5;
    public GameObject spawnerB;
    public GameObject additionalObject1; // Reference to the first additional GameObject to activate
    public GameObject additionalObject2;
    public GameObject additionalObject3;
    public GameObject objectToDeactivate;
    private bool movementStarted = false;
    private bool additionalObjectsActivated = false; // Flag to track if additional objects are activated
    private float timer = 0f;
    public float delayTime = 5f;
    public float movementSpeed = 0.3f;

    public GameObject objectToActivate;


    public AudioSource cargoActivationSound;
    public AudioSource cargoDeactivationSound;
    public AudioSource cargoBDeactivationSound;
    public AudioSource TrainSound;
    public AudioSource HornSound;

    public ParticleSystem cargoDeactivationParticlePrefab;
    public GameObject explosionParticlePrefab;
    public ParticleSystem cargoActivationParticlePrefab;



    private int nextAvailableCargoIndex = 0;

    public TrackM trackScript;
    public TrackM trackScriptR;
    public TrackM trackScriptL;
    public TrackM trackScriptRR;

    public TrackL LtrackScript;
    public TrackL LtrackScriptR;
    public TrackL LtrackScriptL;
    public TrackL LtrackScriptRR;

    public TrackLL LLtrackScript;
    public TrackLL LLtrackScriptR;
    public TrackLL LLtrackScriptL;
    public TrackLL LLtrackScriptRR;

    public TrackRR RRtrackScript;
    public TrackRR RRtrackScriptR;
    public TrackRR RRtrackScriptL;
    public TrackRR RRtrackScriptRR;

    private void Start()
    {
        // Deactivate all cargo containers initially
        foreach (var container in cargoContainers)
        {
            container.SetActive(false);
        }
    }


    


    private void Update()
    {
        // Raycasting from the train's position
        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit hit;

        if (trackScript != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("Track"))
                {
                    trackScript.SetRaycastHit(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    trackScript.SetRaycastHit(false);
                }
            }
            else
            {
                trackScript.SetRaycastHit(false);

            }
        }

        if (trackScriptR != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("TrackR"))
                {
                    trackScriptR.SetRaycastHit(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    trackScriptR.SetRaycastHit(false);
                }
            }
            else
            {
                trackScriptR.SetRaycastHit(false);
            }
        }

        if (trackScriptL != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("TrackL"))
                {
                    trackScriptL.SetRaycastHit(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    trackScriptL.SetRaycastHit(false);
                }
            }
            else
            {
                trackScriptL.SetRaycastHit(false);
            }
        }

        if (trackScriptRR != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("TrackRR"))
                {
                    trackScriptRR.SetRaycastHit(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    trackScriptRR.SetRaycastHit(false);
                }
            }
            else
            {
                trackScriptRR.SetRaycastHit(false);
            }
        }


        if (LtrackScript != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("Track"))
                {
                    LtrackScript.SetRaycastHitL(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    LtrackScript.SetRaycastHitL(false);
                }
            }
            else
            {
                LtrackScript.SetRaycastHitL(false);

            }
        }

        if (LtrackScriptR != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("TrackR"))
                {
                    LtrackScriptR.SetRaycastHitL(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    LtrackScriptR.SetRaycastHitL(false);
                }
            }
            else
            {
                LtrackScriptR.SetRaycastHitL(false);
            }
        }

        if (LtrackScriptL != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("TrackL"))
                {
                    LtrackScriptL.SetRaycastHitL(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    LtrackScriptL.SetRaycastHitL(false);
                }
            }
            else
            {
                LtrackScriptL.SetRaycastHitL(false);
            }
        }

        if (LtrackScriptRR != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("TrackRR"))
                {
                    LtrackScriptRR.SetRaycastHitL(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    LtrackScriptRR.SetRaycastHitL(false);
                }
            }
            else
            {
                LtrackScriptRR.SetRaycastHitL(false);
            }
        }




        if (RRtrackScript != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("Track"))
                {
                    RRtrackScript.SetRaycastHitR(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    RRtrackScript.SetRaycastHitR(false);
                }
            }
            else
            {
                RRtrackScript.SetRaycastHitR(false);

            }
        }

        if (RRtrackScriptR != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("TrackR"))
                {
                    RRtrackScriptR.SetRaycastHitR(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    RRtrackScriptR.SetRaycastHitR(false);
                }
            }
            else
            {
                RRtrackScriptR.SetRaycastHitR(false);
            }
        }

        if (RRtrackScriptL != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("TrackL"))
                {
                    RRtrackScriptL.SetRaycastHitR(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    RRtrackScriptL.SetRaycastHitR(false);
                }
            }
            else
            {
                RRtrackScriptL.SetRaycastHitR(false);
            }
        }

        if (RRtrackScriptRR != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("TrackRR"))
                {
                    RRtrackScriptRR.SetRaycastHitR(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    RRtrackScriptRR.SetRaycastHitR(false);
                }
            }
            else
            {
                RRtrackScriptRR.SetRaycastHitR(false);
            }
        }




        if (LLtrackScript != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("Track"))
                {
                    LLtrackScript.SetRaycastHitLL(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    LLtrackScript.SetRaycastHitLL(false);
                }
            }
            else
            {
                LLtrackScript.SetRaycastHitLL(false);

            }
        }

        if (LLtrackScriptR != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("TrackR"))
                {
                    LLtrackScriptR.SetRaycastHitLL(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    LLtrackScriptR.SetRaycastHitLL(false);
                }
            }
            else
            {
                LLtrackScriptR.SetRaycastHitLL(false);
            }
        }

        if (LLtrackScriptL != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("TrackL"))
                {
                    LLtrackScriptL.SetRaycastHitLL(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    LLtrackScriptL.SetRaycastHitLL(false);
                }
            }
            else
            {
                LLtrackScriptL.SetRaycastHitLL(false);
            }
        }

        if (LLtrackScriptRR != null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the ray hit a track object
                if (hit.transform.CompareTag("TrackRR"))
                {
                    LLtrackScriptRR.SetRaycastHitLL(true); // Call the SetRaycastHit method in the Track script
                }
                else
                {
                    LLtrackScriptRR.SetRaycastHitLL(false);
                }
            }
            else
            {
                LLtrackScriptRR.SetRaycastHitLL(false);
            }
        }







        // Check if the activation threshold is reached
        if (nextAvailableCargoIndex >= activateThreshold)
        {

            if (!movementStarted)
            {
                // Disable collision detection if movement starts
                GetComponent<BoxCollider>().enabled = false;
                movementStarted = true;
                if (HornSound != null)
                {
                    HornSound.Play();
                }
                if (TrainSound != null)
                {
                    TrainSound.Play();
                }

                if (objectToActivate != null)
                {
                    objectToActivate.SetActive(true);
                }
            }
            // Move the train forward
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);




            // Activate the "Spawner B" GameObject if it's set
            if (spawnerB != null)
            {
                spawnerB.SetActive(true);
            }

            // Deactivate the specified GameObject if it's set
            if (objectToDeactivate != null)
            {
                objectToDeactivate.SetActive(false);
            }

            // Activate additional objects after the delay time
            if (!additionalObjectsActivated)
            {
                timer += Time.deltaTime;
                if (timer >= delayTime)
                {
                    if (additionalObject1 != null)
                    {
                        additionalObject1.SetActive(true);
                    }
                    if (additionalObject2 != null)
                    {
                        additionalObject2.SetActive(true);
                    }
                    if (additionalObject3 != null)
                    {
                        additionalObject3.SetActive(true);
                    }
                    additionalObjectsActivated = true; // Set the flag to prevent repeated activation
                }
            }
        }
    }

    public int GetNextAvailableCargoIndex()
    {
        return nextAvailableCargoIndex;
    }

    public bool IsMovementStarted()
    {
        return movementStarted;
    }


    public void ActivateNextCargo()
    {
        // Check if there are available cargo containers
        if (nextAvailableCargoIndex < cargoContainers.Length)
        {
            // Activate the next available cargo container
            cargoContainers[nextAvailableCargoIndex].SetActive(true);
            nextAvailableCargoIndex++;

            // Adjust the size of the box collider when a new container is added
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            Vector3 colliderSize = boxCollider.size;
            colliderSize.z += 0.07f;
            boxCollider.size = colliderSize;

            Vector3 colliderCenter = boxCollider.center;
            colliderCenter.z -= 0.035f;
            boxCollider.center = colliderCenter;

            if (cargoActivationSound != null)
            {
                cargoActivationSound.Play();
            }

            if (cargoActivationParticlePrefab != null)
            {
                Vector3 particlePosition = cargoContainers[nextAvailableCargoIndex].transform.position;
                particlePosition.z -= 0.62f; // Decrease the Z component
                Instantiate(cargoActivationParticlePrefab, particlePosition, Quaternion.identity);
            }


        }
    }


    public void DeactivateOneCargo()
    {
        // Check if there are available cargo containers to deactivate
        if (nextAvailableCargoIndex > 0)
        {
            // Deactivate the last activated cargo container
            nextAvailableCargoIndex--;
            cargoContainers[nextAvailableCargoIndex].SetActive(false);

            // Adjust the size of the box collider when a container is deactivated
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            Vector3 colliderSize = boxCollider.size;
            colliderSize.z -= 0.07f;
            boxCollider.size = colliderSize;

            Vector3 colliderCenter = boxCollider.center;
            colliderCenter.z += 0.035f;
            boxCollider.center = colliderCenter;

            if (cargoDeactivationSound != null)
            {
                cargoDeactivationSound.Play();
            }

            // Instantiate the cargo deactivation particle effect at the deactivated cargo's position
            if (cargoDeactivationParticlePrefab != null)
            {
                Instantiate(cargoDeactivationParticlePrefab, cargoContainers[nextAvailableCargoIndex].transform.position, Quaternion.identity);
            }

        }
    }


    public void Deactivatetwocargos()
    {
        // Check if there are any available cargo containers to deactivate
        if (nextAvailableCargoIndex > 0)
        {
            int containersToDeactivate = Mathf.Min(2, nextAvailableCargoIndex); // Calculate how many containers to deactivate

            // Deactivate the specified number of cargo containers
            for (int i = 0; i < containersToDeactivate; i++)
            {
                nextAvailableCargoIndex--;
                cargoContainers[nextAvailableCargoIndex].SetActive(false);
            }

            // Adjust the size of the box collider when containers are deactivated
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            Vector3 colliderSize = boxCollider.size;
            colliderSize.z -= 0.07f * containersToDeactivate;
            boxCollider.size = colliderSize;

            Vector3 colliderCenter = boxCollider.center;
            colliderCenter.z += 0.035f * containersToDeactivate;
            boxCollider.center = colliderCenter;

            if (cargoBDeactivationSound != null)
            {
                cargoBDeactivationSound.Play();
            }

            if (explosionParticlePrefab != null)
            {
                Instantiate(explosionParticlePrefab, cargoContainers[nextAvailableCargoIndex].transform.position, Quaternion.identity);
            }

        }
    }


}