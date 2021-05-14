using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField] int health = 250;
  [SerializeField] int scoreOnHit;
  [SerializeField] int scoreOnKill;

  [Header("VFX")]
  [SerializeField] GameObject hitVFX;
  [SerializeField] GameObject explosionVFX;
  [SerializeField] float vfxDuration = 1f;

  [Header("SFX")]
  [SerializeField] AudioClip explosionSFX;
  [SerializeField] AudioClip hitSFX;
  [SerializeField] AudioClip fireSFX;
  [SerializeField] [Range(0, 1)] float explosionSFXVolume = 0.75f;
  [SerializeField] [Range(0, 1)] float hitSFXVolume = 0.75f;
  [SerializeField] [Range(0, 1)] float fireSFXVolume = 0.75f;

  [Header("Shooting")]
  [SerializeField] float fireCountdown;
  [SerializeField] float minTimeBetweenShots = 0.2f;
  [SerializeField] float maxTimeBetweenShots = 3f;
  [SerializeField] GameObject laserPrefab;

  // references
  GameSession gameSession;

  // Start is called before the first frame update
  void Start()
  {
    gameSession = FindObjectOfType<GameSession>();
    ResetFireCountdown();
  }

  // Update is called once per frame
  void Update()
  {
    CountdownFire();
  }

  private void ResetFireCountdown()
  {
    fireCountdown = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
  }

  private void CountdownFire()
  {
    fireCountdown -= Time.deltaTime;
    if (fireCountdown <= 0)
    {
      Fire();
      ResetFireCountdown();
    }
  }

  private void Fire()
  {
    // Play sfx
    AudioSource.PlayClipAtPoint(fireSFX, Camera.main.transform.position, fireSFXVolume);

    Instantiate(laserPrefab, transform.position, Quaternion.identity);
  }

  private void TakeHit(DamageDealer damageDealer)
  {
    health -= damageDealer.GetDamage();

    // Add score
    gameSession.AddToScore(scoreOnHit);

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
    // Score
    gameSession.AddToScore(scoreOnKill);
    
    // Play VFX
    var vfxInstance = Instantiate(explosionVFX, transform.position, Quaternion.identity) as GameObject;

    Destroy(vfxInstance, vfxDuration);

    // Play SFX
    AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, explosionSFXVolume);

    Destroy(gameObject);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

    if (damageDealer != null) TakeHit(damageDealer);
  }
}
