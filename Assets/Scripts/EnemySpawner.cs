using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
  [SerializeField] List<WaveConfig> wavesConfigs;
  [SerializeField] bool looping = false;
  int startingWave = 0;

  // Start is called before the first frame update
  IEnumerator Start()
  {
    do { yield return StartCoroutine(SpawnAllWaves()); }
    while (looping);
  }

  private IEnumerator SpawnAllWaves()
  {
    // Spawn each one of the waves
    for (int waveIndex = startingWave; waveIndex < wavesConfigs.Count; waveIndex++)
    {
      var currentWave = wavesConfigs[waveIndex];
      yield return StartCoroutine(SpawnAllWaveEnemies(currentWave));
    }
  }

  private IEnumerator SpawnAllWaveEnemies(WaveConfig waveConfig)
  {
    // Spawn as many enemies as requested
    for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
    {
      var enemyInstance = Instantiate(
        waveConfig.GetEnemyPrefab(),
        waveConfig.GetWaypoints()[0].position,
        Quaternion.identity
      );

      // Set enemies waveConfig
      enemyInstance.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

      yield return new WaitForSeconds(waveConfig.GetSpawnRate());
    }

    // If wave loops, wait until they're all dead
    if (waveConfig.Loops())
    {
      while (FindObjectsOfType<Enemy>().Length > 0) yield return null;
    }
  }
}
