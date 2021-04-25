using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
  // params
  [SerializeField] float projectileSpeed = 5f;

  // Start is called before the first frame update
  void Start()
  {
    // Add velocity
    GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
