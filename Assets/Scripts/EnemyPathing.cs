using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
  [SerializeField] List<Transform> waypoints;
  [SerializeField] float moveSpeed = 5f;
  int waypointIndex = 0;

  // Start is called before the first frame update
  void Start()
  {
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
      var displacement = moveSpeed * Time.deltaTime;
      transform.position = Vector2.MoveTowards(transform.position, targetPosition, displacement);

      if (transform.position == targetPosition) waypointIndex++;
    }
    // If done, destroy self!
    else Destroy(gameObject);
  }
}
