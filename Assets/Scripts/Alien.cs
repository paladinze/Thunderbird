using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] GameObject destructionVfx;
    [SerializeField] float health = 100; 
    [SerializeField] float minTimeTillNextFire = 1.0f;
    [SerializeField] float maxTimeTillNextFire = 3.0f;
    [SerializeField] AudioClip deathAudio;
    [SerializeField] AudioClip fireAudio;

    private float timeTillNextFire;

    // Start is called before the first frame update
    void Start()
    {
        ResetFireTimer();
    }

    // Update is called once per frame
    void Update()
    {
        WaitAndFire();
    }

  private void WaitAndFire()
  {
    timeTillNextFire -= Time.deltaTime;
    if (timeTillNextFire <= 0)  {
        Fire();
        ResetFireTimer();
    }
  }

  private void Fire() {
      var bullet = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
      var bulletVelocity = new Vector2(0f, -10f);
      bullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity; 
      AudioSource.PlayClipAtPoint(fireAudio, Camera.main.transform.position, 0.125f);
  }

  private void ResetFireTimer() {
    timeTillNextFire = UnityEngine.Random.Range(minTimeTillNextFire, maxTimeTillNextFire);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
    ProcessHit(damageDealer);
  }

  private void ProcessHit(DamageDealer damageDealer)
  {
    health -= damageDealer.GetDamage();
    damageDealer.Hit();
    if (health <= 0)
    {
      Die();
    }
  }

  private void Die()
  {
    var vfx = Instantiate(destructionVfx, gameObject.transform.position, Quaternion.identity);
    AudioSource.PlayClipAtPoint(deathAudio, Camera.main.transform.position);
    Destroy(gameObject);
  }
}
