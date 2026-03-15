using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ColorCargo.Data;

namespace ColorCargo.Core
{
    public class Train : MonoBehaviour
    {
        [Header("Data Config")]
        public TrainData trainData;

        [Header("Gameplay Settings")]
        public int activateThreshold = 5;
        public float movementSpeed = 0.3f;
        public float delayTime = 5f;

        [Header("References")]
        public GameObject[] cargoContainers;
        public GameObject spawnerB;
        public GameObject objectToActivate;
        public GameObject objectToDeactivate;
        public GameObject[] additionalObjectsToActivate;

        [Header("Audio")]
        public AudioSource cargoActivationSound;
        public AudioSource cargoDeactivationSound;
        public AudioSource cargoBDeactivationSound;
        public AudioSource trainSound;
        public AudioSource hornSound;

        [Header("VFX")]
        public ParticleSystem cargoActivationParticlePrefab;
        public ParticleSystem cargoDeactivationParticlePrefab;
        public GameObject explosionParticlePrefab;

        [Header("Optimization")]
        public float raycastInterval = 0.1f;
        public LayerMask trackLayer;

        private int _nextAvailableCargoIndex = 0;
        private bool _movementStarted = false;
        private bool _additionalObjectsActivated = false;
        private BoxCollider _boxCollider;
        private ITrack _currentTrack;

        public System.Action<Train> OnMovementStarted;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            // Deactivate all cargo containers initially
            foreach (var container in cargoContainers)
            {
                if (container != null) container.SetActive(false);
            }

            StartCoroutine(TrackDetectionRoutine());
        }

        private IEnumerator TrackDetectionRoutine()
        {
            WaitForSeconds wait = new WaitForSeconds(raycastInterval);
            while (true)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, 10f, trackLayer))
                {
                    ITrack track = hit.transform.GetComponent<ITrack>();
                    if (track != null)
                    {
                        if (_currentTrack != track)
                        {
                            _currentTrack?.SetOccupied(false);
                            _currentTrack = track;
                            _currentTrack.SetOccupied(true);
                        }
                    }
                    else
                    {
                        ClearTrack();
                    }
                }
                else
                {
                    ClearTrack();
                }
                yield return wait;
            }
        }

        private void ClearTrack()
        {
            if (_currentTrack != null)
            {
                _currentTrack.SetOccupied(false);
                _currentTrack = null;
            }
        }

        private void Update()
        {
            if (_movementStarted)
            {
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                HandleAdditionalActivation();
            }
            else if (_nextAvailableCargoIndex >= activateThreshold)
            {
                StartMovement();
            }
        }

        private void StartMovement()
        {
            _movementStarted = true;
            _boxCollider.enabled = false;

            if (hornSound != null) hornSound.Play();
            if (trainSound != null) trainSound.Play();
            if (objectToActivate != null) objectToActivate.SetActive(true);
            if (spawnerB != null) spawnerB.SetActive(true);
            if (objectToDeactivate != null) objectToDeactivate.SetActive(false);

            OnMovementStarted?.Invoke(this);
        }

        private float _timer = 0f;
        private void HandleAdditionalActivation()
        {
            if (!_additionalObjectsActivated)
            {
                _timer += Time.deltaTime;
                if (_timer >= delayTime)
                {
                    foreach (var obj in additionalObjectsToActivate)
                    {
                        if (obj != null) obj.SetActive(true);
                    }
                    _additionalObjectsActivated = true;
                }
            }
        }

        public void ActivateNextCargo()
        {
            if (_nextAvailableCargoIndex < cargoContainers.Length)
            {
                cargoContainers[_nextAvailableCargoIndex].SetActive(true);

                GameObject particlePrefab = cargoActivationParticlePrefab != null ? cargoActivationParticlePrefab.gameObject : (trainData != null ? trainData.activationParticlePrefab : null);
                if (particlePrefab != null)
                {
                    Vector3 particlePosition = cargoContainers[_nextAvailableCargoIndex].transform.position;
                    particlePosition.z -= 0.62f;
                    Instantiate(particlePrefab, particlePosition, Quaternion.identity);
                }

                _nextAvailableCargoIndex++;
                AdjustCollider(0.07f, -0.035f);

                AudioSource sound = cargoActivationSound != null ? cargoActivationSound : null; // Logic for sound from trainData could be added if AudioSource is not needed
                if (sound != null) sound.Play();
                else if (trainData != null && trainData.activationSound != null) AudioSource.PlayClipAtPoint(trainData.activationSound, transform.position);
            }
        }

        public void DeactivateOneCargo()
        {
            if (_nextAvailableCargoIndex > 0)
            {
                _nextAvailableCargoIndex--;
                cargoContainers[_nextAvailableCargoIndex].SetActive(false);
                AdjustCollider(-0.07f, 0.035f);

                GameObject particlePrefab = cargoDeactivationParticlePrefab != null ? cargoDeactivationParticlePrefab.gameObject : (trainData != null ? trainData.deactivationParticlePrefab : null);
                if (particlePrefab != null)
                {
                    Instantiate(particlePrefab, cargoContainers[_nextAvailableCargoIndex].transform.position, Quaternion.identity);
                }

                if (cargoDeactivationSound != null) cargoDeactivationSound.Play();
                else if (trainData != null && trainData.deactivationSound != null) AudioSource.PlayClipAtPoint(trainData.deactivationSound, transform.position);
            }
        }

        public void DeactivateTwoCargos()
        {
            int toDeactivate = Mathf.Min(2, _nextAvailableCargoIndex);
            for (int i = 0; i < toDeactivate; i++)
            {
                _nextAvailableCargoIndex--;
                cargoContainers[_nextAvailableCargoIndex].SetActive(false);
            }

            if (toDeactivate > 0)
            {
                AdjustCollider(-0.07f * toDeactivate, 0.035f * toDeactivate);
                if (cargoBDeactivationSound != null) cargoBDeactivationSound.Play();
                if (explosionParticlePrefab != null)
                {
                    Instantiate(explosionParticlePrefab, cargoContainers[_nextAvailableCargoIndex].transform.position, Quaternion.identity);
                }
            }
        }

        private void AdjustCollider(float sizeZ, float centerZ)
        {
            Vector3 size = _boxCollider.size;
            size.z += sizeZ;
            _boxCollider.size = size;

            Vector3 center = _boxCollider.center;
            center.z += centerZ;
            _boxCollider.center = center;
        }

        public int GetNextAvailableCargoIndex() => _nextAvailableCargoIndex;
        public bool IsMovementStarted() => _movementStarted;
        public CargoColor GetColor() => trainData != null ? trainData.color : CargoColor.Red;
    }
}
