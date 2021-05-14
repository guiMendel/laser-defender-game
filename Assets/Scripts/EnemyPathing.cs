using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
  WaveConfig waveConfig;
  List<Transform> waypoints;
  int waypointIndex = 0;

  public void SetWaveConfig(WaveConfig waveConfig)
  {
    this.waveConfig = waveConfig;
  }

  // Start is called before the first frame update
  void Start()
  {
    waypoints = waveConfig.GetWaypoints();
    transform.position = waypoints[waypointIndex++].position;
  }

  // Update is called once per frame
  void Update() { Move(); }

  private void Move()
  {
    // If not yet at last waypoint
    if (waypointIndex < waypoints.Count)
    {
      var targetPosition = waypoints[waypointIndex].position;
      var displacement = waveConfig.GetMoveSpeed() * Time.deltaTime;
      transform.position = Vector2.MoveTowards(transform.position, targetPosition, displacement);

      if (transform.position == targetPosition) waypointIndex++;
    }
    // If done, check if loops
    else if (waveConfig.Loops()) waypointIndex = 1;
    // If not, die!
    else Destroy(gameObject);
  }
}
