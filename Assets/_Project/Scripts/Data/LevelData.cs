using System.Collections.Generic;
using UnityEngine;

namespace ColorCargo.Data
{
    [CreateAssetMenu(fileName = "NewLevelData", menuName = "ColorCargo/Level Data")]
    public class LevelData : ScriptableObject
    {
        public string levelName;
        public List<TrainConfig> trains;
        public int winThreshold; // If it's a "cargo count" win
        public int requiredTrainsMoving; // If it's a "all trains moving" win

        public List<CargoSpawnConfig> cargoSpawns;

        public AudioClip backgroundMusic;
    }

    [System.Serializable]
    public class TrainConfig
    {
        public TrainData trainData;
        public int activationThreshold;
        public float movementSpeed;
    }

    [System.Serializable]
    public class CargoSpawnConfig
    {
        public GameObject cargoPrefab;
        public float minInterval;
        public float maxInterval;
        public float startTime;
    }
}
