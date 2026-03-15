using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ColorCargo.Core;
using ColorCargo.Data;

namespace ColorCargo.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Level Configuration")]
        public LevelData currentLevelData;

        [Header("UI References")]
        public GameObject levelWinPanel;
        public GameObject gameOverPanel;
        public Text timerText;
        public Text bestTimeText;

        [Header("Audio")]
        public AudioSource winBGM;

        [Header("Settings")]
        public float winDelay = 2f;

        private List<Train> _trains = new List<Train>();
        private int _movingTrainsCount = 0;
        private bool _isGameOver = false;
        private bool _isLevelWon = false;

        public bool IsLevelWon => _isLevelWon;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (levelWinPanel != null) levelWinPanel.SetActive(false);

            // Find all trains in the scene and subscribe to their events
            _trains.AddRange(FindObjectsOfType<Train>());
            foreach (var train in _trains)
            {
                train.OnMovementStarted += HandleTrainMovementStarted;
            }
        }

        private void HandleTrainMovementStarted(Train train)
        {
            _movingTrainsCount++;
            CheckWinCondition();
        }

        private void CheckWinCondition()
        {
            if (_isLevelWon) return;

            int required = currentLevelData != null ? currentLevelData.requiredTrainsMoving : _trains.Count;

            if (_movingTrainsCount >= required)
            {
                _isLevelWon = true;
                StartCoroutine(WinSequence());
            }
        }

        private IEnumerator WinSequence()
        {
            yield return new WaitForSeconds(winDelay);

            float currentTime = 0;
            if (timerText != null && float.TryParse(timerText.text, out float t))
            {
                currentTime = t;
            }

            SaveBestTime(currentTime);
            StopAllBGM();

            if (winBGM != null) winBGM.Play();
            if (levelWinPanel != null) levelWinPanel.SetActive(true);
        }

        private void SaveBestTime(float time)
        {
            string levelName = SceneManager.GetActiveScene().name;
            string key = "BestTime" + levelName;
            float bestTime = PlayerPrefs.GetFloat(key, Mathf.Infinity);

            if (time < bestTime)
            {
                PlayerPrefs.SetFloat(key, time);
            }
        }

        private void StopAllBGM()
        {
            GameObject[] bgms = GameObject.FindGameObjectsWithTag("BGM");
            foreach (var bgm in bgms)
            {
                bgm.GetComponent<AudioSource>()?.Stop();
            }

            // Add other BGM tags if they exist
            string[] otherTags = { "BGM2", "BGM3" };
            foreach (var tag in otherTags)
            {
                GameObject[] others = GameObject.FindGameObjectsWithTag(tag);
                foreach (var other in others)
                {
                    other.GetComponent<AudioSource>()?.Stop();
                }
            }
        }

        public void GameOver()
        {
            if (_isGameOver) return;
            _isGameOver = true;
            Time.timeScale = 0;

            StopAllBGM();
            if (gameOverPanel != null) gameOverPanel.SetActive(true);
        }
    }
}
