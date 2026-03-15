using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorCargo.Data;

namespace ColorCargo.Core
{
    public class CargoSpawner : MonoBehaviour
    {
        [Header("Configuration")]
        public List<CargoSpawnConfig> spawnConfigs;

        private void Start()
        {
            foreach (var config in spawnConfigs)
            {
                StartCoroutine(SpawnRoutine(config));
            }
        }

        private IEnumerator SpawnRoutine(CargoSpawnConfig config)
        {
            if (config.startTime > 0)
            {
                yield return new WaitForSeconds(config.startTime);
            }

            while (true)
            {
                Instantiate(config.cargoPrefab, transform.position, Quaternion.identity);
                float interval = Random.Range(config.minInterval, config.maxInterval);
                yield return new WaitForSeconds(interval);
            }
        }
    }
}
