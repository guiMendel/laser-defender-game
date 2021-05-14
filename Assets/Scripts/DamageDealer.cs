using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
  [SerializeField] int damage = 100;

  public int GetDamage() { return damage; }

  public void Hit() { 
    EnemyLaser laser;
    if (laser = GetComponent<EnemyLaser>()) {
      laser.Hit();
    }
    else Destroy(gameObject);
   }
}
