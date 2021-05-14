using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
  [SerializeField] GameObject enemyPrefab;
  [SerializeField] GameObject pathPrefab;
  [SerializeField] float spawnRate = 0.5f;
  [SerializeField] float spawnRandomFactor = 0.3f;
  [SerializeField] int numberOfEnemies;
  [SerializeField] float moveSpeed = 2f;
  [SerializeField] bool loop = false;

  public GameObject GetEnemyPrefab() { return enemyPrefab; }
  public float GetSpawnRate() { return spawnRate; }
  public float GetSpawnRandomFactor() { return spawnRandomFactor; }
  public int GetNumberOfEnemies() { return numberOfEnemies; }
  public float GetMoveSpeed() { return moveSpeed; }
  public bool Loops() { return loop; }

  public List<Transform> GetWaypoints()
  {
    var waveWaypoints = new List<Transform>();

    foreach (Transform child in pathPrefab.transform) waveWaypoints.Add(child);

    return waveWaypoints;
  }


}
