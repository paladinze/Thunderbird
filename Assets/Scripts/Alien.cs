﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] bool weaponAutoAim = false;
    [SerializeField] GameObject destructionVfx;
    [SerializeField] GameObject hitVfx;
    [SerializeField] float health = 100; 
    [SerializeField] float score = 10;
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
      var player = FindObjectOfType<PlayerShip>();
      var bulletVelocity = new Vector2(0f, -10f);
      if (weaponAutoAim && player != null) {
        var alienToPlayerVector = player.transform.position - transform.position;
        bulletVelocity = alienToPlayerVector;
      }
      bullet.GetComponent<Rigidbody2D>().velocity = bulletVelocity; 
      AudioSource.PlayClipAtPoint(fireAudio, Camera.main.transform.position, 0.125f);
  }

  private void ResetFireTimer() {
    timeTillNextFire = UnityEngine.Random.Range(minTimeTillNextFire, maxTimeTillNextFire);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
    if (damageDealer == null) return; 
    health -= damageDealer.GetDamage();
    if (!collision.gameObject.tag.Equals("player")) {
      damageDealer.Destroy();
    }
    if (health <= 0)
    {
      Die();
    } else {
      Instantiate(hitVfx, gameObject.transform.position, Quaternion.identity);
    }
  }

  private void Die()
  {
    Instantiate(destructionVfx, gameObject.transform.position, Quaternion.identity);
    AudioSource.PlayClipAtPoint(deathAudio, Camera.main.transform.position);
    Destroy(gameObject);
    FindObjectOfType<GameSession>().addScore(score);
  }
}
