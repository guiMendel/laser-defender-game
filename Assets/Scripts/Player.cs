using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  // params
  [SerializeField] int health = 500;
  [SerializeField] int gameoverTimeout = 2;
  [SerializeField] Level levelHandler;

  [Header("Movement")]
  [SerializeField] float moveSpeed = 10f;
  [SerializeField] float moveBoundaryPaddingX = 1f;
  [SerializeField] float moveBoundaryPaddingY = 1f;

  [Header("Shooting")]
  [SerializeField] GameObject laserPrefab;
  [SerializeField] float laserFiringRate = 0.1f;

  [Header("VFX")]
  [SerializeField] GameObject explosionVFX;
  [SerializeField] GameObject hitVFX;
  [SerializeField] float vfxDuration = 1f;

  [Header("SFX")]
  [SerializeField] AudioClip explosionSFX;
  [SerializeField] AudioClip hitSFX;
  [SerializeField] AudioClip fireSFX;
  [SerializeField] [Range(0, 1)] float explosionSFXVolume = 0.75f;
  [SerializeField] [Range(0, 1)] float hitSFXVolume = 0.75f;
  [SerializeField] [Range(0, 1)] float fireSFXVolume = 0.75f;


  // state

  Coroutine firingCoroutine;

  // movement boundaries
  float xMin;
  float xMax;
  float yMin;
  float yMax;

  public int GetHealth() { return health; }

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

  /////////////////// MOVEMENT

  private void SetMoveBoundaries()
  {
    Camera gameCamera = Camera.main;
    xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + moveBoundaryPaddingX;
    xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - moveBoundaryPaddingX;

    yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + moveBoundaryPaddingY;
    yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - moveBoundaryPaddingY;
  }

  private void Move()
  {
    // Get X variation
    var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

    // Get new position, clamped to screen space
    var newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

    // Get Y variation
    var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

    // Get new position, clamped to screen space
    var newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

    transform.position = new Vector2(newXPosition, newYPosition);
  }

  /////////////////// FIRING

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
    // Play sfx
    AudioSource.PlayClipAtPoint(fireSFX, Camera.main.transform.position, fireSFXVolume);

    Instantiate(laserPrefab, transform.position, Quaternion.identity);
  }

  /////////////////// COLLISION

  private void TakeHit(DamageDealer damageDealer)
  {
    health -= damageDealer.GetDamage();

    // Regsiter the hit
    damageDealer.Hit();

    // Play vfx
    var vfxInstance = Instantiate(hitVFX, transform.position, Quaternion.identity) as GameObject;
    // Play sfx
    AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, hitSFXVolume);

    Destroy(vfxInstance, vfxDuration);

    // Die if dead
    if (health <= 0) Explode();
  }

  void Explode()
  {
    // Play VFX
    var vfxInstance = Instantiate(explosionVFX, transform.position, Quaternion.identity) as GameObject;
    // Play SFX
    AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, explosionSFXVolume);

    Destroy(vfxInstance, vfxDuration);

    // Prepare game over load
    levelHandler.LoadGameOver(gameoverTimeout);

    Destroy(gameObject);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

    if (damageDealer != null) TakeHit(damageDealer);
  }

}
