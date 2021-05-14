using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
  // params
  [SerializeField] float projectileSpeed = 5f;
  [SerializeField] GameObject hitVFX;
  [SerializeField] float vfxDuration = 1f;

  // Start is called before the first frame update
  void Start()
  {
    // Add velocity
    GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
  }

  public void Hit()
  {
    if (hitVFX)
    {
      var vfxInstance = Instantiate(hitVFX, transform.position, Quaternion.identity) as GameObject;
      Destroy(vfxInstance, vfxDuration);
    }
    Destroy(gameObject);
  }
}
