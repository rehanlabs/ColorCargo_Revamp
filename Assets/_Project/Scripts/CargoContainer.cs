using UnityEngine;
using ColorCargo.Data;
using ColorCargo.Core;
using ColorCargo.Managers;

namespace ColorCargo.Core
{
    public class CargoContainer : MonoBehaviour
    {
        [Header("Data Config")]
        public TrainData[] possibleTrainData;

        [Header("Settings")]
        public float minMoveSpeed = 3f;
        public float maxMoveSpeed = 7f;
        public GameObject gameOverPanel;
        public AudioSource gameOverBGM;

        private TrainData _currentData;
        private float _currentMoveSpeed;
        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void Start()
        {
            InitializeCargo();
        }

        private void InitializeCargo()
        {
            if (possibleTrainData != null && possibleTrainData.Length > 0)
            {
                _currentData = possibleTrainData[Random.Range(0, possibleTrainData.Length)];
                ApplyVisuals();
            }

            _currentMoveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
            if (gameOverPanel != null) gameOverPanel.SetActive(false);
        }

        private void ApplyVisuals()
        {
            if (_currentData == null) return;

            if (ColorUtility.TryParseHtmlString(_currentData.hexColor, out Color color))
            {
                _renderer.material.color = color;
                _renderer.material.SetColor("_EmissionColor", _currentData.emissionColor);
            }
        }

        private void Update()
        {
            transform.Translate(Vector3.back * _currentMoveSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Train"))
            {
                Train train = collision.gameObject.GetComponent<Train>();
                if (train != null)
                {
                    HandleTrainCollision(train);
                }
                Destroy(gameObject);
            }
        }

        private void HandleTrainCollision(Train train)
        {
            if (train.GetColor() == _currentData.color)
            {
                train.ActivateNextCargo();
            }
            else
            {
                if (train.GetNextAvailableCargoIndex() == 0)
                {
                    TriggerGameOver();
                }
                else
                {
                    train.DeactivateOneCargo();
                }
            }
        }

        private void TriggerGameOver()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
                if (gameOverBGM != null) gameOverBGM.Play();
            }
            else
            {
                Time.timeScale = 0;
                if (gameOverBGM != null) gameOverBGM.Play();
                if (gameOverPanel != null) gameOverPanel.SetActive(true);
            }
        }
    }
}
