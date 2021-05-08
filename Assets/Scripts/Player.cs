using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  // params
  [SerializeField] float moveSpeed = 10f;
  [SerializeField] float moveBoundaryPadding = 10f;
  [SerializeField] GameObject laserPrefab;
  [SerializeField] float laserFiringRate = 0.1f;

  // state

  Coroutine firingCoroutine;

  // movement boundaries
  float xMin;
  float xMax;

  private void Start()
  {
    SetMoveBoundaries();
  }

  // Update is called once per frame
  void Update()
  {
    Move();
    CommandFire();
  }

  private void CommandFire()
  {
    // Starts firing
    void StartFiring()
    {
      // Prevents coroutine leak
      StopFiring();

      firingCoroutine = StartCoroutine(FireContinuously());
    }
    // Stops firing
    void StopFiring()
    {
      if (firingCoroutine != null) StopCoroutine(firingCoroutine);
      firingCoroutine = null;
    }

    // Starts firing on first button press
    if (Input.GetButtonDown("Fire1")) StartFiring();

    // Stop firing on button release
    if (Input.GetButtonUp("Fire1")) StopFiring();

  }

  private IEnumerator FireContinuously()
  {
    while (true)
    {
      Fire();
      yield return new WaitForSeconds(laserFiringRate);
    }
  }

  // Fires a projectile
  private void Fire()
  {
    Instantiate(laserPrefab, transform.position, Quaternion.identity);
  }

  private void Move()
  {
    // Get X variation
    var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

    // Get new position, clamped to screen space
    var newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);


    transform.position = new Vector2(newXPosition, transform.position.y);
  }

  private void SetMoveBoundaries()
  {
    Camera gameCamera = Camera.main;
    xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + moveBoundaryPadding;
    xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - moveBoundaryPadding;
  }


}
